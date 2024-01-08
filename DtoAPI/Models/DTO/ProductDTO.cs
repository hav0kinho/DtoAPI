using System.ComponentModel.DataAnnotations;

namespace DtoAPI.Models.DTO
{
    public class ProductDTO
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public float? Price { get; set; }
    }
}
