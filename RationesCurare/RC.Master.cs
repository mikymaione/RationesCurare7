using RationesCurare.DB.DataWrapper;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace RationesCurare
{
    public partial class RC : MasterPage
    {

        private string Tipo => GB.GetQueryString(Request, "T");

        private string CurrentPage => Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "");

        protected void Page_Load(object sender, EventArgs e)
        {
            var s = GB.Instance.getCurrentSession(Session);

            if (s == null)
            {
                Response.Redirect("welcome.aspx");
            }
            else if (!s.LoggedIN)
            {
                Response.Redirect("welcome.aspx");
            }
            else
            {
                DisableNavs();

                var p = new PeriodiciService();
                p.ControllaPeriodici(Session);
            }
        }

        private void DisableNavs()
        {
            var me = "nav_" + CurrentPage;

            var navs = new HtmlAnchor[] {
                nav_mMenu,
                nav_mCasse,
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