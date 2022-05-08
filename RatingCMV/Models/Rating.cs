using System.ComponentModel.DataAnnotations;

namespace CommunicationAppAPI.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string? Name { get; set; }

        [Required]
        [Range(1,5)]
        public int Rate { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        public DateTime Time { get; set; }

    }
}
