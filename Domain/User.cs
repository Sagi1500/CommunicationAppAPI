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
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [JsonIgnore]
        public List<Contact>? ContactsList { get; set; } = new List<Contact>();
    }
}
