using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Photogasm
{
    public partial class SiteMaster : MasterPage
    {
        public string loginalert = "Enter your e-mail and password";
        public string registeralert = "Enter your registration informations";

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SqlTask.conn.State != System.Data.ConnectionState.Closed) { SqlTask.conn.Close(); }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //berkan sql login
            if (string.IsNullOrEmpty(TxtEmail.Text) == false && string.IsNullOrEmpty(txtPassword.Text) == false)
            {
                try
                {
                    SqlTask.conn = new SqlConnection(SqlTask.connString);
                    User user = new User();
                    SqlTask.conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Users] WHERE Email=@Email and Password=@Password", SqlTask.conn);
                    cmd.Parameters.AddWithValue("@Email", TxtEmail.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    if (cmd.ExecuteScalar() != null)
                    {
                        divloginmsg.Attributes.Add("class", "success");
                        iconloginmsg.Attributes["class"] = "glyphicon glyphicon-ok success";
                        textloginmsg.InnerText = "Login Successfull";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int ordinal = reader.GetOrdinal("ID");
                                if (!reader.IsDBNull(ordinal))
                                {
                                    user.ID = reader.GetString(ordinal);
                                    Session["loginuser"] = user.ID;
                                }
                                ordinal = reader.GetOrdinal("FName");
                                if (!reader.IsDBNull(ordinal))
                                    user.FName = reader.GetString(ordinal);
                                ordinal = reader.GetOrdinal("SName");
                                if (!reader.IsDBNull(ordinal))
                                    user.SName = reader.GetString(ordinal);
                                Session["menuUsername"] = user.FName + " " + user.SName;

                            }
                        }
                        Response.Redirect("/Default.aspx");
                    }
                    else
                    {
                        divloginmsg.Attributes.Add("class", "error");
                        iconloginmsg.Attributes["class"] = "glyphicon glyphicon-remove error";
                        textloginmsg.InnerText = "E-mail or Password wrong.";
                        btnLogin.Attributes.Remove("disabled");
                    }
                }
                catch (Exception ex)
                {
                    divloginmsg.Attributes.Add("class", "error");
                    iconloginmsg.Attributes["class"] = "glyphicon glyphicon-remove error";
                    textloginmsg.InnerText = "Error :" + ex.Message;
                    btnLogin.Attributes.Remove("disabled");
                }
                finally
                {
                    SqlTask.conn.Close();
                }
            }
            else
            {
                textloginmsg.InnerText = "Email and password field cannot be empty.";
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (txtRegisterPassword.Text.Length < 6)
                {
                    divregistermsg.Attributes.Add("class", "error");
                    iconregistermsg.Attributes["class"] = "glyphicon glyphicon-remove error";
                    textregistermsg.InnerText = "Password must be at least 6 character";
                    btnRegister.Attributes.Remove("disabled");
                    return;
                }
                User user = new User();
                Random r1 = new Random();
                int num = r1.Next(0, 99999999);
                string number = string.Format("{0:D8}", num);
                Random r2 = new Random();
                int num2 = r2.Next(0, 26);
                char letter = (char)('A' + num2);

                user.ID = letter + number; 
                user.FName = txtRegisterName.Text;
                user.SName = txtRegisterSurname.Text;
                user.Password = txtRegisterPassword.Text;
                user.Email = txtRegisterEmail.Text;
                user.URL = user.ID;
                user.ImgPath = "/User/Avatar/default_user2.jpg";

                try
                {
                    SqlTask.conn = new SqlConnection(SqlTask.connString);
                    SqlTask.conn.Open();
                    SqlCommand cmdcheck = new SqlCommand("SELECT * FROM [Users] WHERE Email=@Email OR ID=@id", SqlTask.conn);
                    cmdcheck.Parameters.AddWithValue("@Email", user.Email.Trim());
                    cmdcheck.Parameters.AddWithValue("@id", user.ID.Trim());
                    if (cmdcheck.ExecuteScalar() != null)
                    {
                        divregistermsg.Attributes.Add("class", "error");
                        iconregistermsg.Attributes["class"] = "glyphicon glyphicon-remove error";
                        textregistermsg.InnerText = "This E-mail Already Registered";
                    }
                    else
                    {
                        try
                        {
                            Random rnd = new Random();
                            SqlCommand cmdregister = new SqlCommand("INSERT INTO Users([ID],[Email],[FName],[SName],[Password],[Online],[ImgPath],[Url]) VALUES (@ID,@Email,@FName,@SName,@Password,@Online,@ImgPath,@Url)", SqlTask.conn);
                            cmdregister.Parameters.AddWithValue("@ID", user.ID);
                            cmdregister.Parameters.AddWithValue("@Email", user.Email.Trim());
                            cmdregister.Parameters.AddWithValue("@Password", user.Password.Trim());
                            cmdregister.Parameters.AddWithValue("@FName", user.FName.Trim());
                            cmdregister.Parameters.AddWithValue("@SName", user.SName.Trim());
                            cmdregister.Parameters.AddWithValue("@Online", user.Online);
                            cmdregister.Parameters.AddWithValue("@ImgPath", user.ImgPath.Trim());
                            cmdregister.Parameters.AddWithValue("@Url", user.URL.Trim());

                            if (cmdregister.ExecuteNonQuery() > 0)
                            {
                                divregistermsg.Attributes.Add("class", "success");
                                iconregistermsg.Attributes["class"] = "glyphicon glyphicon-ok success";
                                textregistermsg.InnerText = "Registration Successfull";

                                Session["loginuser"] = user.ID;
                                Session["menuUsername"] = user.FName + " " + user.SName;
                                Response.Redirect("/Default.aspx");
                            }
                        }
                        catch (Exception ex)
                        {
                            divregistermsg.Attributes.Add("class", "error");
                            iconregistermsg.Attributes["class"] = "glyphicon glyphicon-remove error";
                            textregistermsg.InnerText = "Error:" + ex.Message;
                        }
                    }
                }
                catch (Exception ex)
                {
                    divregistermsg.Attributes.Add("class", "error");
                    iconregistermsg.Attributes["class"] = "glyphicon glyphicon-remove error";
                    textregistermsg.InnerText = "Error:" + ex.Message;
                }
                finally
                {
                    SqlTask.conn.Close();
                }
            }
        }

        public void btnFBLogin_Click(object sender, EventArgs e)
        {
            FacebookConnection face = new FacebookConnection();
            //Facebook Login
            var Link = face.CreateLoginUrl().ToString();
            Response.Redirect(Link);


        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("/search.aspx?s="+txtSearch.Value);
        }

    }
}