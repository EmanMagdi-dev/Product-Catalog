using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Catalog.BLL.Services;
using Product_Catalog.DTOs;
using Product_Catalog.Models;
using System.Security.Claims;
using static Product_Catalog.BLL.Helper.ProductHelper;

namespace Product_Catalog.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly Icommon<Product> _icommon;
        private readonly Icommon<Category> _Icommon;
        private readonly ILogg _logg;

        public AdminController(
                    Icommon<Product> icommon,
                    Icommon<Category> Icommon,
                    ILogg logg)
        {
            _icommon = icommon;
            _Icommon = Icommon;
            _logg = logg;
        }
        // GET: AdminController
        public async Task<ActionResult> Index()
        {
            var proList = await _icommon.Index(1, 10, "");
            return View(proList.Data);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Product>> Create(ProductDTO product, IFormFile ImagePath)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "User";
            string file = "";
            if (ImagePath == null || ImagePath.Length == 0)
            {
                ModelState.AddModelError("ImagePath", "The image is required.");
            }
            if (ImagePath != null)
            {
                 file=ValidateImagePath(ImagePath);
            }

            // Store the file path in ImagePath (as a string)
            product.ImagePath = $"/uploads/{file}";

            if (ModelState.IsValid)
            {
                Product pro = product;
                pro.ImagePath = product.ImagePath;
                pro.CreatedByUserId = userId;

                var productdata = await _icommon.Add(pro);
                if (productdata.IsSuccess)
                {
                   await _logg.SaveLog(pro.Id, userId,ActionType.Added);
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        // GET: AdminController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var product = await _icommon.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            ProductDTO model = product.Data;
            var cats = await _Icommon.Index(0, 0, "");
            ViewBag.cats = new SelectList(cats.Data, "Id", "Name");
            return View(model);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductDTO product, IFormFile ImagePath)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "User";

            if (ImagePath != null)
            {
               string fileName=ValidateImagePath(ImagePath);

                // Store the file path in ImagePath (as a string)
                product.ImagePath = $"/uploads/{fileName}";
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Product pro = product;
                    pro.ImagePath = product.ImagePath;
                    pro.CreatedByUserId = userId;
                    await _icommon.Update(id, pro);
                  await  _logg.SaveLog(pro.Id, userId, ActionType.Updated);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return NotFound();
                }
            }
            return View(product);


        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [AcceptVerbs("GET", "POST")]
        public string ValidateImagePath([FromForm] IFormFile ImagePath)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            long maxFileSize = 1 * 1024 * 1024; // 1 MB

            if (ImagePath.Length > maxFileSize)
            {
                ModelState.AddModelError("ImagePath", $"File size cannot exceed {maxFileSize / (1024 * 1024)} MB.");
            }
            else
            {
                var fileExtension = Path.GetExtension(ImagePath.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImagePath", "Only JPG and PNG files are allowed.");
                }
            }


            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                ImagePath.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
