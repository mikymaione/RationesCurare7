using System;

namespace RCWebMobile
{
    public partial class RC : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GB.CurrentSession == null)
                Response.Redirect("mLogin.aspx");
            else if (!GB.CurrentSession.LoggedIN)
                Response.Redirect("mLogin.aspx");

            var p = this.Page.ToString() + this.Page.ClientQueryString;

            if (p == "ASP.mmenu_aspx")
                AApplyStyle(ref a1);
            if (p.IndexOf("ASP.mgrafico_aspx") > -1)
                AApplyStyle(ref a8);
        }

        private void AApplyStyle(ref System.Web.UI.HtmlControls.HtmlAnchor a)
        {
            a.Style["border-bottom-style"] = "solid";
            a.Style["border-bottom-width"] = "2px";
            a.Style["border-bottom-color"] = "#FFFFFF";
        }


    }
}