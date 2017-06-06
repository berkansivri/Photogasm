using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Photogasm.controls
{
    public partial class ctlChatBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["loginuser"] != null)
            {
                hdnCurrentUserName.Value = Session["menuUsername"].ToString();
                hdnCurrentUserID.Value = Session["loginuser"].ToString();
            }
            
        }
    }
}