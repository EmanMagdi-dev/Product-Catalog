using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Product_Catalog.Models;
namespace Product_Catalog.PL.Controllers
{

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ProductDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
                                ProductDbContext dbcontext,
                                UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _dbContext = dbcontext;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = _dbContext.Users.ToList();
            ViewBag.Roles = _dbContext.Roles.ToList();
            ViewBag.userRoles = _dbContext.UserRoles.ToList();
            return View(users);
        }
        [HttpGet]
        public IActionResult addRole()
        {
            return View();
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> addRole(string RoleName)
        {
            IdentityRole role = new IdentityRole();
            role.Name = RoleName;
            role.ConcurrencyStamp = Guid.NewGuid().ToString();

            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("addRole");
            }
            else
            {
                return View(RoleName);
            }
        }
        [HttpPost]
        public async Task<IActionResult> giveRole(string Id, string roleId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == Id);
            var role = _dbContext.Roles.FirstOrDefault(x => x.Id == roleId).Name;
            IdentityResult result = await _userManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index");
            }

        }
    }
}


