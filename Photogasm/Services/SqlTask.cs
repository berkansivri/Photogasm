using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace Photogasm
{
    public class SqlTask
    {
        public static string connString = "Data Source=teambro.database.windows.net;Initial Catalog=PhotochArt_db;Integrated Security=False;User ID=n00ne;Password=123Qwe123;Connect Timeout=90;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static SqlConnection conn = new SqlConnection(connString);

        public SqlTask()
        {
            if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); }
            
        }

        public static string GetUserIdByUrl(string url)
        {
            conn = new SqlConnection(connString);
            try
            {
                string userid = string.Empty;
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID FROM Users WHERE Url=@url", SqlTask.conn);
                cmd.Parameters.AddWithValue("@url", url);
                if (cmd.ExecuteScalar() != null)
                {
                    userid = cmd.ExecuteScalar().ToString();
                }
                return userid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

        public static string GetUserUrlById(string id)
        {
            conn = new SqlConnection(connString);
            try
            {
                string url = string.Empty;
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Url FROM Users WHERE ID=@id", SqlTask.conn);
                cmd.Parameters.AddWithValue("@id", id);
                if (cmd.ExecuteScalar() != null)
                {
                    id = cmd.ExecuteScalar().ToString();
                }
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static User GetUserInfo(string id)
        {
            
            conn = new SqlConnection(connString);
            try
            {
                User user = new User();
                conn.Open();
                SqlCommand getInfo = new SqlCommand("SELECT * FROM [Users] WHERE ID=@id", conn);
                getInfo.Parameters.AddWithValue("@id", id);

                SqlCommand getPhotos = new SqlCommand("SELECT COUNT(PID) FROM [Photos] WHERE Photos.U_ID=@id AND publish='1'", conn);
                getPhotos.Parameters.AddWithValue("@id", id);
                int totalphoto = (int)getPhotos.ExecuteScalar();

                int totallike = 0;
                if (totalphoto > 0)
                {
                    SqlCommand getLike = new SqlCommand("SELECT SUM(T_Like) FROM [Photos] WHERE Photos.U_ID=@id and publish='1'", conn);
                    getLike.Parameters.AddWithValue("@id", id);
                    totallike = Convert.ToInt32(getLike.ExecuteScalar());
                }


                SqlCommand getProjects = new SqlCommand("SELECT COUNT(Pro_ID) FROM [Projects] WHERE Projects.User_ID=@id", conn);
                getProjects.Parameters.AddWithValue("@id", id);
                int totalproject = (int)getProjects.ExecuteScalar();

                if (getInfo.ExecuteScalar() != null)
                {
                    using (SqlDataReader reader = getInfo.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ordinal = reader.GetOrdinal("ID");
                            if (!reader.IsDBNull(ordinal))
                                user.ID = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("Email");
                            if (!reader.IsDBNull(ordinal))
                                user.Email = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("FName");
                            if (!reader.IsDBNull(ordinal))
                                user.FName = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("SName");
                            if (!reader.IsDBNull(ordinal))
                                user.SName = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("Disc");
                            if (!reader.IsDBNull(ordinal))
                                user.Desc = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("ImgPath");
                            if (!reader.IsDBNull(ordinal))
                                user.ImgPath = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("Url");
                            if (!reader.IsDBNull(ordinal))
                                user.URL = reader.GetString(ordinal);
                            user.totalLike = totallike;
                            user.totalPhoto = totalphoto;
                            user.totalProject = totalproject;
                        }
                        reader.Close();
                    }
                }
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }


        }

        public static List<Photo> GetUserPhotos(string id)
        {
            conn = new SqlConnection(connString);

            try
            {
                List<Photo> photolist = new List<Photo>();

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Photos] WHERE U_ID=@id and publish='1'", conn);
                cmd.Parameters.AddWithValue("@id", id);

                if (cmd.ExecuteScalar() != null)
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Photo photo = new Photo();
                            int ordinal = reader.GetOrdinal("PID");
                            if (!reader.IsDBNull(ordinal))
                                photo.PID = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("U_ID");
                            if (!reader.IsDBNull(ordinal))
                                photo.UserID = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("Rate");
                            if (!reader.IsDBNull(ordinal))
                                photo.Rate = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("Publish");
                            if (!reader.IsDBNull(ordinal))
                                photo.Publish = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("Disc");
                            if (!reader.IsDBNull(ordinal))
                                photo.Desc = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("P_Path");
                            if (!reader.IsDBNull(ordinal))
                                photo.Path = "https://" + reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("T_Like");
                            if (!reader.IsDBNull(ordinal))
                                photo.T_Like = reader.GetInt32(ordinal);
                            ordinal = reader.GetOrdinal("Pr_ID");
                            if (!reader.IsDBNull(ordinal))
                                photo.ProjectID = reader.GetString(ordinal);
                            photolist.Add(photo);
                        }
                    }
                }
                return photolist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static PhotoInfo GetPhotoInfo(string id)
        {
            PhotoInfo photo = new PhotoInfo();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT U_ID,p_path,PID,publish,rate,t_like,fname,sname,imgpath,Name,Photos.Disc FROM PHOTOS,USERS,PROJECTS WHERE PHOTOS.PID=@id AND PHOTOS.U_ID=USERS.ID AND PHOTOS.Pr_ID=PROJECTS.Pro_ID", conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    photo = new PhotoInfo()
                    {
                        UID = dr[0].ToString(),
                        imgUrl = "https://" + dr[1].ToString(),
                        PID = dr[2].ToString(),
                        userFName = dr[6].ToString(),
                        userSName = dr[7].ToString(),
                        userImg = dr[8].ToString(),
                        publish = dr[3].ToString(),
                        rate = dr[4].ToString(),
                        tLike = Convert.ToInt32(dr[5]),
                        projectname = dr[9].ToString(),
                        Disc = dr[10].ToString(),
                    };
                }
                return photo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Project> GetUserProjects(string id)
        {
            List<Project> listproject = new List<Project>();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand getProject = new SqlCommand("SELECT Projects.Pro_ID, Projects.Name, COUNT(Photos.PID) FROM Projects JOIN Photos ON Projects.Pro_ID = Photos.Pr_ID WHERE Projects.User_ID = @id AND publish='1' GROUP BY Projects.Pro_ID, Projects.Name", conn);
                getProject.Parameters.AddWithValue("@id", id);
                if (getProject.ExecuteScalar() != null)
                {
                    SqlDataReader dr = getProject.ExecuteReader();
                    while (dr.Read())
                    {
                        listproject.Add(new Project
                        {
                            ID = dr[0].ToString(),
                            Name = dr[1].ToString(),
                            PhotoCount = Convert.ToInt32(dr[2]),
                        });
                    }
                    dr.Close();
                    foreach (Project pr in listproject)
                    {
                        SqlCommand getSamplePhoto = new SqlCommand("SELECT TOP 1 Photos.P_Path FROM Photos WHERE Photos.Pr_ID=@id", conn);
                        getSamplePhoto.Parameters.AddWithValue("@id", pr.ID);
                        if (getSamplePhoto.ExecuteScalar() != null)
                            pr.SamplePhoto = "https://" + getSamplePhoto.ExecuteScalar().ToString();
                    }
                }
                return listproject;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Photo> GetPhotosByProject(string id)
        {
            List<Photo> listphoto = new List<Photo>();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Photos Where Pr_ID=@id AND publish='1'", conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listphoto.Add(new Photo
                    {
                        PID = dr[0].ToString(),
                        UserID = dr[1].ToString(),
                        Rate = dr[2].ToString(),
                        T_Like = Convert.ToInt32(dr[3]),
                        Path = "https://" + dr[4].ToString(),
                        Publish = dr[5].ToString(),
                        Desc = dr[6].ToString(),
                        ProjectID = dr[7].ToString(),
                    });
                }
                return listphoto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<PhotoInfo> GetLikedPhotos(string id)
        {
            List<PhotoInfo> listphoto = new List<PhotoInfo>();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Photos.P_Path,Users.FName,Users.SName,Users.ImgPath,Users.Url,PID FROM Likes,Users,Photos WHERE Photos.U_ID=@id AND Likes.U_ID=Users.ID AND Likes.P_ID=Photos.PID AND publish='1' ORDER BY L_Time DESC", conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listphoto.Add(new PhotoInfo
                    {
                        imgUrl = "https://" + dr[0].ToString(),
                        userFName = dr[1].ToString(),
                        userSName = dr[2].ToString(),
                        userImg = dr[3].ToString(),
                        Url = dr[4].ToString(),
                        PID = dr[5].ToString(),
                    });
                }
                return listphoto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }


        }

        public static PhotoInfo GetLikedPhoto(PhotoInfo photo, string UID)
        {
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT P_ID FROM Likes WHERE U_ID=@uid AND P_ID=@pid", conn);
                cmd.Parameters.AddWithValue("@uid", UID);
                cmd.Parameters.AddWithValue("@pid", photo.PID);
                if (cmd.ExecuteScalar() != null)
                {
                    photo.Liked = "/Images/like.png";
                }
                SqlTask.conn.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something wrong happened :", ex);
            }

            return photo;
        }

        public static List<User> GetSearchedUser(string text)
        {
            List<User> userlist = new List<User>();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Email LIKE '%" + text + "%' OR FName LIKE '%" + text + "%' OR SName LIKE '%" + text + "%'", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    userlist.Add(new User
                    {
                        ID = dr[0].ToString(),
                        Email = dr[2].ToString(),
                        FName = dr[3].ToString(),
                        SName = dr[4].ToString(),
                        Desc = dr[5].ToString(),
                        ImgPath = dr[7].ToString(),
                        URL = dr[8].ToString(),
                    });
                }
                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<User> GetSearchedUser(string Name = "", string Surname = "")
        {
            List<User> userlist = new List<User>();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE FName LIKE '%" + Name + "%' OR SName LIKE '%" + Surname + "%' OR FName LIKE '%" + Surname + "%' OR SName LIKE '%" + Name + "%'", conn);
                string t = cmd.CommandText;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    userlist.Add(new User
                    {
                        ID = dr[0].ToString(),
                        Email = dr[2].ToString(),
                        FName = dr[3].ToString(),
                        SName = dr[4].ToString(),
                        Desc = dr[5].ToString(),
                        ImgPath = dr[7].ToString(),
                        URL = dr[8].ToString(),
                    });
                }
                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static int getTotalLikeByPhoto(string id)
        {
            conn = new SqlConnection(connString);

            int totallike = 0;
            try
            {
                conn.Open();
                SqlCommand getLike = new SqlCommand("SELECT T_Like FROM [Photos] WHERE PID=@id", conn);
                getLike.Parameters.AddWithValue("@id", id);

                totallike = (int)getLike.ExecuteScalar();
                return totallike;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Comment> GetCommentsByPhotoId(string pid)
        {
           
            List<Comment> commentlist = new List<Comment>();

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Comments.*, ImgPath, FName, SName, Url FROM Comments,Users WHERE Comments.P_ID=@pid AND Users.ID = Comments.U_ID ORDER BY CDate DESC", conn);
                cmd.Parameters.AddWithValue("@pid", pid);
                if (cmd.ExecuteScalar() != null)
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            commentlist.Add(new Comment
                            {
                                PID = dr[0].ToString(),
                                UID = dr[1].ToString(),
                                CText = dr[2].ToString(),
                                CDate = Convert.ToDateTime(dr[3]),
                                UserImg = dr[4].ToString(),
                                UserFName = dr[5].ToString(),
                                UserSName = dr[6].ToString(),
                                UserUrl = dr[7].ToString(),
                            });
                        }
                        dt.Dispose();
                    }
                }
                return commentlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static User GetUserInfoForComment(string uid)
        {
            User user = new User();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT FName,SName,ImgPath FROM Users WHERE ID=@id", conn);
                cmd.Parameters.AddWithValue("@id", uid);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    user = new User()
                    {
                        FName = dr[0].ToString(),
                        SName = dr[1].ToString(),
                        ImgPath = dr[2].ToString(),
                    };
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static int GetTotalComment(string pid)
        {
            conn = new SqlConnection(connString);
            int tComment = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(U_ID) FROM Comments WHERE P_ID=@pid", conn);
                cmd.Parameters.AddWithValue("@pid", pid);
                tComment = (int)cmd.ExecuteScalar();
                return tComment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }






        public static List<Photo> GetPhotographerPhotos(string id)
        {
            List<Photo> photolist = new List<Photo>();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Photos] WHERE U_ID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                if (cmd.ExecuteScalar() != null)
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Photo photo = new Photo();
                            int ordinal = reader.GetOrdinal("PID");
                            if (!reader.IsDBNull(ordinal))
                                photo.PID = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("U_ID");
                            if (!reader.IsDBNull(ordinal))
                                photo.UserID = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("Rate");
                            if (!reader.IsDBNull(ordinal))
                                photo.Rate = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("Publish");
                            if (!reader.IsDBNull(ordinal))
                                photo.Publish = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("Disc");
                            if (!reader.IsDBNull(ordinal))
                                photo.Desc = reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("P_Path");
                            if (!reader.IsDBNull(ordinal))
                                photo.Path = "https://" + reader.GetString(ordinal);
                            ordinal = reader.GetOrdinal("T_Like");
                            if (!reader.IsDBNull(ordinal))
                                photo.T_Like = reader.GetInt32(ordinal);
                            ordinal = reader.GetOrdinal("Pr_ID");
                            if (!reader.IsDBNull(ordinal))
                                photo.ProjectID = reader.GetString(ordinal);
                            photolist.Add(photo);
                        }
                    }
                }
                return photolist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

        public static PhotoInfo UpdatePhotoInfo(string comment, string rate, string publish, string id)
        {
            PhotoInfo photo = new PhotoInfo();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Photos SET Rate=@rate , Disc=@comment , Publish=@publish WHERE PID=@pid ", conn);
                cmd.Parameters.AddWithValue("@comment", comment);
                cmd.Parameters.AddWithValue("@rate", rate);
                cmd.Parameters.AddWithValue("@publish", publish);
                cmd.Parameters.AddWithValue("@pid", id);
                cmd.ExecuteNonQuery();


                cmd = new SqlCommand("SELECT * FROM PHOTOS WHERE PID=@pid ", conn);
                cmd.Parameters.AddWithValue("@pid", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    photo = new PhotoInfo
                    {
                        PID = dr["PID"].ToString(),
                        imgUrl = "http://" + dr["P_Path"].ToString(),
                        UID = dr["U_ID"].ToString(),
                        publish = dr["Publish"].ToString(),
                        rate = dr["Rate"].ToString(),
                        tLike = Convert.ToInt32(dr["T_Like"].ToString()),
                        Disc = dr["Disc"].ToString()
                    };
                }
                return photo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DeletePhotoInfo(string pid)
        {
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE  Details WHERE PID=@pid ", conn);
                cmd.Parameters.AddWithValue("@pid", pid);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DELETE  Comments WHERE P_ID=@pid ", conn);
                cmd.Parameters.AddWithValue("@pid", pid);
                cmd.ExecuteScalar();
                cmd = new SqlCommand("DELETE  Likes WHERE P_ID=@pid ", conn);
                cmd.Parameters.AddWithValue("@pid", pid);
                cmd.ExecuteScalar();
                cmd = new SqlCommand("DELETE  Photos WHERE PID=@pid ", conn);
                cmd.Parameters.AddWithValue("@pid", pid);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Project> GetPhotographerProjects(string id)
        {
            conn = new SqlConnection(connString);
            List<Project> listproject = new List<Project>();
            try
            {
                SqlCommand getProject = new SqlCommand("SELECT Projects.Pro_ID, Projects.Name, COUNT(Photos.PID) FROM Projects JOIN Photos ON Projects.Pro_ID = Photos.Pr_ID WHERE Projects.User_ID = @id GROUP BY Projects.Pro_ID, Projects.Name", conn);
                conn.Open();
                getProject.Parameters.AddWithValue("@id", id);
                if (getProject.ExecuteScalar() != null)
                {
                    SqlDataReader dr = getProject.ExecuteReader();
                    while (dr.Read())
                    {
                        listproject.Add(new Project
                        {
                            ID = dr[0].ToString(),
                            Name = dr[1].ToString(),
                            PhotoCount = Convert.ToInt32(dr[2]),
                        });
                    }
                    dr.Close();
                    foreach (Project pr in listproject)
                    {
                        SqlCommand getSamplePhoto = new SqlCommand("SELECT TOP 1 Photos.P_Path FROM Photos WHERE Photos.Pr_ID=@id", conn);
                        getSamplePhoto.Parameters.AddWithValue("@id", pr.ID);
                        if (getSamplePhoto.ExecuteScalar() != null)
                            pr.SamplePhoto = "https://" + getSamplePhoto.ExecuteScalar().ToString();
                    }
                }
                return listproject;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Photo> GetPhotosByProject_Photographer(string id)
        {
            List<Photo> listphoto = new List<Photo>();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Photos Where Pr_ID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listphoto.Add(new Photo
                    {
                        PID = dr[0].ToString(),
                        UserID = dr[1].ToString(),
                        Rate = dr[2].ToString(),
                        T_Like = Convert.ToInt32(dr[3]),
                        Path = "https://" + dr[4].ToString(),
                        Publish = dr[5].ToString(),
                        Desc = dr[6].ToString(),
                        ProjectID = dr[7].ToString(),
                    });
                }
                return listphoto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<PhotoInfo> GetLikedPhotosForPhotographer(string id)
        {
            List<PhotoInfo> listphoto = new List<PhotoInfo>();
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Photos.P_Path,Users.FName,Users.SName,Users.ImgPath,Users.Url,PID FROM Likes,Users,Photos WHERE Photos.U_ID=@id AND Likes.U_ID=Users.ID AND Likes.P_ID=Photos.PID ORDER BY L_Time DESC", conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listphoto.Add(new PhotoInfo
                    {
                        imgUrl = "https://" + dr[0].ToString(),
                        userFName = dr[1].ToString(),
                        userSName = dr[2].ToString(),
                        userImg = dr[3].ToString(),
                        Url = dr[4].ToString(),
                        PID = dr[5].ToString(),
                    });
                }
                return listphoto;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DeleteProject(string ProjectID)
        {
            List<Photo> listphoto = GetPhotosByProject_Photographer(ProjectID);
            for (int i = 0; i < listphoto.Count; i++)
            {
                DeletePhotoInfo(listphoto[i].PID);
            }
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE Projects WHERE Pro_ID=@pid ", conn);
                cmd.Parameters.AddWithValue("@pid", ProjectID);
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
        }
    }
}