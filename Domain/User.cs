using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Domain
{
    public class User
    {
        [Key]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        [Display(Name = "UserName")]
        public string? id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? password { get; set; }

        [JsonIgnore]
        public List<Contact> contactsList { get; set; } = new List<Contact>();
    }
}
