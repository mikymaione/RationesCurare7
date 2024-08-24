using System;

namespace RationesCurare
{
    public partial class index : System.Web.UI.Page
    {
        
        protected void bEntra_Click(object sender, EventArgs e)
        {
            Response.Redirect("mLogin.aspx");
        }

    }
}