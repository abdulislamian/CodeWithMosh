using CodeWithMosh.Data;
using CodeWithMosh.Models;
using CodeWithMosh.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CodeWithMosh.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            var data = _db.Movies.Include(c => c.Genre).ToList();
            if (data == null)
            {
                return NotFound();
            }
            if (User.IsInRole(Utility.Helper.Admin))
            {
                return View("Index", data);
            }
            return View("ReadOnlyList", data);

        }
        public ActionResult Details(int id)
        {
            var movies = _db.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);

            if (movies == null)
                return NotFound();

            return View(movies);
        }



    }
}
