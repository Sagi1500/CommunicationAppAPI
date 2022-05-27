using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Invitation
    {
        [Required]
        public string From;

        [Required]
        public string To;

        [Required]
        public string Server;

    }
}
