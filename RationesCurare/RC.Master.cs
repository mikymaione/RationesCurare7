using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace RationesCurare
{
    public partial class RC : MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GB.Instance.getCurrentSession(Session) == null)
                Response.Redirect("mLogin.aspx");
            else if (!GB.Instance.getCurrentSession(Session).LoggedIN)
                Response.Redirect("mLogin.aspx");
            else
                DisableNavs();
        }

        private void DisableNavs()
        {
            var me = "nav_" + Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "");

            var navs = new HtmlAnchor[] {
                nav_mMenu,
                nav_mSaldo,
                nav_mGrafico,
                nav_mLogout
            };

            foreach (var nav in navs)
                nav.Attributes["class"] = me.Equals(nav.ID)
                    ? "not-active"
                    : "";
        }
    }
}