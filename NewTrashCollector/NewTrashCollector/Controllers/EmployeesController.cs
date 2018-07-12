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
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Home()
        {
            var user = User.Identity.GetUserId();
            var emp = db.Employees.Where(data => data.UserId.Equals(user)).First();
            
            var customers = db.Customers.Where(data => data.ZipCode.Equals(emp.ZipCode)).Include(data => data.PickUpDay).ToList();
            for(int i = 0; i< customers.Count;i++)
            {
                var dayOfWeekNow = DateTime.Now.DayOfWeek.ToString();
                if ((customers[i].ExtraPickUpDay.HasValue && !(DateTime.Now.Date == customers[i].ExtraPickUpDay.Value.Date)) && (!dayOfWeekNow.Equals(customers[i].PickUpDay.Day.ToString())))
                {
                    customers.Remove(customers[i]);
                    i--;
                }
            }
            for(int i = 0; i < customers.Count;i++)
            {
                var suspended = db.SuspendedTimes.ToList();
                for(int j = 0; j < suspended.Count; j++)
                {
                    if(suspended[j].CustomerId != customers[i].Id)
                    {
                        suspended.Remove(suspended[j]);
                        j--;
                    }
                }
                if(suspended.Count == 1)
                {
                    var sus = suspended[0];
                    if (sus.Start.Date <= DateTime.Now.Date && sus.End.Date > DateTime.Now.Date)
                    {
                        customers.Remove(customers[i]);
                        i--;
                    }
                }
                
            }
            return View(customers);
        }
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.User);
            return View(employees.ToList());
        }
        public ActionResult Confirm(int id)
        {
            var customer = db.Customers.Where(data => data.Id == id).First();
            customer.AmountOwed += 5;
            db.SaveChanges();
            return RedirectToAction("Home");
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            var user = User.Identity.GetUserId();
            Employee emp = new Employee();
            emp.UserId = user;
            db.Employees.Add(emp);
            db.SaveChanges();
            return RedirectToAction("Home");
        }


        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            employee.User = db.Users.Where(data => data.Id.Equals(employee.UserId)).First();
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,User,ZipCode")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.UserId = db.Users.Where(data => data.UserName.Equals(employee.User.UserName)).Select(data => data.Id).First();
                var emp = db.Employees.Where(data => data.Id == employee.Id).First();
                emp.ZipCode = employee.ZipCode;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
