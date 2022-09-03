namespace AluraFlixAPI.Models
{
    public class Token
    {

        public string Value { get; }

        public Token(string value)
        {
            this.Value = value;

        }
    }
}