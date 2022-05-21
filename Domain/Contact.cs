using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Contact
    {
        [Key]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        public string? id { get; set; }

        [Key]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        public string? name { get; set; }
        
        [Required]
        public string? server { get; set; }
        
        public string last { get; set; }
        
        public DateTime lastdate { get; set; }

        [JsonIgnore]
        public List<Message> messagesList { get; set; } = new List<Message>();

        [JsonIgnore]
        public User user { get; set; }
    }
}
