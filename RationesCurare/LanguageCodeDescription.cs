namespace RationesCurare
{
    public class LanguageCodeDescription : System.IComparable<LanguageCodeDescription>, System.IEquatable<LanguageCodeDescription>
    {
        public string code { get; set; }
        public string description { get; set; }

        public int CompareTo(LanguageCodeDescription other)
        {
            if (other == null)
                return 1;

            return this.description.CompareTo(other.description);
        }

        public bool Equals(LanguageCodeDescription other)
        {
            if (other == null)
                return false;

            return this.code == other.code;
        }

    }
}