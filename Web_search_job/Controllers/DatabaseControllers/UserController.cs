using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_search_job.Data;
using Web_search_job.DatabaseClasses;

namespace Web_search_job.Controllers.DatabaseControllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await _context.Users
                .Include(u => u.UserInfo)
                    .ThenInclude(l => l.Location)
                .FirstOrDefaultAsync(u => u.Id == userId);

            Console.WriteLine(user);
            return user;
        }


        public async Task<List<SavedJob>> GetSavedJobsByUser(string userId)
        {
            // Отримати список збережених робіт для даного користувача

            var user = await _context.Users
                .Include(u => u.UserInfo)
                    .ThenInclude(l => l.SavedJobs)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.UserInfo == null)
            {
                return new List<SavedJob>(); // Повернути порожній список, якщо користувача не знайдено
            }

            return user.UserInfo.SavedJobs.ToList();

        }

        /*
                // GET: UserControllercs
                public ActionResult Index()
                {
                    return View();
                }

                // GET: UserControllercs/Details/5
                public ActionResult Details(int id)
                {
                    return View();
                }

                // GET: UserControllercs/Create
                public ActionResult Create()
                {
                    return View();
                }

                // POST: UserControllercs/Create
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Create(IFormCollection collection)
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

                // GET: UserControllercs/Edit/5
                public ActionResult Edit(int id)
                {
                    return View();
                }

                // POST: UserControllercs/Edit/5
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Edit(int id, IFormCollection collection)
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

                // GET: UserControllercs/Delete/5
                public ActionResult Delete(int id)
                {
                    return View();
                }

                // POST: UserControllercs/Delete/5
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
                }*/
    }
}
