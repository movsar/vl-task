using System.ComponentModel.DataAnnotations;

namespace Vl_Task.Models {
    public class Product {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        List<ProductVersion> productVersions { get; set; }
    }
}
