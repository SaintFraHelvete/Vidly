using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private VidlyDBContext _context;

        public MoviesController()
        {
            _context = new VidlyDBContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _context.Dispose();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1", Id = 1 },
                new Customer { Name = "Customer 2", Id = 2 }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            
            return View(viewModel);

        }

        public ActionResult Index()
        {
            var viewModel = new MoviesViewModel
            {
                Movies = _context.Movies.ToList()
            };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.SingleOrDefault<Movie>(m => m.Id == id);

            return View(movie);
        }

        public ActionResult New()
        {
            return View("MoviesForm");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View("MoviesForm", movie);
        }

        [HttpPost]
        public ActionResult Save(Movie model)
        {
            if (model.Id == 0)
                _context.Movies.Add(model);
            else {
                var movieInDB = _context.Movies.Single(m => m.Id == model.Id);

                movieInDB.Name = model.Name;
                movieInDB.Genre = model.Genre;
                movieInDB.NumberInStock = model.NumberInStock;
                movieInDB.DateAdded = model.DateAdded;
                movieInDB.DateReleased = model.DateReleased;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}