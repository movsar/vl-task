using Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Data.Models {
    public class ProductVersion : DbModelBase {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Required]
        public float Width { get; set; }
        [Required]
        public float Height { get; set; }
        [Required]
        public float Length { get; set; }
    }
}
