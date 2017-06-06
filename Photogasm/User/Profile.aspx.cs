using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;

namespace Photogasm
{
    public partial class Profile : System.Web.UI.Page
    {
        public string alertprofile = "Your profile details.";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SqlTask.conn.Close();
                if (Session["loginuser"] != null)
                {
                    try
                    {
                        User user = SqlTask.GetUserInfo(Session["loginuser"].ToString());

                        txtFName.Text = user.FName;
                        txtSName.Text = user.SName;
                        txtProfileEmail.Text = user.Email;
                        txtDesc.Text = user.Desc;
                        imgUser.ImageUrl = user.ImgPath;
                        txtID.Text = user.ID;
                        txtURL.Text = user.URL;

                        if (char.IsDigit(user.ID[0]) == true)
                        {
                            chgPass.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        alertprofilepage.Attributes["class"] = "alert alert-warning alert-dismissable";
                        alertprofile = "The following error is occured: " + ex.Message;
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }  
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlTask.conn = new SqlConnection(SqlTask.connString);
            if (Session["loginuser"] == null) { return; }
            User user = new User()
            {
                ID = txtID.Text,
                FName = txtFName.Text,
                SName = txtSName.Text,
                Desc = txtDesc.Text,
                Email = txtProfileEmail.Text,
                ImgPath = imgUser.ImageUrl,
            };
            string url = txtURL.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(url) == false)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(url, "^[a-zA-Z0-9]*$") == false)
                {
                    alertprofilepage.Attributes["class"] = "alert alert-danger alert-dismissable";
                    alertprofile = "URL accepts only *alphanumeric* characters";
                    txtURL.Text.Remove(txtURL.Text.Length - 1);
                    return;
                }
                else
                {
                    SqlTask.conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ID,Url FROM Users WHERE Url=@url", SqlTask.conn);
                    cmd.Parameters.AddWithValue("@url", url);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr[1] != null && dr[0].ToString() != user.ID)
                        {
                            alertprofilepage.Attributes["class"] = "alert alert-danger alert-dismissable";
                            alertprofile = "This URL is used. Please change your URL";
                            return;
                        }
                        else
                        {
                            user.URL = url;
                        }
                    }
                    else { user.URL = url; }
                    SqlTask.conn.Close();
                }
            }
            else { user.URL = user.ID; }

            if (this.imgUploadPath.HasFile)
            {
                if (this.imgUploadPath.PostedFile.ContentLength <= 3000000)
                {
                    imgUploadPath.SaveAs(Server.MapPath("/User/Avatar/" + user.ID + "_" + this.imgUploadPath.FileName));
                    string imgName = Path.GetFileName(imgUploadPath.PostedFile.FileName);
                    imgUser.ImageUrl = "/User/Avatar/" + user.ID + "_" + imgName;
                    user.ImgPath = imgUser.ImageUrl;
                }
                else
                {
                    alertprofilepage.Attributes["class"] = "alert alert-warning alert-dismissable";
                    alertprofile = "Uploaded image exceed max size!";
                    return;
                }
            }
            try
            {
                SqlTask.conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Users SET FName=@fname, SName=@sname, Disc=@desc, ImgPath=@imgpath, Url=@url WHERE Id=@id", SqlTask.conn);
                cmd.Parameters.AddWithValue("@fname", user.FName.Trim());
                cmd.Parameters.AddWithValue("@sname", user.SName.Trim());
                cmd.Parameters.AddWithValue("@desc", user.Desc.Trim());
                cmd.Parameters.AddWithValue("@imgpath", user.ImgPath.Trim());
                cmd.Parameters.AddWithValue("@url", user.URL);
                cmd.Parameters.AddWithValue("@id", Session["loginuser"].ToString());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    alertprofilepage.Attributes["class"] = "alert alert-success alert-dismissable";
                    alertprofile = "Changes successfully updated.";
                    Session["menuUsername"] = user.FName + " " + user.SName;
                }
                else
                {
                    alertprofilepage.Attributes["class"] = "alert alert-warning alert-dismissable";
                    alertprofile = "An error is occured. Try again later.";
                } 
            }
            catch (Exception ex)
            {
                alertprofilepage.Attributes["class"] = "alert alert-warning alert-dismissable";
                alertprofile = "The following error is occured: " + ex.Message;
            }
            finally
            {
                SqlTask.conn.Close();
            }
            return;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/User/Profile.aspx");
        }

        protected void chgPass_Click(object sender, EventArgs e)
        {
            if (Session["loginuser"] != null) { Response.Redirect("/User/ChangePassword.aspx"); } else { Response.Redirect("/Default.aspx"); }
        }

    }
}