using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COMP4941_Term_Project;
using COMP4941_Term_Project.Filters;
using COMP4941_Term_Project.Models;

namespace COMP4941_Term_Project.Controllers
{
    [ErrorLogFilter]
    [CustomActionFilter]
    public class ContactsController : Controller
    {
        private AppContext db;

        public ContactsController()
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

        // GET: Contacts
        [CustomActionFilter]
        public ActionResult Index(Guid? id)
        {
            Session["id"] = id;
            var contact = db.Contacts.Where(x => x.Person.ID == id).Include(x => x.Name).Include(c => c.Branch).ToList();

            return View(contact);
        }

        // GET: Contacts/Details/5
        [CustomActionFilter]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact, FullName name, FullAddress ha)
        {
            if (ModelState.IsValid)
            {
                BranchContext branchDb = new BranchContext("b-" + contact.BranchID);
                var personID = (Guid)Session["id"];
                var person = branchDb.People.Find(personID);
                contact.ID = Guid.NewGuid();
                ha.ID = Guid.NewGuid();

                contact.Person = person;
                contact.Name = name;
                contact.HomeAddress = ha;
                ha.PersonID = contact.ID;

                branchDb.Contacts.Add(contact);
                branchDb.FullAddresses.Add(ha);
                branchDb.SaveChanges(); // save changes to the database
                return RedirectToAction("Index", new { id = personID });
            }

            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", contact.BranchID);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Include(c => c.Name).Include(c => c.HomeAddress).SingleOrDefault(c => c.ID == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", contact.BranchID);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact, FullNameEdit name, FullAddressEdit ha)
        {
            if (ModelState.IsValid)
            {
                var personID = (Guid)Session["id"];
                db.Entry(name.UpdateFullName(db.FullNames.Find(name.fnID))).State = EntityState.Modified;
                db.Entry(ha.UpdateFullAddress(db.FullAddresses.Find(ha.faID))).State = EntityState.Modified;
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = personID });
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", contact.BranchID);
            contact = db.Contacts.Include(c => c.Name).Include(c => c.HomeAddress).SingleOrDefault(c => c.ID == contact.ID);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [CustomActionFilter]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var personID = (Guid)Session["id"];
            Contact contact = db.Contacts.Find(id);
            db.Entry(contact).Reference(c => c.Name).Load();
            db.People.Remove(contact);
            db.FullAddresses.RemoveRange(db.FullAddresses.Where(a => a.PersonID == contact.ID));
            db.SaveChanges();
            return RedirectToAction("Index", new { id = personID });
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
