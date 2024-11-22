using System.Linq.Expressions;

namespace Product_Catalog.BLL.Services
{
    public interface IFilter<T>
    {
        List<T> search(Expression<Func<T, bool>> filter);
    }
}
