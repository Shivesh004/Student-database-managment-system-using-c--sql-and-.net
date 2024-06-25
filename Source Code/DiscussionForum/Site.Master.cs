using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiscussionForum
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        } 

        protected void HandleLogOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("LoginPage.aspx");
        }

        protected bool IsLoginPage()
        {
            string currentPageURL = Request.Url.AbsolutePath;
            return currentPageURL.EndsWith("LoginPage");
        }
    }
}