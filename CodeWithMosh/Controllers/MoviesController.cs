using CodeWithMosh.Data;
using CodeWithMosh.Models;
using CodeWithMosh.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeWithMosh.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public MoviesController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public ViewResult New()
        {
            var genres = dbContext.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        public IActionResult Random()
        {
            var movie = new Movies() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }
        public IActionResult Edit(int id)
        {
            return View();
        }
        public IActionResult Index(int? pageIndex, string? sortBy)
        {
            if(!pageIndex.HasValue)
            {
                pageIndex = 1;
            }
            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";
            return Content(string.Format("Page Index={0}&sortBy={1}",pageIndex,sortBy));
        }
    }
}
