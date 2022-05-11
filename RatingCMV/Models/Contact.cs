using System.ComponentModel.DataAnnotations;
namespace CommunicationAppApi.Models
{
    public class Contact
    {
        [Key]
        [Required]
        public string? id { get; set; }
        public string? name { get; set; }
        public string? server { get; set; }
        public string? last { get; set; }
        public string? lastdate { get; set; }
    }
}