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
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                // TODO: Add insert logic here

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
        public ActionResult<Movies> Edit(int id, [Bind("Title,Synopsis,CategoryId,RatingId")] Movies movie)
        {
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
                return Edit(id);
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}