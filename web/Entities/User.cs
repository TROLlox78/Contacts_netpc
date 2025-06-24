namespace Web.Entities
{
    public class User // user for authorization
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHashed { get; set; }
    }
}
