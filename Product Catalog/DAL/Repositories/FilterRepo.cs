using Microsoft.EntityFrameworkCore;
using Product_Catalog.BLL.Services;
using Product_Catalog.Models;
using System.Linq.Expressions;

namespace Product_Catalog.DAL.Repositories
{
    public class FilterRepo<T> : IFilter<T> where T : class
    {
        private readonly ProductDbContext _context;

        public FilterRepo(ProductDbContext context)
        {
            _context = context;
        }

        public List<T> search(Expression<Func<T, bool>> filter)
        {
            var dbSet = _context.Set<T>();
            return dbSet.Where(filter).ToList();
        }

      
    }

}
