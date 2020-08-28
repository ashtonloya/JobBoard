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
    public class ApplicationController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Applications
        public ActionResult Index()
        {
            var applications = db.Applications.Include(a => a.ApplicationStatu).Include(a => a.UserDetail).Include(a => a.OpenPosition);
            return View(applications.ToList());
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationStatusId = new SelectList(db.ApplicationStatus, "ApplicationStatusId", "StatusName");
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName");
            ViewBag.ApplicationId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationId,UserId,OpenPostionId,ApplicationDate,ManagerNotes,ApplicationStatusId,ResumeFilename")] Application application, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                //use a default image if none is provided 
                string imgName = "noPDF.pdf";
                if (logo != null)//Your HttpPostedFilebase Object that should be 
                                 //added to the action !=null
                {
                    //get image and assing to variable 
                    imgName = logo.FileName;

                    //declare and assign ext value
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    //declare a list of valid extensions
                    string[] goodExts = { ".pdf" };

                    //check the ext variable (toLower()) against the valid list 
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        //if it is in the list rename using a guid
                        logo.SaveAs(Server
                            .MapPath("~/Content/Images" + imgName));

                        //save to the webserver 
                        logo.SaveAs(Server.MapPath("~Content/Images/" + imgName));
                    }
                    else
                    {
                        imgName = "noPDF.pdf";
                    }
                }

                //no matter what add the imageName to the obejct
                application.ResumeFilename = imgName;
                #endregion
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationStatusId = new SelectList(db.ApplicationStatus, "ApplicationStatusId", "StatusName", application.ApplicationStatusId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", application.UserId);
            ViewBag.ApplicationId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId", application.ApplicationId);
            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationStatusId = new SelectList(db.ApplicationStatus, "ApplicationStatusId", "StatusName", application.ApplicationStatusId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", application.UserId);
            ViewBag.ApplicationId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId", application.ApplicationId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationId,UserId,OpenPostionId,ApplicationDate,ManagerNotes,ApplicationStatusId,ResumeFilename")] Application application, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                if (logo != null)//Your HttpPostedFileBase Object that should be added to the action != null
                {
                    //get image and assign to variable 
                    string imgName = logo.FileName;

                    //declare and assign ext value
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    //declare a list of valid extension 
                    string[] goodExts = { ".pdf" };

                    //check the ext variable (toLower()) aginst the valid list
                    if (goodExts.Contains(ext.ToLower()))
                    { 

                        //save to the webserver 
                        logo.SaveAs(Server.MapPath("~Content/Images/" + imgName));

                        //only save if the image meets criteria imageName to the object
                        application.ResumeFilename = imgName;
                    }
                    #endregion
                    db.Entry(application).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ApplicationStatusId = new SelectList(db.ApplicationStatus, "ApplicationStatusId", "StatusName", application.ApplicationStatusId);
                ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", application.UserId);
                ViewBag.ApplicationId = new SelectList(db.OpenPositions, "OpenPositionId", "OpenPositionId", application.ApplicationId);
                return View(application);
            }

            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);

            if (application.ResumeFilename != null && application.ResumeFilename != "NoImage.png")
            {
                //remove the original file from the edit view
                System.IO.File.Delete(Server.MapPath("~/Content/Images/" + Session["currentImage"].ToString()));
            }

            db.Applications.Remove(application);
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