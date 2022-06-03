using System.ComponentModel.DataAnnotations;

namespace real.Models
{
    public class User

    {
        public int Id { get; set; }
        [Key] 
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string? image { get; set; }
        public List<Contact>? contacts { get; set; }
    }
}
