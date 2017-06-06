using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace Photogasm
{
    public partial class _Default : Page
    {
        //List<Photos> _UsersPhotos;
        //List<User> _UserList;
        List<PhotoInfo> _listViewBind;
        List<Comments> _allComments = new List<Comments>();
        public string YourScript = "";
        SqlCommand cmd;
        SqlDataReader dr;
        SqlConnection conn = new SqlConnection("Data Source=teambro.database.windows.net;Initial Catalog=PhotochArt_db;Integrated Security=False;User ID=n00ne;Password=123Qwe123;Connect Timeout=90;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private string WebUrlGetAllComments = "http://teambrowebapi.azurewebsites.net/comments/getcomments/";
        [WebMethod]
        void mainItem()
        {
        
            using (WebClient webClient = new WebClient())
            {
                 _allComments =  JsonConvert.DeserializeObject<List<Comments>>(webClient.DownloadString(WebUrlGetAllComments));
            }
       

            _listViewBind = new List<PhotoInfo>();
            try
            {
                if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); }
                conn.Open();
                cmd = new SqlCommand("SELECT TOP 20 U_ID,p_path,publish,rate,t_like,fname,sname,imgpath,PID,Url,Photos.Disc,Projects.Name FROM PHOTOS,USERS,PROJECTS WHERE U_ID=ID AND publish='1' AND Projects.Pro_ID=Photos.Pr_ID ORDER BY t_like DESC",conn);
                dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    _listViewBind.Add(new PhotoInfo()
                    {
                        UID = dr[0].ToString(),
                        imgUrl = "https://" + dr[1].ToString(),
                        userFName = dr[5].ToString(),
                        userSName = dr[6].ToString(),
                        userImg = dr[7].ToString(),
                        publish = dr[2].ToString(),
                        rate = dr[3].ToString(),
                        tLike = Convert.ToInt32(dr[4]),
                        PID = dr[8].ToString(),
                        Url = dr[9].ToString(),
                        Disc = dr[10].ToString(),
                        projectname = dr[11].ToString(),
                    });
                }
                conn.Close();

                if (Session["loginuser"] != null)
                {
                    try
                    {
                        _listViewBind = LikeClass.getLiked(_listViewBind, Session["loginuser"].ToString());
                    }
                    catch (Exception ex)
                    { 
                        throw ex;
                    }
                }
                foreach (PhotoInfo item in _listViewBind)
                {
                    //List<Comment> _newComent = new List<Comment>();
                    //var allCommentItems = _allComments.Where(db => db.P_ID == item.PID).ToList();
                    //foreach (var itemComments in allCommentItems)
                    //{
                    //    _newComent.Add(new Comment
                    //    {
                    //        CDate = Convert.ToDateTime(itemComments.CDate),
                    //        PID=itemComments.P_ID,
                    //        UID = itemComments.U_ID,
                    //        CText = itemComments.P_Comment,
                    //        UserUrl=item.Url,
                    //        UserFName = item.userFName,
                    //        UserSName = item.userSName,
                    //        UserImg = item.userImg,
                    //    });
                    //}
                    //item.CommentList = _newComent;
                    item.CommentList = SqlTask.GetCommentsByPhotoId(item.PID);
                    item.tComment = item.CommentList.Count;
                    //item.tComment = SqlTask.GetTotalComment(item.PID);
                    switch (item.rate)
                    {
                        case "0":
                            item.imgStar = "/Images/0star.png";
                            break;
                        case "1":
                            item.imgStar = "/Images/1star.png";
                            break;
                        case "2":
                            item.imgStar = "/Images/2star.png";
                            break;
                        case "3":
                            item.imgStar = "/Images/3star.png";
                            break;
                        case "4":
                            item.imgStar = "/Images/4star.png";
                            break;
                        case "5":
                            item.imgStar = "/Images/5star.png";
                            break;
                        default:
                            item.imgStar = "/Images/0star.png";
                            break;
                    }
                }

                ListView1.DataSource = _listViewBind;
                ListView1.DataBind();
            }
            finally
            {
                conn.Close();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); }
            if (!Page.IsPostBack)
            {
                if (Session["loginuser"] == null) { FbLogin.facetoken = null; }
                mainItem();
            }
        }

        protected void btnLike_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["loginuser"] == null) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true); }
            else
            {
                var x = sender as ImageButton;
                string UID = x.CommandName;             
                string PID = x.Attributes["imgId"];
                string liked = x.ImageUrl;
                if (liked == "/Images/unlike.png")
                {
                    if (LikeClass.sendLike(UID, PID) == true)
                    {
                        x.ImageUrl = "/Images/like.png";
                        ListViewDataItem item = (ListViewDataItem)(sender as Control).NamingContainer;
                        Label lblTLike = (Label)item.FindControl("lblTLike");
                        lblTLike.Text = SqlTask.getTotalLikeByPhoto(PID).ToString();
                    }
                }
                else if (liked == "/Images/like.png")
                {
                    if (LikeClass.UnLike(UID, PID) == true)
                    {
                        x.ImageUrl = "/Images/unlike.png";
                        ListViewDataItem item = (ListViewDataItem)(sender as Control).NamingContainer;
                        Label lblTLike = (Label)item.FindControl("lblTLike");
                        lblTLike.Text = SqlTask.getTotalLikeByPhoto(PID).ToString();
                    }          
                }
            }
        }

        protected void btnExifPopUp_Click(object sender, ImageClickEventArgs e)
        {
            var x = sender as ImageButton;
            exifCssID.Attributes.CssStyle.Add("visibility", "visible");
            exifCssExitID.Attributes.CssStyle.Add("visibility", "visible");
            exifCssID.Attributes.CssStyle.Add("transform", "translate(-50%, -50%) scale(1)");
            exifCssExitID.Attributes.CssStyle.Add("transform", "scale(1)");
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

        protected void btnSendComment_Click(object sender, EventArgs e)
        {
            if (Session["loginuser"] == null) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true); }
            else
            {
                var x = sender as Button;
                ListViewDataItem item = (ListViewDataItem)(sender as Control).NamingContainer;
                TextBox txtComment = (TextBox)item.FindControl("txtComment");
                if (string.IsNullOrEmpty(txtComment.Text) == false)
                {
                    Comment comm = new Comment()
                    {
                        CText = txtComment.Text,
                        PID = x.Attributes["imgId"],
                        UID = Session["loginuser"].ToString(),
                        CDate = DateTime.Now,
                    };
                    if (Comment.sendComment(comm) == true)
                    {
                        txtComment.Text = string.Empty;
                        var ulcomm = item.FindControl("ulComment");

                        User userr = SqlTask.GetUserInfoForComment(comm.UID);
                        LiteralControl comment = new LiteralControl();

                        string addcomment1 = @"<li class='comment' runat='server'><a class='pull-left' href ='#'><img class='avatar' src='";
                        string addcomment2 = userr.ImgPath + @"'></a><div class='comment-body'><div class='comment-heading'><h4 class='user'>";
                        string addcomment3 = userr.FName+" "+ userr.SName + @"</h4><h5 class='time'>(";
                        string addcomment4 = DateTime.Now + @")</h5></div><p>";
                        string addcomment5 = comm.CText + @"</p></div></li>";

                        comment.Text = addcomment1 + addcomment2 + addcomment3 + addcomment4 + addcomment5;
                        ulcomm.Controls.AddAt(0,comment);

                        ListViewDataItem itemm = (ListViewDataItem)(sender as Control).NamingContainer;
                        Label lblTComment = (Label)item.FindControl("lblTComment");
                        lblTComment.Text = SqlTask.GetTotalComment(comm.PID).ToString();
                    }
                }
            }
        }

        protected void btnCloseExif_Click(object sender, ImageClickEventArgs e)
        {
            exifCssID.Attributes.CssStyle.Add("visibility", "hidden");
            exifCssExitID.Attributes.CssStyle.Add("visibility", "hidden");
            exifCssID.Attributes.CssStyle.Add("transform", "translate(-50%, -50%) scale(0)");
            exifCssExitID.Attributes.CssStyle.Add("transform", "scale(0)");
        }
    }
}