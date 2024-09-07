using System;

namespace RationesCurare
{
    public partial class index : System.Web.UI.Page
    {

        private string Richiesta => GB.GetQueryString(Request, "C");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AutoLogin();
            }
        }

        private void AutoLogin()
        {
            var auto = GB.GetCookie(Request, "AutoLogin");
            var nome = GB.GetCookie(Request, "AutoLogin_UserName");
            var psw = GB.GetCookie(Request, "AutoLogin_UserPassword");

            if (auto.Equals("TRUE", StringComparison.OrdinalIgnoreCase))
            {
                Login_(nome, psw, true);
            }
        }

        private void Login_(string nome, string psw, bool redirect)
        {
            var App_Data = MapPath("App_Data");

            if (GB.ControllaCredenziali(App_Data, nome, psw))
            {
                var f = System.IO.Path.Combine(App_Data, nome);

                GB.Instance.setCurrentSession(Session, new cSession());
                GB.Instance.getCurrentSession(Session).LoggedIN = true;
                GB.Instance.getCurrentSession(Session).UserName = nome;
                GB.Instance.getCurrentSession(Session).PathDB = f + ".rqd8";
                GB.Instance.getCurrentSession(Session).Culture = GestioneValute.LeggiValutaInDbInfo(Session, nome);

                if (redirect)
                    Response.Redirect("mMenu.aspx");
            }
        }

        protected void bEntra_Click(object sender, EventArgs e)
        {
            Response.Redirect("mLogin.aspx");
        }

    }
}
