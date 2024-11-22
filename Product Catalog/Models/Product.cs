using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Catalog.BLL.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Catalog.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string CreatedByUserId { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 day")]
        public int Duration { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public string ImagePath { get; set; }
      
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } 
    }
}
