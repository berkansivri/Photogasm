using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace Photogasm
{
    public partial class FbLogin : System.Web.UI.Page
    {
        public static dynamic facetoken = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["code"] != null)
            {
                User fbUser = new User();
                FacebookConnection face = new FacebookConnection();
                string code = Request.QueryString["code"];
                string state = "";
                string type = "";
                dynamic token = face.GetAccessToken(code, state, type);
                fbUser = face.GetUserInfo(token);
                //fbUser.Online = 1;

                //insert to database
                string alertlogin = string.Empty;
                try
                {
                    SqlTask.conn = new SqlConnection(SqlTask.connString);
                    SqlTask.conn.Open();
                    SqlCommand cmdcheck = new SqlCommand("SELECT * FROM [Users] WHERE Email=@email", SqlTask.conn);
                    cmdcheck.Parameters.AddWithValue("@email", fbUser.Email.Trim());
                    if (cmdcheck.ExecuteScalar() != null)
                    {
                        alertlogin = "Login successfull.";
                        Session.Add("loginuser", fbUser.ID);
                        Session.Add("menuUsername", fbUser.FName + " " + fbUser.SName);
                        facetoken = token;
                    }
                    else
                    {
                        try
                        {
                            Random rnd = new Random();
                            SqlCommand cmdregister = new SqlCommand("INSERT INTO Users([ID],[Email],[FName],[SName],[Password],[Online], [ImgPath], [Url]) VALUES (@ID,@Email,@FName,@SName,@Password,@Online,@ImgPath,@Url)", SqlTask.conn);
                            cmdregister.Parameters.AddWithValue("@ID", fbUser.ID.Trim());
                            cmdregister.Parameters.AddWithValue("@Email", fbUser.Email.Trim());
                            cmdregister.Parameters.AddWithValue("@Password", getHash(fbUser.ID));
                            string hash = getHash(fbUser.ID);
                            cmdregister.Parameters.AddWithValue("@FName", fbUser.FName.Trim());
                            cmdregister.Parameters.AddWithValue("@SName", fbUser.SName.Trim());
                            cmdregister.Parameters.AddWithValue("@Online", fbUser.Online);
                            cmdregister.Parameters.AddWithValue("@ImgPath", fbUser.ImgPath.Trim());
                            cmdregister.Parameters.AddWithValue("@Url", fbUser.ID.Trim());

                            if (cmdregister.ExecuteNonQuery() > 0)
                            {
                                alertlogin = "Registration Successfull";
                                Session.Add("loginuser", fbUser.ID);
                                Session.Add("menuUsername", fbUser.FName + " " + fbUser.SName);
                                facetoken = token;
                            }
                        }
                        catch (Exception ex)
                        {
                            alertlogin = "Registration Failed. - " + ex.Message;
                        }
                        finally
                        {
                            SqlTask.conn.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    alertlogin = "Failed. - " + ex.Message;
                }
            }
            Response.Redirect("/");
        }

        private static string getHash(string text)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hashedbytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashedbytes).Replace("-", "").ToLower();
            }
        }
    }
}