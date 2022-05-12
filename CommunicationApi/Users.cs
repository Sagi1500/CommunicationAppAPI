using System.ComponentModel.DataAnnotations;

namespace CommunicationApi
{
    public class Users
    {
        //[Key]
        [Required]
        //[RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string? id { get; set; }
        [Required]
        //[StringLength(100)]
        //[DataType(DataType.Password)]
        //[RegularExpression(@"^[a-z]+[A-Z0-9]+$")]
        public string? password { get; set; }
    }
}