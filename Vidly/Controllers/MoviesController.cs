using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            //ViewData["Movie"] = movie;
            //ViewBag.Movie = movie;

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
            // return Content("Hello World!");
            // return HttpNotFound();
            // return new EmptyResult();
            // return RedirectToAction("Index", "Home", new {page=1, sortBy = "name"});
        }

        public ActionResult Edit(int id)
        {
            var movie =  _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MovieFormViewModel(movie)
            {
                
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

        public ActionResult Details(int ID)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == ID);
            if(movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult New()
        {
            var genreTypes = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genreTypes
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                } ;
                return View("MovieForm", viewModel);

            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDB = _context.Movies.Single(m=>m.Id == movie.Id);
                movieInDB.Name = movie.Name;
                movieInDB.GenreID = movie.GenreID;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }


        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek"},
        //        new Movie { Id = 2, Name = "Wall-e"}
        //    };
        //}

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }

        //    if (String.IsNullOrEmpty(sortBy))
        //    {
        //        sortBy = "Name";
        //    }

        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
           return Content(year + "/" + month);
        }

    }
}                                                    