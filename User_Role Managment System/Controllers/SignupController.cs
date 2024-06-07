using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using User_Role_Managment_System.DBContext;
using User_Role_Managment_System.Models;

namespace User_Role_Managment_System.Controllers
{
    public class SignupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SignupController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var RoleName = await _context.Role_Tbl.ToListAsync();
            TempData["RoleName"] = RoleName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(Signup_model model, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    model.Image = memoryStream.ToArray();
                }
            }
            var newUser = new Signup_model
            {
                Username = model.Username,
                Email = model.Email,
                Contact = model.Contact,
                Password = model.Password,
                RoleID = model.RoleID,
                Image = model.Image,
            };
            _context.Entry(newUser).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return RedirectToAction("Login", "Signup");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Signup_model model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            try
            {
                var user = await _context.Signup_Tbl
                   .Include(u => u.Role)
                   .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);


                if (user != null)
                {
                    if (user.Role != null)
                    {
                        HttpContext.Session.SetString("Username", user.Username);
                        HttpContext.Session.SetString("Role", user.Role.RoleName);

                        string role = user.Role.RoleName;
                        return role switch
                        {
                            "Admin" => RedirectToAction("Details", "Users"),
                            "Employee" => RedirectToAction("DetailsforEmployee", "Users"),
                            "Guest" => RedirectToAction("UersProfile", "Users"),
                            _ => RedirectToAction("Index", "Home")
                        };
                    }
                    else
                    {
                        HttpContext.Session.SetString("Username", user.Username);
                        HttpContext.Session.SetString("Role", "DefaultRole");
                        return RedirectToAction("Index", "DefaultPage");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password");
                    return View("Login", model);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred");
                return View("Login", model);
            }
        }
       
    }
}
