using System.ComponentModel.DataAnnotations;

namespace DtoAPI.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string? Name { get; set; }
        [Required]
        [StringLength(40)]
        public string? Password { get; set; }
        [Required]
        public List<string>? Roles { get; set; }

    }
}
