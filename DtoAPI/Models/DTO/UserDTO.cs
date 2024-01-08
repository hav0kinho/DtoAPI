using System.ComponentModel.DataAnnotations;

namespace DtoAPI.Models.DTO
{
    public class UserDTO
    {
        [Required]
        [StringLength(40)]
        public string? Name { get; set; }
        [Required]
        [StringLength(40)]
        public string? Password { get; set; }
    }
}
