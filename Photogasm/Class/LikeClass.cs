using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Photogasm
{
    public class LikeClass
    {
        public string UID { get; set; }
        public string PID { get; set; }
        public DateTime Date { get; set; }

        public static bool sendLike(string UID,string PID)
        {
            SqlTask.conn = new SqlConnection(SqlTask.connString);
            try
            {
                SqlTask.conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Likes VALUES (@pid,@uid,@date)", SqlTask.conn);
                cmd.Parameters.AddWithValue("@uid", UID);
                cmd.Parameters.AddWithValue("@pid", PID);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);

                SqlCommand cmd2 = new SqlCommand("UPDATE Photos SET T_Like=T_Like+1 WHERE PID=@pid", SqlTask.conn);
                cmd2.Parameters.AddWithValue("@pid", PID);
                if (cmd.ExecuteNonQuery() > 0 && cmd2.ExecuteNonQuery() > 0)
                {
                    SqlTask.conn.Close();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something wrong happened :", ex);
            }
            finally
            {
                SqlTask.conn.Close();
            }

        }
        public static bool UnLike(string UID, string PID)
        {
            SqlTask.conn = new SqlConnection(SqlTask.connString);
            try
            {
                SqlTask.conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Likes WHERE P_ID=@pid AND U_ID=@uid", SqlTask.conn);
                cmd.Parameters.AddWithValue("@pid", PID);
                cmd.Parameters.AddWithValue("@uid", UID);
                SqlCommand cmd2 = new SqlCommand("UPDATE Photos SET T_Like=T_Like-1 WHERE PID=@pid", SqlTask.conn);
                cmd2.Parameters.AddWithValue("@pid", PID);
                if (cmd.ExecuteNonQuery() > 0 && cmd2.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something wrong happened :", ex);
            }         
            finally
            {
                SqlTask.conn.Close();
            }
        }
        public static List<PhotoInfo> getLiked(List<PhotoInfo> LikedPhotoList, string UID)
        {
            SqlTask.conn = new SqlConnection(SqlTask.connString);
            try
            {
                SqlTask.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT P_ID FROM Likes WHERE U_ID=@uid", SqlTask.conn);
                cmd.Parameters.AddWithValue("@uid", UID);
                SqlDataReader dr = cmd.ExecuteReader();
                LikedPhotoList.ToList().ForEach(x => x.UID = UID);
               while (dr.Read())
                {
                    if(LikedPhotoList.Find(x => x.PID == dr[0].ToString())!=null)
                    LikedPhotoList.Find(x => x.PID == dr[0].ToString()).Liked = "/Images/like.png";
                }
                return LikedPhotoList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something wrong happened :", ex);
            }
            finally
            {
                SqlTask.conn.Close();
            }
            
            
        }
    }
}