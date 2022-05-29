using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Transfer
    {
        public string? From { get; set; }

        public string? To { get; set; } 

        public string? Content { get; set; }

    }
}
