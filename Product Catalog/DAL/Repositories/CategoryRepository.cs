using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Catalog.BLL.Services;
using Product_Catalog.Models;

namespace Product_Catalog.DAL.Repositories
{
    public class CategoryRepository : Icommon<Category> ,ISelectList<Category>
    {

        private readonly ProductDbContext _dbContext;

        public CategoryRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SelectList GetSelectList()
        {
            var list = _dbContext.Categories.ToList();
          return  new SelectList(list, "Id", "Name");
        }

        public async Task<RepositoryResult<List<Category>>> Index(int pageNumber, int pageSize, string searchTerm = "")
        {
            try { 
           var cats= _dbContext.Categories.ToList();
            if (!cats.Any())
                return RepositoryResult<List<Category>>.Failure("There is no categories");

            return RepositoryResult<List<Category>>.Success(cats);
        }
            catch (Exception ex)
            {
                return RepositoryResult<List<Category>>.Failure($"An error occurred: {ex.Message}");
            }
           
        }

        public IActionResult Update(int Id, Category model)
        {
            throw new NotImplementedException();
        }

        Task<RepositoryResult<Category>> Icommon<Category>.Add(Category model)
        {
            throw new NotImplementedException();
        }

        Task<ActionResult<Product>> Icommon<Category>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        Task<RepositoryResult<Category>> Icommon<Category>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<RepositoryResult<Category>> Icommon<Category>.Update(int Id, Category model)
        {
            throw new NotImplementedException();
        }
    }
}
