using Product_Catalog.DTOs;
using Product_Catalog.Models;

namespace Product_Catalog.BLL.Helper
{
    public  class ProductHelper
	{
       
        public enum ActionType
		{
			Added = 1,
			Updated = 2,
			Deleted = 3
		}

		public static List<ProductDTO> ToDTOList(List<Product> products)
		{
			if (products == null || products.Count == 0)
				throw new ArgumentNullException(nameof(products));

			return products.Select(prod => new ProductDTO
			{
				Id = prod.Id,
				Name = prod.Name,
				Duration = prod.Duration,
				Price = prod.Price,
				CategoryName = prod.Category?.Name,
				CategoryId = (int)prod.Category?.Id,
				ImagePath=prod.ImagePath

			}).ToList();
		}

		public static List<Product> ToProductList( List<ProductDTO> productDTO)
		{
			if (productDTO == null || productDTO.Count == 0)
				throw new ArgumentNullException(nameof(ProductDTO));

			List<Product> products = new List<Product>();
			Product product;
			foreach (var prod in productDTO)
			{
				product = prod;
				products.Add(product);
			}
			return products;
		}

	}
}
