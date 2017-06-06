using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Photogasm
{
    public partial class MyPage3 : System.Web.UI.Page
    {
  

        protected void Page_Load(object sender, EventArgs e)
        {

            if (SqlTask.conn.State != System.Data.ConnectionState.Closed)
            { SqlTask.conn.Close(); }
            if (!IsPostBack)
            {
                User user = new User();
                if (Session["loginuser"] != null)
                {
                    user = SqlTask.GetUserInfo(Session["loginuser"].ToString());

                    lblLikes.Text = user.totalLike.ToString();
                    lblPhotos.Text = user.totalPhoto.ToString();
                    lblProjects.Text = user.totalProject.ToString();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
                //---get photos---
                List<Photo> photolist = SqlTask.GetPhotographerPhotos(user.ID);
                lstPhoto.DataSource = photolist;
                lstPhoto.DataBind();

                List<Project> projectlist = SqlTask.GetPhotographerProjects(user.ID);
                lstProject.DataSource = projectlist;
                lstProject.DataBind();

                List<PhotoInfo> likedlist = SqlTask.GetLikedPhotosForPhotographer(user.ID);
                lstLikes.DataSource = likedlist;
                lstLikes.DataBind();

            }
        }

        protected void imgProject_Click(object sender, ImageClickEventArgs e)
        {

            ImageButton b = sender as ImageButton;
           
            string projectId = b.Attributes["projectId"];
            lblprojectID.Text = projectId;
            string projectName = b.Attributes["projectName"];
            lblProjectName.Text = projectName;
            lblProjectName.Visible = true;
            List<Photo> listphoto = SqlTask.GetPhotosByProject_Photographer(projectId);
            lstPhoto.DataSource = listphoto;
            //deneme.Text = listphoto.FirstOrDefault().PID;
            lstPhoto.DataBind();
        }

        string photoID = "";
        protected void imgPhoto_Click(object sender, ImageClickEventArgs e)
        {
            
            txt_comment_edit.Visible = true;
            ImageButton b = sender as ImageButton;

            if (b.Attributes != null)
            {
                photoID = b.Attributes["imgID"];
                deneme.Text = photoID.ToString();



                PhotoInfo photo = SqlTask.GetPhotoInfo(photoID);
                switch (photo.rate)
                {
                    case "1":
                        s1.ImageUrl = "/Images/cstar.png";
                        s2.ImageUrl = "/Images/ucstar.png";
                        s3.ImageUrl = "/Images/ucstar.png";
                        s4.ImageUrl = "/Images/ucstar.png";
                        s5.ImageUrl = "/Images/ucstar.png";
                        break;
                    case "2":
                        s1.ImageUrl = "/Images/cstar.png";
                        s2.ImageUrl = "/Images/cstar.png";
                        s3.ImageUrl = "/Images/ucstar.png";
                        s4.ImageUrl = "/Images/ucstar.png";
                        s5.ImageUrl = "/Images/ucstar.png";
                        break;
                    case "3":
                        s1.ImageUrl = "/Images/cstar.png";
                        s2.ImageUrl = "/Images/cstar.png";
                        s3.ImageUrl = "/Images/cstar.png";
                        s4.ImageUrl = "/Images/ucstar.png";
                        s5.ImageUrl = "/Images/ucstar.png";
                        break;
                    case "4":
                        s1.ImageUrl = "/Images/cstar.png";
                        s2.ImageUrl = "/Images/cstar.png";
                        s3.ImageUrl = "/Images/cstar.png";
                        s4.ImageUrl = "/Images/cstar.png";
                        s5.ImageUrl = "/Images/ucstar.png";
                        break;
                    case "5":
                        s1.ImageUrl = "/Images/cstar.png";
                        s2.ImageUrl = "/Images/cstar.png";
                        s3.ImageUrl = "/Images/cstar.png";
                        s4.ImageUrl = "/Images/cstar.png";
                        s5.ImageUrl = "/Images/cstar.png";
                        break;
                    default:
                        break;
                }

                deneme2.Text = photo.rate.ToString();
                imgW.ImageUrl = photo.imgUrl;
                userImg.ImageUrl = photo.userImg;
                phtProjectName.Text = photo.projectname;
                phtNameSurname.Text = photo.userFName + " " + photo.userSName;
                lblRate.Text = photo.rate;
                lbl_comment.Text = photo.Disc;
                if (photo.publish == "1")
                    Chck_publish.Checked = true;
                else
                    Chck_publish.Checked = false;

                txt_comment_edit.Visible = true;
                txt_comment_edit.Text = photo.Disc;
                lbl_comment.Visible = false;

                if (Session["loginuser"] != null)
                {
                    photo = SqlTask.GetLikedPhoto(photo, Session["loginuser"].ToString());
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                uptImgModal.Update();
            }
        }


        /* protected void AddFoto(object sender, EventArgs e)
         {
             var x = sender as Button;
             var Pro_ID = x.CommandName;
         }
         */
   
        


        protected void imgPhotoEdit_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton b = sender as ImageButton;
            var Id = b.Attributes["imgid"];

            
            string com = txt_comment_edit.Text;
            string pub;
            string rate;

            var ra = b.ID;

            if (ra.Contains("1"))
                rate = "1";
            else if (ra.Contains("2"))
                rate = "2";
            else if (ra.Contains("3"))
                rate = "3";
            else if (ra.Contains("4"))
                rate = "4";
            else
                rate = "5";

            if (Chck_publish.Checked)
                pub = "1";
            else
                pub = "0";

            Id = deneme.Text;
            PhotoInfo photo = SqlTask.UpdatePhotoInfo(com, rate, pub, Id);
            switch (rate)
            {
                case "1":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/ucstar.png";
                    s3.ImageUrl = "/Images/ucstar.png";
                    s4.ImageUrl = "/Images/ucstar.png";
                    s5.ImageUrl = "/Images/ucstar.png";
                    break;
                case "2":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    s3.ImageUrl = "/Images/ucstar.png";
                    s4.ImageUrl = "/Images/ucstar.png";
                    s5.ImageUrl = "/Images/ucstar.png";
                    break;
                case "3":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    s3.ImageUrl = "/Images/cstar.png";
                    s4.ImageUrl = "/Images/ucstar.png";
                    s5.ImageUrl = "/Images/ucstar.png";
                    break;
                case "4":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    s3.ImageUrl = "/Images/cstar.png";
                    s4.ImageUrl = "/Images/cstar.png";
                    s5.ImageUrl = "/Images/ucstar.png";
                    break;
                case "5":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    s3.ImageUrl = "/Images/cstar.png";
                    s4.ImageUrl = "/Images/cstar.png";
                    s5.ImageUrl = "/Images/cstar.png";
                    break;
                default:
                    break;
            }

            deneme2.Text = rate.ToString();
            imgW.ImageUrl = photo.imgUrl;
            userImg.ImageUrl = photo.userImg;
            phtProjectName.Text = photo.projectname;
            phtNameSurname.Text = photo.userFName + " " + photo.userSName;
            lblRate.Text = photo.rate;
            lbl_comment.Text = photo.Disc;
            if (photo.publish == "1")
                Chck_publish.Checked = true;
            else
                Chck_publish.Checked = false;

            txt_comment_edit.Visible = true;
            txt_comment_edit.Text = photo.Disc;
            lbl_comment.Visible = false;


            if (Session["loginuser"] != null)
            {
                //photo = SqlTask.GetLikedPhoto(photo, Session["loginuser"].ToString());
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }

        protected void editok_Click(object sender, EventArgs e)
        {
             string Id = deneme.Text;
            string com = txt_comment_edit.Text;
            string pub;
            string rate;

            var ra = deneme2.Text;

            if (ra.Contains("1"))
                rate = "1";
            else if (ra.Contains("2"))
                rate = "2";
            else if (ra.Contains("3"))
                rate = "3";
            else if (ra.Contains("4"))
                rate = "4";
            else
                rate = "5";

            if (Chck_publish.Checked)
                pub = "1";
            else
                pub = "0";

             
            PhotoInfo photo = SqlTask.UpdatePhotoInfo(com, rate, pub, Id);
            switch (rate)
            {
                case "1":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/ucstar.png";
                    s3.ImageUrl = "/Images/ucstar.png";
                    s4.ImageUrl = "/Images/ucstar.png";
                    s5.ImageUrl = "/Images/ucstar.png";
                    break;
                case "2":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    s3.ImageUrl = "/Images/ucstar.png";
                    s4.ImageUrl = "/Images/ucstar.png";
                    s5.ImageUrl = "/Images/ucstar.png";
                    break;
                case "3":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    s3.ImageUrl = "/Images/cstar.png";
                    s4.ImageUrl = "/Images/ucstar.png";
                    s5.ImageUrl = "/Images/ucstar.png";
                    break;
                case "4":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    s3.ImageUrl = "/Images/cstar.png";
                    s4.ImageUrl = "/Images/cstar.png";
                    s5.ImageUrl = "/Images/ucstar.png";
                    break;
                case "5":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    s3.ImageUrl = "/Images/cstar.png";
                    s4.ImageUrl = "/Images/cstar.png";
                    s5.ImageUrl = "/Images/cstar.png";
                    break;
                default:
                    break;
            }
            imgW.ImageUrl = photo.imgUrl;
            userImg.ImageUrl = photo.userImg;
            phtProjectName.Text = photo.projectname;
            phtNameSurname.Text = photo.userFName + " " + photo.userSName;
            lblRate.Text = photo.rate;
            lbl_comment.Text = photo.Disc;
            if (photo.publish == "1")
                Chck_publish.Checked = true;
            else
                Chck_publish.Checked = false;

            txt_comment_edit.Visible = true;
            txt_comment_edit.Text = photo.Disc;
            lbl_comment.Visible = false;
            
            Response.Redirect("/MyPage.aspx");

        }

        protected void Deletee_Click(object sender, EventArgs e)
        {
            string Id = deneme.Text;
            SqlTask.DeletePhotoInfo(Id);
            deneme.Text = "";
            deneme2.Text = "";
            Response.Redirect("/MyPage.aspx");
        }

        protected void DeleteProject_Click(object sender, EventArgs e)
        {
            SqlTask.DeleteProject(lblprojectID.Text);
            Response.Redirect("/MyPage.aspx");
        }
    }
}