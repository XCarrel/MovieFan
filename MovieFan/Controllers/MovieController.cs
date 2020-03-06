using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieFan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieFan.Controllers
{
    public class MovieController : Controller
    {
        readonly moviefanContext db;

        public MovieController(moviefanContext db)
        {
            this.db = db;
        }

        // GET: Movie
        public ActionResult Index()
        {
            List<Movies> allmovies = db.Movies
                .Include(m => m.Category)
                .Include(m => m.Rating)
                .ToList();
            return View(allmovies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            List<SelectListItem> categories = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Categories = categories;
            ViewBag.ratings = db.Ratings.ToList();
            Movies movie = db.Movies
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
            List<SelectListItem> categories = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Categories = categories;
            ViewBag.ratings = db.Ratings.ToList();
            Movies newmovie = new Movies();
            ViewBag.viewMode = 3;
            return View("Details", newmovie);
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Movies> Create([Bind("Title,Synopsis,CategoryId,RatingId, Picture,ReleaseDate")] Movies movie)
        {
            try
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                TempData["flashmessage"] = "Film Ajouté";
                TempData["flashmessagetype"] = "info";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Movies> Edit(int id, [Bind("Title,Synopsis,CategoryId,RatingId,Picture,ReleaseDate")] Movies movie)
        {
            if (ModelState.IsValid)
                try
                {
                    movie.Id = id; // because id isn't bound to the form inputs
                    db.Update(movie);
                    db.SaveChanges();
                    TempData["flashmessage"] = "Changement enregistré";
                    TempData["flashmessagetype"] = "info";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    TempData["flashmessage"] = "Problème...";
                    TempData["flashmessagetype"] = "danger";
                    Console.WriteLine(e.ToString());
                    return View("Details", movie);
                }
            else
            {
                ViewBag.viewMode = 2;
                return Details(id);
            }

        }

        // POST: Movie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                db.Remove(db.Movies.First(m => m.Id == id));
                db.SaveChanges();
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
    }
}