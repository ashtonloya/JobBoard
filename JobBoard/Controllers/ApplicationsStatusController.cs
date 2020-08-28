﻿using System;
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
    public class ApplicationsStatusController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: ApplicationsStatus
        public ActionResult Index()
        {
            return View(db.ApplicationStatus.ToList());
        }

        // GET: ApplicationsStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationStatu applicationStatu = db.ApplicationStatus.Find(id);
            if (applicationStatu == null)
            {
                return HttpNotFound();
            }
            return View(applicationStatu);
        }

        // GET: ApplicationsStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationsStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationStatusId,StatusName,StatusDescription")] ApplicationStatu applicationStatu)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationStatus.Add(applicationStatu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationStatu);
        }

        // GET: ApplicationsStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationStatu applicationStatu = db.ApplicationStatus.Find(id);
            if (applicationStatu == null)
            {
                return HttpNotFound();
            }
            return View(applicationStatu);
        }

        // POST: ApplicationsStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationStatusId,StatusName,StatusDescription")] ApplicationStatu applicationStatu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationStatu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationStatu);
        }

        // GET: ApplicationsStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationStatu applicationStatu = db.ApplicationStatus.Find(id);
            if (applicationStatu == null)
            {
                return HttpNotFound();
            }
            return View(applicationStatu);
        }

        // POST: ApplicationsStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationStatu applicationStatu = db.ApplicationStatus.Find(id);
            db.ApplicationStatus.Remove(applicationStatu);
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
