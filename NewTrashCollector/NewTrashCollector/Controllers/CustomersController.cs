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
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Home()
        {
            var user = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(data => data.UserId.Equals(user)).First();
            ViewBag.Charge = customer.AmountOwed;
            ViewBag.Name = customer.FullName;
            return View(customer);
        }
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        public ActionResult PickUpDetails()
        {
            var user = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(data => data.UserId.Equals(user)).Include(data => data.PickUpDay).First();
            return View(customer);
        }
        public ActionResult PickUpEdit()
        {
            var user = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(data => data.UserId.Equals(user)).Include(data => data.PickUpDay).First();
            ViewBag.PickUpDayId = new SelectList(db.PickUpDays, "Id", "Day");
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PickUpEdit(Customer customer)
        {
            if (ModelState.IsValid)
            {

                var user = User.Identity.GetUserId();
                Customer cust = db.Customers.Where(data => data.UserId.Equals(user)).Include(data => data.PickUpDay).First();
                cust.PickUpDayId = customer.PickUpDayId;
                db.Entry(cust).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PickUpDetails");
            }
            return View(customer);
        }

        public ActionResult ExtraDay()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExtraDay(DatePickerModel date)
        {
            if(date != null)
            {
                var user = User.Identity.GetUserId();
                Customer customer = db.Customers.Where(data => data.UserId.Equals(user)).Include(data => data.PickUpDay).First();
                customer.ExtraPickUpDay = date.dtmDate;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.PickUpDayId = new SelectList(db.PickUpDays, "Id", "Day");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,FullName,PickUpDayId,Street,City,State,ZipCode")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.UserId = User.Identity.GetUserId();
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Home");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,FullName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
