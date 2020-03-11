using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieFan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace MovieFan.Controllers
{
    public class MovieController : Controller
    {
        private readonly moviefanContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MovieController(moviefanContext db, SignInManager<IdentityUser> signInManager)
        {
            _db = db;
            _signInManager = signInManager;
        }

        // GET: Movie
        public ActionResult Index()
        {
            if (AccessDenied()) return Redirect("/");
            List<Movies> allmovies = _db.Movies
                .Include(m => m.Category)
                .Include(m => m.Rating)
                .ToList();
            return View(allmovies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            if (AccessDenied()) return Redirect("/");
            List<SelectListItem> categories = _db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Categories = categories;
            ViewBag.ratings = _db.Ratings.ToList();
            Movies movie = _db.Movies
                .Include(m => m.Category)
                .Include(m => m.Rating)
                .Include(m => m.UserLikeMovie)
                .ThenInclude(ulm => ulm.User)
                .First(m => m.Id == id);
            return View("Details",movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            if (AccessDenied()) return Redirect("/");
            List<SelectListItem> categories = _db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Categories = categories;
            ViewBag.ratings = _db.Ratings.ToList();
            Movies newmovie = new Movies();
            newmovie.Category = _db.Categories.ToArray()[0];
            newmovie.Rating = _db.Ratings.ToArray()[0];
            ViewBag.viewMode = Helpers.ViewModes.Create;
            return View("Details", newmovie);
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Movies> Create([Bind("Title,Synopsis,CategoryId,RatingId, Picture,ReleaseDate")] Movies movie)
        {
            if (AccessDenied()) return Redirect("/");
            try
            {
                _db.Movies.Add(movie);
                _db.SaveChanges();
                TempData["flashmessage"] = "Film Ajouté";
                TempData["flashmessagetype"] = "info";
                ViewBag.viewMode = Helpers.ViewModes.Show;
                return Details(movie.Id);
            }
            catch (Exception e)
            {
                TempData["flashmessage"] = "Problème...";
                TempData["flashmessagetype"] = "danger";
                Console.WriteLine(e.ToString());
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Movies> Edit(int id, [Bind("Title,Synopsis,CategoryId,RatingId,Picture,ReleaseDate")] Movies movie)
        {
            if (AccessDenied()) return Redirect("/");
            if (ModelState.IsValid)
                try
                {
                    movie.Id = id; // because id isn't bound to the form inputs
                    _db.Update(movie);
                    _db.SaveChanges();
                    TempData["flashmessage"] = "Changement enregistré";
                    TempData["flashmessagetype"] = "info";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    TempData["flashmessage"] = "Problème...";
                    TempData["flashmessagetype"] = "danger";
                    Console.WriteLine(e.ToString());
                    ViewBag.viewMode = Helpers.ViewModes.Edit;
                    return View("Details", movie);
                }
            else
            {
                ViewBag.viewMode = Helpers.ViewModes.Edit;
                return Details(id);
            }

        }

        // POST: Movie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (AccessDenied()) return Redirect("/");
            try
            {
                _db.Remove(_db.Movies.First(m => m.Id == id));
                _db.SaveChanges();
                TempData["flashmessage"] = "Film supprimé";
                TempData["flashmessagetype"] = "info";
            }
            catch (Exception e)
            {
                TempData["flashmessage"] = "Problème...";
                TempData["flashmessagetype"] = "danger";
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction(nameof(Index));
        }

        // Check if user is logged in
        private bool AccessDenied()
        {
            Users user = Helpers.LoggedInUser(_db, _signInManager);
            if (user != null)
            {
                ViewBag.UserName = user.FullName;
                return false;
            }
            else
            {
                TempData["flashmessage"] = "Cette section est réservée à nos membres...";
                TempData["flashmessagetype"] = "info";
                return true;
            }
        }
    }
}