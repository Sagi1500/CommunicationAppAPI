using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Message
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string? content { get; set; }

        public DateTime created { get; } = DateTime.Now;

        [JsonIgnore]
        public Contact contact { get; set; }

        [JsonIgnore]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        public string? contactName { get; set; }

        [JsonIgnore]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        public string? userName { get; set; }

        [JsonIgnore]
        public bool sent { get; set; }

        
    }
}
