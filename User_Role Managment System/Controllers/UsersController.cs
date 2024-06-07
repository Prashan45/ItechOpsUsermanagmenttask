using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Role_Managment_System.DBContext;
using User_Role_Managment_System.Models;

namespace User_Role_Managment_System.Controllers
{
    public class UsersController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string Username = HttpContext.Session.GetString("Username");
            ViewData["Username"] = Username;
            return View();
        }
        public async Task<IActionResult> SaveUser(User_Model model, IFormFile profiepic)
        {
            if(model.user_Id == 0)
            {
                if (profiepic != null && profiepic.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await profiepic.CopyToAsync(memoryStream);
                        model.ProfiePic = memoryStream.ToArray();
                    }
                }
                var newUser = new User_Model
                {
                    User_FirstName = model.User_FirstName,
                    User_Lastname = model.User_Lastname,
                    Usermail = model.Usermail,
                    UserAddress = model.UserAddress,
                    User_contact = model.User_contact,
                    ProfiePic = model.ProfiePic,
                };
                _context.Entry(newUser).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            else
            {
                if (profiepic != null && profiepic.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await profiepic.CopyToAsync(memoryStream);
                        model.ProfiePic = memoryStream.ToArray();
                    }
                }
                var newUser = new User_Model
                {
                    user_Id = model.user_Id,
                    User_FirstName = model.User_FirstName,
                    User_Lastname = model.User_Lastname,
                    Usermail = model.Usermail,
                    UserAddress = model.UserAddress,
                    User_contact = model.User_contact,
                    ProfiePic = model.ProfiePic,
                };
                _context.Entry(newUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Users");
        }
        public async Task<IActionResult> Details()
        {
            string Username = HttpContext.Session.GetString("Username");
            ViewData["Username"] = Username;
            var users = await _context.User_Tbl.ToListAsync();

            return View(users);
        }
        public async Task<IActionResult> Edit(int id)
        {
            string Username = HttpContext.Session.GetString("Username");
            ViewData["Username"] = Username;

            var user = await _context.User_Tbl.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.User_Tbl.FindAsync(id);

            if (user != null)
            {
                _context.User_Tbl.Remove(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Users");
            }

            return NotFound();
        }
        public async Task<IActionResult> DetailsforEmployee()
        {
            string Username = HttpContext.Session.GetString("Username");
            ViewData["Username"] = Username;
            var users = await _context.User_Tbl.ToListAsync();

            return View(users);
        }
        public async Task<IActionResult> FullDetailsofuser(int id)
        {
            string Username = HttpContext.Session.GetString("Username");
            ViewData["Username"] = Username;
            var user = await _context.User_Tbl.FindAsync(id);

            return View(user);
        }
        public async Task<IActionResult> UersProfile()
        {
            string Username = HttpContext.Session.GetString("Username");
            ViewData["Username"] = Username;
            var users = await _context.User_Tbl.ToListAsync();

            return View(users);
        }
    }
}
