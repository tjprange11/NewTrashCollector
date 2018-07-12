using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewTrashCollector.Models;
using Microsoft.AspNet.Identity;

namespace NewTrashCollector.Controllers
{
    public class SuspendedTimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SuspendedTimes
        public ActionResult Index()
        {
            var suspendedTimes = db.SuspendedTimes.Include(s => s.Customer);
            return View(suspendedTimes.ToList());
        }

        // GET: SuspendedTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendedTime suspendedTime = db.SuspendedTimes.Find(id);
            if (suspendedTime == null)
            {
                return HttpNotFound();
            }
            return View(suspendedTime);
        }

        // GET: SuspendedTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuspendedTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,Start,End")] SuspendedTime suspendedTime)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();
                Customer customer = db.Customers.Where(data => data.UserId.Equals(user)).First();
                suspendedTime.CustomerId = customer.Id;
                db.SuspendedTimes.Add(suspendedTime);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "UserId", suspendedTime.CustomerId);
            return View(suspendedTime);
        }

        // GET: SuspendedTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendedTime suspendedTime = db.SuspendedTimes.Find(id);
            if (suspendedTime == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "UserId", suspendedTime.CustomerId);
            return View(suspendedTime);
        }

        // POST: SuspendedTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,Start,End")] SuspendedTime suspendedTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suspendedTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "UserId", suspendedTime.CustomerId);
            return View(suspendedTime);
        }

        // GET: SuspendedTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendedTime suspendedTime = db.SuspendedTimes.Find(id);
            if (suspendedTime == null)
            {
                return HttpNotFound();
            }
            return View(suspendedTime);
        }

        // POST: SuspendedTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuspendedTime suspendedTime = db.SuspendedTimes.Find(id);
            db.SuspendedTimes.Remove(suspendedTime);
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
