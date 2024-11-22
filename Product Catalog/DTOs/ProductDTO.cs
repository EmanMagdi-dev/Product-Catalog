using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Product_Catalog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace Product_Catalog.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public string CreatedByUserId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public int Duration { get; set; }
        public decimal Price { get; set; }

        //[Remote(action: "ValidateImagePath", controller: "Product")]
        public string ImagePath { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;



        public static implicit operator ProductDTO(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Duration = product.Duration,
                CategoryName = product.Category?.Name ?? "Unknown",
                CategoryId = product.CategoryId,
                Price = product.Price,
                ImagePath = product.ImagePath

            };
        }

        public static implicit operator Product(ProductDTO productdto)
        {
            if (productdto == null)
                throw new ArgumentNullException(nameof(productdto));

            return new Product
            {
                Id = productdto.Id,
                Name = productdto.Name,
                Duration = productdto.Duration,
                CategoryId = productdto.CategoryId,
                Price = productdto.Price,
                ImagePath = productdto.ImagePath
            };
        }


    }
}
