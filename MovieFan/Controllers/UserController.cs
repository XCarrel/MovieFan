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

    public class UserController : Controller
    {
        readonly moviefanContext db;

        public UserController(moviefanContext db)
        {
            this.db = db;
        }
        // GET: User
        public ActionResult Index()
        {
            List<Users> users = db.Users.ToList();
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            Users user = db.Users
                            .Include(u => u.UserLikeMovie)
                            .ThenInclude(ulm => ulm.Movie)
                            .SingleOrDefault(u => u.Id == id);
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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

        public ActionResult ManageMovies(int id)
        {
            Users user = db.Users
                            .Include(u => u.UserLikeMovie)
                            .ThenInclude(ulm => ulm.Movie)
                            .SingleOrDefault(u => u.Id == id);
            return View("ManageMovies",user);
        }
        public ActionResult AddMovieReview(int id)
        {
            Users user = db.Users
                            .Include(u => u.UserLikeMovie)
                            .ThenInclude(ulm => ulm.Movie)
                            .SingleOrDefault(u => u.Id == id);
            List<int> alreadyReviewed = db.UserLikeMovie.Where(u => u.UserId == user.Id).Select(u => u.MovieId).ToList();

            List<SelectListItem> movies = db.Movies.Where(m => !alreadyReviewed.Contains(m.Id)).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Title }).ToList();
            ViewBag.Movies = movies;

            return View(user);
        }

        public ActionResult NewMovieReview (int movieId, int stars, string review, int userId)
        {
            UserLikeMovie ulm = new UserLikeMovie();
            ulm.MovieId = movieId;
            ulm.UserId = userId;
            ulm.Stars = stars;
            ulm.Comment = review;
            ulm.HasSeen = 1;
            db.UserLikeMovie.Add(ulm);
            db.SaveChanges();
            return ManageMovies(userId);
        }

        public ActionResult EditMovieReview(int id)
        {
            UserLikeMovie ulm = db.UserLikeMovie
                            .Include(ulm => ulm.User)
                            .Include(ulm => ulm.Movie)
                            .SingleOrDefault(ulm => ulm.Id == id);
            return View(ulm);
        }
    }
}