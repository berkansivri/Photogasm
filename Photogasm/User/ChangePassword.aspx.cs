using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Photogasm
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string alertchangepassword = "Please fill the blanks.";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginuser"] != null)
            {
                try
                {
                    User user = SqlTask.GetUserInfo(Session["loginuser"].ToString());

                    txtProfileEmail.Text = user.Email;
                    imgUser.ImageUrl = user.ImgPath;
                    txtID.Text = user.ID;
                }
                catch (Exception ex)
                {
                    alertprofilepage.Attributes["class"] = "alert alert-warning alert-dismissable";
                    alertchangepassword = "The following error is occured: " + ex.Message;
                }
            }
            else
            {
                Response.Redirect("/Default.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/User/Profile.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["loginuser"] == null) { return; }
            if (string.IsNullOrEmpty(txtCurrentPassword.Text) == true)
            {
                alertprofilepage.Attributes["class"] = "alert alert-danger alert-dismissable";
                alertchangepassword = "You have to enter your current password";
                return;
            }
            else
            {
                SqlTask.conn = new SqlConnection(SqlTask.connString);
                User user = new User()
                {
                    ID = txtID.Text,
                    Password = txtCurrentPassword.Text,
                };
                try
                {
                    SqlTask.conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Password FROM Users WHERE ID=@id", SqlTask.conn);
                    cmd.Parameters.AddWithValue("@id", user.ID);
                    string pass = cmd.ExecuteScalar().ToString();
                    if (pass != user.Password)
                    {
                        alertprofilepage.Attributes["class"] = "alert alert-danger alert-dismissable";
                        alertchangepassword = "You entered wrong current password.";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtNewPassword.Text) == false)
                        {
                            try
                            {
                                SqlCommand uptpass = new SqlCommand("UPDATE Users SET Password=@pass WHERE ID=@id", SqlTask.conn);
                                uptpass.Parameters.AddWithValue("@id", user.ID);
                                uptpass.Parameters.AddWithValue("@pass", txtNewPassword.Text.Trim());
                                if (uptpass.ExecuteNonQuery() > 0)
                                {
                                    alertprofilepage.Attributes["class"] = "alert alert-success alert-dismissable";
                                    alertchangepassword = "Your password successfully updated";
                                }
                            }
                            catch (Exception ex)
                            {
                                alertprofilepage.Attributes["class"] = "alert alert-warning alert-dismissable";
                                alertchangepassword = "The following error is occured :" + ex;
                                throw;
                            }

                        }
                        else
                        {
                            alertprofilepage.Attributes["class"] = "alert alert-info alert-dismissable";
                            alertchangepassword = "Your password not changed.";
                        }
                    }
                }
                catch (Exception ex)
                {

                    alertprofilepage.Attributes["class"] = "alert alert-warning alert-dismissable";
                    alertchangepassword = "An error is occured. Try again later." + ex;
                }
                finally
                {
                    SqlTask.conn.Close();
                }
                txtConfirmNewPassword.Text = "";
            }
        }
    }
}