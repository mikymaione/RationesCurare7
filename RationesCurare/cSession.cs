namespace RationesCurare
{
    public class cSession
    {
        public bool LoggedIN = false;
        public string UserName;
        public string PathDB;
        public string ProviderName;
        public RationesCurare7.DB.cDB.DataBase TipoDB = RationesCurare7.DB.cDB.DataBase.Access;
        System.Web.SessionState.HttpSessionState Sessione;

        public cSession(System.Web.SessionState.HttpSessionState Sessione_)
        {
            Sessione = Sessione_;
        }

        ~cSession()
        {
            Save();
        }

        private void Save()
        {
            if (Sessione != null)
                Sessione.Add("cSession", this);
        }


    }
}