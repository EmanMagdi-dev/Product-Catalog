using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Catalog.BLL.Services;
using Product_Catalog.Models;

namespace Product_Catalog.DAL.Repositories
{
    public class ProductRepository : Icommon<Product>
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RepositoryResult<Product>> Add(Product model)
        {
            if (model is null)
            {
                return RepositoryResult<Product>.Failure("Not Found");

            }
            else
            {
                _dbContext.Products.Add(model);
                _dbContext.SaveChanges();
                return RepositoryResult<Product>.Success(model);

            }

        }

        public async Task<ActionResult<Product>>  Delete(int id)
        {
            var pro =await GetById(id);
            _dbContext.Products.Remove(pro.Data);
            _dbContext.SaveChanges();
            return pro.Data;

        }

        public async Task<RepositoryResult<Product>> GetById(int id)
        {

            try
            {
                var product = _dbContext.Products.Include(s => s.Category).FirstOrDefault(s => s.Id == id);
                if (product == null)
                    return RepositoryResult<Product>.Failure("Not Found");

                return RepositoryResult<Product>.Success(product);


            }
            catch (Exception ex)
            {
                return RepositoryResult<Product>.Failure($"An error occurred: {ex.Message}");
            }

        }

        public async Task<RepositoryResult<List<Product>>> Index(int pageNumber, int pageSize, string searchTerm = "")
        {

            try
            {
                var query = _dbContext.Products.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(p => p.Name.Contains(searchTerm) ||
                                             p.Category.Name.Contains(searchTerm));
                }
               
                var productList = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Where(s => s.StartDate.AddDays(s.Duration) >= s.StartDate)
                    .Include(s => s.Category)
                    .ToListAsync();

                if (!productList.Any())
                    return RepositoryResult<List<Product>>.Failure("There is no products");

                return RepositoryResult<List<Product>>.Success(productList);
            }
            catch (Exception ex)
            {
                return RepositoryResult<List<Product>>.Failure($"An error occurred: {ex.Message}");
            }


        }

      

       

        public async Task<RepositoryResult<Product>> Update(int Id, Product model)
        {
            try
            {
              _dbContext.Entry(model).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RepositoryResult<Product>.Success(model);

            }
            catch (Exception ex)
            {
                return RepositoryResult<Product>.Failure($"An error occurred: {ex.Message}");

            }



        }


    }
}
