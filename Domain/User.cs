namespace Domain
{
    public class User
    {
        public string id { get; set; }   
        public string password { get; set; }
        public List<Contact> contacts { get; set; }
    }
}
