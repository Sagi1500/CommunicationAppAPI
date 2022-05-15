using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Registers
    {
        [Key]
        [Required]
        public string id { get; set; }
        [Required]
        public string password { get; set; }
        public List<Contacts> contactsList { get; set; } = new List<Contacts>();
    }
}
