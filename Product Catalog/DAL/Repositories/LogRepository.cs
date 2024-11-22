
using Microsoft.AspNetCore.Mvc;
using Product_Catalog.BLL.Helper;
using Product_Catalog.BLL.Services;
using Product_Catalog.Models;

namespace Product_Catalog.DAL.Repositories
{
	public class LogRepository : ILogg
	{
		private readonly ProductDbContext _dbContext;

		public LogRepository(ProductDbContext dbContext)
        {
			_dbContext = dbContext;
        }
        public async Task<RepositoryResult<AuditLog>> SaveLog(int ProductId, string User, ProductHelper.ActionType action)
		{
			if(User == null)
			{
				return RepositoryResult<AuditLog>.Failure("Must Login First");
			}
			try
			{
			 _dbContext.AuditLogs.Add(new AuditLog() { ProductId = ProductId, UserId = User, Action = action, AtTime = DateTime.UtcNow });
				_dbContext.SaveChanges();
				return RepositoryResult<AuditLog>.Success(null);

			}
			catch
			{
				return RepositoryResult<AuditLog>.Failure("There is something Wrong");

			}
		}

		
	}
}
