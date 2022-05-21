using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Contact
    {
        [Key]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        public string? Id { get; set; }
        
        public string? Name { get; set; }
        
        [Required]
        public string? Server { get; set; }
        
        public string Last { get; set; }
        
        public DateTime Lastdate { get; set; }

        [JsonIgnore]
        public List<Message> MessagesList { get; set; } = new List<Message>();

        [JsonIgnore]
        public User User { get; set; }

        [Key]
        [Required]
        [JsonIgnore]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        public string? UserId { get; set; }
    }
}
