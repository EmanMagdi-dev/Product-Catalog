using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product_Catalog.BLL.Helper;
using Product_Catalog.BLL.Services;
using Product_Catalog.DTOs;
using Product_Catalog.Models;

namespace Product_Catalog.PL.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly Icommon<Product> icommon;
        private readonly ISelectList<Category> _select;
        private readonly IFilter<Product> _filter;


        public ProductController(Icommon<Product> Icommon,
                                ISelectList<Category> select,
                                IFilter<Product> filter
                                )
        {
            icommon = Icommon;
            _select = select;
            _filter = filter;

        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 12, string searchTerm = "")
        {

            var cats = _select.GetSelectList();

            var jsonCategories = System.Text.Json.JsonSerializer.Serialize(cats);

            HttpContext.Session.SetString("Categories", jsonCategories);

            var result = await icommon.Index(pageNumber, pageSize, searchTerm);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            var products = ProductHelper.ToDTOList(result.Data);

            var totalItems = products.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.HasNextPage = pageNumber < totalPages;

            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult<Product>> Details(int id)
        {
            var product = await icommon.GetById(id);
            return product.Data;
        }

        [HttpGet]
        public ActionResult<List<Product>> SearchProducts(string searchTerm)
        {
            var pro = _filter.search(p => p.Name.Contains(searchTerm) ||
                                              p.Category.Name.Contains(searchTerm));
            return pro;
        }

        [HttpGet]
        public ActionResult<List<Product>> SearchProductsbyCat(string id)
        {
            var pro = _filter.search(p => p.CategoryId == int.Parse(id));
            return pro;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> filter(int id)
        {
            var result = await icommon.Index(0, 0, "");

            if (result.Data.Any())
            {
                var products = ProductHelper.ToDTOList(result.Data);
                return products;
            }
            var prods = await icommon.Index(0, 0, "");
            var ProductList = ProductHelper.ToDTOList(prods.Data);

            return ProductList;

        }

    }



}
