using System;

namespace RationesCurare
{
    public partial class RC : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GB.CurrentSession == null)
                Response.Redirect("mLogin.aspx");
            else if (!GB.CurrentSession.LoggedIN)
                Response.Redirect("mLogin.aspx");
        }

    }
}