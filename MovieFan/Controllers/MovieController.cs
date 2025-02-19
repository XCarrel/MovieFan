﻿using System;
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
        public ActionResult Index(int CategoryId)
        {
            List<SelectListItem> categories = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name , Selected = (x.Id == CategoryId)}).ToList();
            categories.Insert(0, new SelectListItem { Value = "0", Text = "(Toutes)" });
            ViewBag.Categories = categories;
            List<Movies> movies;
            if (CategoryId == 0)
                movies = db.Movies
                    .Include(m => m.Category)
                    .Include(m => m.Rating)
                    .ToList();
            else
                movies = db.Movies.Where(m => m.CategoryId == CategoryId)
                    .Include(m => m.Category)
                    .Include(m => m.Rating)
                    .ToList();
            return View(movies);
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
            return View("Details", movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            List<SelectListItem> categories = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.Categories = categories;
            ViewBag.ratings = db.Ratings.ToList();
            Movies newmovie = new Movies();
            newmovie.Category = db.Categories.ToArray()[0];
            newmovie.Rating = db.Ratings.ToArray()[0];
            ViewBag.viewMode = Helpers.ViewModes.Create;
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