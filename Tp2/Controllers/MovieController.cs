using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tp2.Models;

namespace Tp2.Controllers
{
    public class MovieController : Controller
    {
        private CinemaEntities db = new CinemaEntities();

        // GET: Movie
        public ActionResult Index()
        {
            var movie = db.Movie.Include(m => m.Producer);
            return View(movie.ToList());
        }

        //GET:Movie/MoviesAndTheirProds
        public ActionResult MoviesAndTheirProds()
        {

            var movie = db.Movie.Include(m => m.Producer);
            return View(movie.ToList());
        }

        public ActionResult MoviesAndTheirProds_UsingModel()
        {

            return View(from x in db.Movie.ToList() join y in db.Producer.ToList() on x.Producer.Id equals(y.Id) select new ProdMovie { mTitle=x.Title ,mGenre=x.Genre,pName=y.Name,pNat=y.Nationality});
        }

        public ActionResult SearchByTitle(String id)
        {

            return View(db.Movie.Where(m=>m.Title.ToString().Equals(id)));
        }

        public ActionResult SearchBy2(String id)
        {

            return View(db.Movie.Where(m => m.Title.ToString().Equals(id)));
        }

        // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            ViewBag.ProducerId = new SelectList(db.Producer, "Id", "Name");
            return View();
        }

        // POST: Movie/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ReleaseDate,Genre,ProducerId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movie.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProducerId = new SelectList(db.Producer, "Id", "Name", movie.ProducerId);
            return View(movie);
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProducerId = new SelectList(db.Producer, "Id", "Name", movie.ProducerId);
            return View(movie);
        }

        // POST: Movie/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ReleaseDate,Genre,ProducerId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerId = new SelectList(db.Producer, "Id", "Name", movie.ProducerId);
            return View(movie);
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movie.Find(id);
            db.Movie.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
