using Product_Catalog.DAL.Repositories;
using Product_Catalog.Models;
using static Product_Catalog.BLL.Helper.ProductHelper;

namespace Product_Catalog.BLL.Services
{
	public interface ILogg
	{
		Task<RepositoryResult<AuditLog>> SaveLog(int ProductId,string User, ActionType action);
	}
}
