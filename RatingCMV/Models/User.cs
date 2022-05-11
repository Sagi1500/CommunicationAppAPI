namespace CommunicationAppApi.Models
{
    public class User
    {
        public string? id { get; set; }
        public string? password { get; set; }
        public List<Contact>? contactsList { get; set; }
    }
}
