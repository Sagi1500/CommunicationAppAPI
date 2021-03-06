using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }

        public DateTime? Created { get; set; }

        [JsonIgnore]
        public Contact? Contact{ get; set; }

        [JsonIgnore]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        public string? ContactId { get; set; }

        [JsonIgnore]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        public string? UserId { get; set; }

        public bool? Sent { get; set; }

        
    }
}
