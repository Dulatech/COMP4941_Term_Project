using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using COMP4941_Term_Project.Models;
using COMP4941_Term_Project.Filters;

namespace COMP4941_Term_Project.Controllers
{
    [CustomActionFilter]
    public class CustomersController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Customers
        [CustomActionFilter]
        public ActionResult Index()
        {
            var people = db.Customers.Include(c => c.Branch).Include(c => c.Name);
            return View(people.ToList());
        }

        // GET: Customers/Details/5
        [CustomActionFilter]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = (Customer)db.People.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name");
            ViewBag.ID = new SelectList(db.FullNames, "ID", "Title");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BranchID,Picture")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.ID = Guid.NewGuid();
                db.People.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", customer.BranchID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "Title", customer.ID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = (Customer)db.People.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", customer.BranchID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "Title", customer.ID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BranchID,Picture")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", customer.BranchID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "Title", customer.ID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        [CustomActionFilter]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = (Customer)db.People.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Customer customer = (Customer)db.People.Find(id);
            db.People.Remove(customer);
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
