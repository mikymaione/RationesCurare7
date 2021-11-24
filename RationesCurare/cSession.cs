namespace RationesCurare
{
    public class cSession
    {
        public bool LoggedIN = false;
        public string UserName;
        public string PathDB;
        public string ProviderName;
        public RationesCurare7.DB.cDB.DataBase TipoDB = RationesCurare7.DB.cDB.DataBase.Access;
    }
}