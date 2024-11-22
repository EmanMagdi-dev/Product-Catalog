using Microsoft.AspNetCore.Mvc;
using Product_Catalog.DAL.Repositories;
using Product_Catalog.DTOs;
using Product_Catalog.Models;

namespace Product_Catalog.BLL.Services
{
	public interface Icommon<T> where T : class
	{
		Task<RepositoryResult<List<T>>> Index(int pageNumber, int pageSize, string searchTerm = "");
        Task<RepositoryResult<T>> GetById(int id);
        Task<RepositoryResult<T>> Add(T model);
        Task<RepositoryResult<T>> Update(int Id, T model);
        Task<ActionResult<Product>> Delete(int id);
	}
}
