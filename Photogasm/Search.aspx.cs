using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Photogasm
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SqlTask.conn.State == System.Data.ConnectionState.Open || SqlTask.conn.State == System.Data.ConnectionState.Connecting)
            { SqlTask.conn.Close(); }
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["s"]) == false)
                {
                    txtSearchh.Value = Request.QueryString["s"].ToLower().Trim();
                    btnSearchh_ServerClick(this, new EventArgs());
                }
            }        
        }

        protected void btnSearchh_ServerClick(object sender, EventArgs e)
        {
            string text = txtSearchh.Value.ToLower().Trim();
            string[] textt = new string[] { };
            if (text.Contains(" "))
            {
                textt = text.Split(' ');
                if (textt.Length > 1)
                {
                    List<User> userlist = SqlTask.GetSearchedUser(textt[0], textt[1]);
                    lstSearch.DataSource = userlist;
                    lstSearch.DataBind();
                }
            }    
            else
            {
                List<User> userlist = SqlTask.GetSearchedUser(text);
                lstSearch.DataSource = userlist;
                lstSearch.DataBind();
            }
        }
    }
}