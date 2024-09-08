using System;

namespace RationesCurare
{
    public partial class ErrorPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var ex = (Exception)Application["TheException"];

            if (ex != null)
            {
                ErrorMessageLiteral.Text = $"<h4>Error Message</h4><p>{ex.Message}</p>";
                ErrorDetailLiteral.Text = $"<h4>Error Details</h4><p class='noGiustificato'><small>{ex.StackTrace}</small></p>";
            }
        }

    }
}
