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
        private AppContext db;

        public CustomersController()
        {
            // set DbContext specific to the branch of the logged in user
            object branchID = System.Web.HttpContext.Current.Session["branch"];
            if (branchID != null)
            {
                if (branchID.ToString() == "admin")
                    db = new AppContext();
                else
                    db = new BranchContext("b-" + branchID);
            }
        }

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
        public ActionResult Create(Customer customer, // Input field values of from the client side's html form
                                   FullName name,     // that are associated with these entities are automatically 
                                   FullAddress ha)    // binded to these objects ([BindAttribute] is optional)
        {
            if (ModelState.IsValid)
            {
                customer.ID = Guid.NewGuid(); // Create new Guid's for ID,
                name.ID = Guid.NewGuid();     // FullName's ID,
                ha.ID = Guid.NewGuid();       // and FullAddress' ID
                customer.Name = name;         // Set Customer's Name entity to the FullName parameter
                customer.HomeAddress = ha;    // same for FullAddress
                // connect to the instance of database specific to this customer's branch
                BranchContext branchDb = new BranchContext("b-" + customer.BranchID);
                // Add this customer to DBSet. All the associated values are also inserted to
                // their corresonding tables automatically as well (People, FullName, and FullAddress)
                branchDb.Customers.Add(customer);
                branchDb.SaveChanges(); // save changes to the database
                return RedirectToAction("Index"); // redirects user to the Index View
            }
            // if ModelState is invalid, sends the same page as response (stays on the same page)
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
            Customer customer = db.Customers.Include(c => c.Name).Include(c => c.HomeAddress).SingleOrDefault(c => c.ID == id);
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
        public ActionResult Edit(Customer customer,
                                 FullNameEdit name,  // ViewModels
                                 FullAddressEdit ha) // for editing
        {
            if (ModelState.IsValid)
            {
                // For name and address, the input values form the client side is binded to a view model,
                // then those information is used to set the values of the actual name and address object from
                // the database with the same ID. Then, we set the state of the entities for Customer,
                // FullName and FullAddress to Modified
                db.Entry(name.UpdateFullName(db.FullNames.Find(name.fnID))).State = EntityState.Modified;
                db.Entry(ha.UpdateFullAddress(db.FullAddresses.Find(ha.faID))).State = EntityState.Modified;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges(); // save modifications to the database
                return RedirectToAction("Index");
            }
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
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
