using System;

namespace RationesCurare
{
    public partial class welcome : System.Web.UI.Page
    {
        protected void bEntra_Click(object sender, EventArgs e)
        {
            Response.Redirect("/mLogin.aspx");
        }
    }
}