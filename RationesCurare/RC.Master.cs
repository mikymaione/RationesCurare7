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
            if (GB.Instance.getCurrentSession(Session) == null)
                Response.Redirect("mLogin.aspx");
            else if (!GB.Instance.getCurrentSession(Session).LoggedIN)
                Response.Redirect("mLogin.aspx");
            else
                DisableNavs();
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

        protected void bNuovo_Click(object sender, EventArgs e)
        {
            var tipo = Tipo.Length == 0 ? "" : $"&T={Tipo}";

            Response.Redirect($"mMovimento.aspx?ID=-1{tipo}");
        }

    }
}