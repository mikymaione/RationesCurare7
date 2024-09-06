using System;

namespace RationesCurare
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Error(object sender, EventArgs e)
        {
            Application["TheException"] = Server.GetLastError();
            Server.ClearError();
            Response.Redirect("~/ErrorPage.aspx");
        }

    }
}