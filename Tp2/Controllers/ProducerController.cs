using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tp2.Models;

namespace Tp2.Controllers
{
    public class ProducerController : Controller
    {
        CinemaEntities ce = new CinemaEntities();
        // GET: Producer
        public ActionResult Index()
        {
            return View(ce.Producer.ToList());
        }

        public ActionResult ProdsAndTheirMovies()
        {
            return View(ce.Producer.ToList());
        }


        public ActionResult ProdsAndTheirMovies_UsingModel(int id)
        {
            return View(from x in ce.Producer join y in ce.Movie.ToList() on x.Id equals (y.Producer.Id) where (x.Id==id)select new ProdMovie { mTitle = y.Title, mGenre = y.Genre, pName = x.Name, pNat = x.Nationality });
        }


        // GET: Producer/Details/5
        public ActionResult Details(int id)
        {
            return View(ce.Producer.Find(id));
        }

        // GET: Producer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producer/Create
        [HttpPost]
        public ActionResult Create(Producer p)
        {
            try
            {
                // TODO: Add insert logic here
                ce.Producer.Add(p);
                ce.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producer/Edit/5
        public ActionResult Edit(int id)
        {

            return View(ce.Producer.Find(id));
        }

        // POST: Producer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Producer p)
        {
            try
            {
                // TODO: Add update logic here
                ce.Entry(p).State = System.Data.Entity.EntityState.Modified;
                ce.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producer/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ce.Producer.Find(id));
        }

        // POST: Producer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Producer p)
        {
            try
            {
                // TODO: Add delete logic here
                ce.Producer.Remove(ce.Producer.Find(id));
                foreach(var m in ce.Movie.Where(f => f.ProducerId == id))
                {
                    ce.Movie.Remove(m);
                }
                ce.SaveChanges();
                //autre methode ce.Entry(p).State = System.Data.Entity.EntityState.Deleted;
                ce.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
