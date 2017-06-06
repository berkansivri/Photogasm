using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Photogasm
{
    public partial class MyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SqlTask.conn.State == System.Data.ConnectionState.Open || SqlTask.conn.State == System.Data.ConnectionState.Connecting)
            { SqlTask.conn.Close(); }
            if (!IsPostBack)
            {
                User user = new User();
                if (this.Page.RouteData.Values["userUrl"] != null)
                {
                    string userid = SqlTask.GetUserIdByUrl(this.Page.RouteData.Values["userUrl"].ToString());
                    if (string.IsNullOrEmpty(userid)) { Response.Redirect("/Error.aspx"); }

                    user = SqlTask.GetUserInfo(userid);
                    lblName.Text = user.FName;
                    imgUser.ImageUrl = user.ImgPath;
                    lblDesc.Text = user.Desc;
                    lblEmail.Text = user.Email;
                    lblLikes.Text = user.totalLike.ToString();
                    lblPhotos.Text = user.totalPhoto.ToString();
                    lblProjects.Text = user.totalProject.ToString();
                    if (Session["loginuser"] == null || Session["loginuser"].ToString() != user.ID)
                        btnUpdate.Visible = false;

                }
                else if (Session["loginuser"] != null)
                {
                    user = SqlTask.GetUserInfo(Session["loginuser"].ToString());
                    lblName.Text = user.FName;
                    imgUser.ImageUrl = user.ImgPath;
                    lblDesc.Text = user.Desc;
                    lblEmail.Text = user.Email;
                    lblLikes.Text = user.totalLike.ToString();
                    lblPhotos.Text = user.totalPhoto.ToString();
                    lblProjects.Text = user.totalProject.ToString();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
                //---get photos---
                List<Photo> photolist = SqlTask.GetUserPhotos(user.ID);
                lstPhoto.DataSource = photolist;
                lstPhoto.DataBind();

                List<Project> projectlist = SqlTask.GetUserProjects(user.ID);
                lstProject.DataSource = projectlist;
                lstProject.DataBind();

                List<PhotoInfo> likedlist = SqlTask.GetLikedPhotos(user.ID);
                lstLikes.DataSource = likedlist;
                lstLikes.DataBind();

            }
        }

        protected void imgPhoto_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton b = sender as ImageButton;
            string Id = b.Attributes["imgid"];

            PhotoInfo photo = SqlTask.GetPhotoInfo(Id);
            switch (photo.rate)
            {
                case "1":
                    s1.ImageUrl = "/Images/cstar.png";
                    break;
                case "2":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    break;
                case "3":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    s3.ImageUrl = "/Images/cstar.png";
                    break;
                case "4":
                    s1.ImageUrl = "/Images/cstar.png";
                    s2.ImageUrl = "/Images/cstar.png";
                    s3.ImageUrl = "/Images/cstar.png";
                    s4.ImageUrl = "/Images/cstar.png";
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
            lblLikeCount.Text = photo.tLike.ToString();
            lblPhotoDesc.Text = photo.Disc;
            btnLike.Attributes.Add("imgId", photo.PID);
            btnExifPopUp.CommandName = photo.PID;

            if (Session["loginuser"] != null)
            {
                photo = SqlTask.GetLikedPhoto(photo, Session["loginuser"].ToString());
            }
            btnLike.ImageUrl = photo.Liked;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //uptImgModal.Update();
        }

        protected void imgProject_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton b = sender as ImageButton;
            string projectId = b.Attributes["projectId"];
            string projectName = b.Attributes["projectName"];
            lblProjectName.Text = projectName;
            lblProjectName.Visible = true;
            List<Photo> listphoto = SqlTask.GetPhotosByProject(projectId);
            string userid = listphoto.ElementAt(0).UserID;
            btnBackAllPhoto.PostBackUrl = "http://teambro.azurewebsites.net/" + SqlTask.GetUserUrlById(listphoto.ElementAt(0).UserID);
            btnBackAllPhoto.Visible = true;
            lstPhoto.DataSource = listphoto;
            lstPhoto.DataBind();
        }

        protected void btnLike_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["loginuser"] == null) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Poppp", "openLogin();", true); }
            else
            {
                var x = sender as ImageButton;
                string UID = Session["loginuser"].ToString();
                string PID = x.Attributes["imgId"];
                string liked = x.ImageUrl;
                int likes = Convert.ToInt32(lblLikeCount.Text);
                if (liked == "/Images/unlike.png")
                {
                    if (LikeClass.sendLike(UID, PID) == true)
                    {
                        x.ImageUrl = "/Images/like.png";
                        likes += 1;
                    }
                }
                else if (liked == "/Images/like.png")
                {
                    if (LikeClass.UnLike(UID, PID) == true)
                    {
                        x.ImageUrl = "/Images/unlike.png";
                        likes -= 1;
                    }
                }
                lblLikeCount.Text = likes.ToString();
            }
        }

        protected void btnExifPopUp_Click(object sender, ImageClickEventArgs e)
        {
            var x = sender as ImageButton;
            exifCssID.Attributes.CssStyle.Add("visibility", "visible");
            exifCssID.Attributes.CssStyle.Add("transform", "translate(-50%, -50%) scale(1)");

            //exifCssExitID.Attributes.CssStyle.Add("visibility", "visible");
            //exifCssExitID.Attributes.CssStyle.Add("transform", "scale(1)");
            var PID = x.CommandName;
            List<ExifDetails> y = ExifClass.getPhotosExif(PID);
            foreach (var item in y)
            {
                lblaperture_value.Text = item.A_Value;
                lblcamera_Model.Text = item.Camera;
                lblfocal_Length.Text = item.Focal_Rate;
                lbliso_Speed.Text = item.ISO;
                lblshutter_Speed.Text = item.S_Value;
                lblhorizontal_Resolution.Text = item.H_Resolution;
                lblverticalResolution.Text = item.V_Resolution;
                lblColorSpace.Text = item.Color_Space;
                lbltaken_Date.Text = item.P_Date;
                lblbitsPerPixel.Text = item.Bits_Per_Pixel;
                lblimageSize.Text = item.Image_Size;
            }
        }

        protected void btnCloseExif_Click(object sender, ImageClickEventArgs e)
        {
            exifCssID.Attributes.CssStyle.Add("visibility", "hidden");
            exifCssID.Attributes.CssStyle.Add("transform", "translate(-50%, -50%) scale(0)");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Poppp", "openModal();", true);
            //exifCssExitID.Attributes.CssStyle.Add("visibility", "hidden");
            //exifCssExitID.Attributes.CssStyle.Add("transform", "scale(0)");
        }
    }
}
