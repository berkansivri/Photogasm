using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Photogasm
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginuser"] != null)
            {
                Session.Abandon();
                if (FbLogin.facetoken != null)
                {
                    //Response.Redirect("https://www.facebook.com/logout.php?next=http://localhost:1257/Default&access_token=" + FbLogin.facetoken);
                    Response.Redirect("https://www.facebook.com/logout.php?next=http://teambro.azurewebsites.net/&access_token=" + FbLogin.facetoken);

                }
            }
            Response.Redirect("~/Default.aspx");

        }
    }
}