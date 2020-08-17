using System;
using System.Web.UI.WebControls;

namespace RCWebMobile
{
    public partial class RC : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GB.CurrentSession == null)
            {
                Response.Redirect("mLogin.aspx");
            }
            else if (!GB.CurrentSession.LoggedIN)
            {
                Response.Redirect("mLogin.aspx");
            }

            AggiungiCasseExtra();

            var p = this.Page.ToString() + this.Page.ClientQueryString;

            if (p == "ASP.mmenu_aspx")
                AApplyStyle(ref a1);
            if (p == "ASP.msaldo_aspxT=Cassaforte")
                AApplyStyle(ref a2);
            if (p == "ASP.msaldo_aspxT=Salvadanaio")
                AApplyStyle(ref a3);
            if (p == "ASP.msaldo_aspxT=Portafogli")
                AApplyStyle(ref a4);
            if (p == "ASP.msaldo_aspxT=Dare")
                AApplyStyle(ref a5);
            if (p == "ASP.msaldo_aspxT=Avere")
                AApplyStyle(ref a6);
            if (p == "ASP.msaldo_aspxT=Saldo")
                AApplyStyle(ref a7);
            if (p.IndexOf("ASP.mgrafico_aspx") > -1)
                AApplyStyle(ref a8);
        }

        public void AggiungiCasseExtra()
        {
            try
            {
                var z = MapPath("DB");
                z = System.IO.Path.Combine(z, "DBW");

                using (var cdb = new RationesCurare7.DB.cDB(GB.CurrentSession.TipoDB, GB.CurrentSession.PathDB))
                {
                    var cas = new RationesCurare7.DB.DataWrapper.cCasse();
                    var CasseAggiuntive = cas.CasseAggiuntive(cdb, z);

                    if (CasseAggiuntive != null)
                        foreach (var Cassa in CasseAggiuntive)
                        {
                            SaveImageFromByte(Cassa.nome, Cassa.imgName);
                            AddCassa(Cassa.nome);
                        }
                }
            }
            catch
            {
                //non trovate
            }
        }

        private void SaveImageFromByte(string name, byte[] img)
        {
            var p = MapPath("images");
            p = System.IO.Path.Combine(p, name + ".png");

            if (!System.IO.File.Exists(p))
                try
                {
                    var i = System.Drawing.Image.FromStream(new System.IO.MemoryStream(img));
                    var b = new System.Drawing.Bitmap(i, 32, 32);

                    b.Save(p, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch
                {
                    //no img                                
                }
        }

        private void AddCassa(string nome)
        {
            var cell1 = new TableCell();

            var a = new System.Web.UI.HtmlControls.HtmlAnchor()
            {
                HRef = "mSaldo.aspx?T=" + nome
            };

            var i = new System.Web.UI.HtmlControls.HtmlImage()
            {
                Src = "images/" + nome + ".png"
            };
            i.Attributes.Add("title", nome);

            a.Controls.Add(i);
            cell1.Controls.Add(a);
            myTable.Rows[0].Cells.AddAt(7, cell1);
        }

        private void AApplyStyle(ref System.Web.UI.HtmlControls.HtmlAnchor a)
        {
            a.Style["border-bottom-style"] = "solid";
            a.Style["border-bottom-width"] = "2px";
            a.Style["border-bottom-color"] = "#FFFFFF";
        }


    }
}