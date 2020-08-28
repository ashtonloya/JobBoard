using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;

namespace JobBoard.Controllers
{
    public class OpenPositionController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: OpenPosition
        public ActionResult Index()
        {
            var openPositions = db.OpenPositions.Include(o => o.Application).Include(o => o.Location).Include(o => o.Position).Include(o => o.UserDetail);
            return View(openPositions.ToList());
        }

        // GET: OpenPosition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // GET: OpenPosition/Create
        public ActionResult Create()
        {
            ViewBag.OpenPositionId = new SelectList(db.Applications, "ApplicationId", "ManagerNotes");
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber");
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title");
            ViewBag.LocationId = new SelectList(db.UserDetails, "UserId", "FirstName");
            return View();
        }

        // POST: OpenPosition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpenPositionId,LocationId,PositionId")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.OpenPositions.Add(openPosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OpenPositionId = new SelectList(db.Applications, "ApplicationId", "ManagerNotes", openPosition.OpenPositionId);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            ViewBag.LocationId = new SelectList(db.UserDetails, "UserId", "FirstName", openPosition.LocationId);
            return View(openPosition);
        }

        // GET: OpenPosition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            ViewBag.OpenPositionId = new SelectList(db.Applications, "ApplicationId", "ManagerNotes", openPosition.OpenPositionId);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            ViewBag.LocationId = new SelectList(db.UserDetails, "UserId", "FirstName", openPosition.LocationId);
            return View(openPosition);
        }

        // POST: OpenPosition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpenPositionId,LocationId,PositionId")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openPosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OpenPositionId = new SelectList(db.Applications, "ApplicationId", "ManagerNotes", openPosition.OpenPositionId);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            ViewBag.LocationId = new SelectList(db.UserDetails, "UserId", "FirstName", openPosition.LocationId);
            return View(openPosition);
        }

        // GET: OpenPosition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // POST: OpenPosition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpenPosition openPosition = db.OpenPositions.Find(id);
            db.OpenPositions.Remove(openPosition);
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

        public ActionResult Apply(int OpenPositionID)
        {
                Application appliction = new Application();
                db.SaveChanges();
                return RedirectToAction("Index");
        }

    }
}
