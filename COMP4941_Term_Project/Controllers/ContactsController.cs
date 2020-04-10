using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COMP4941_Term_Project;
using COMP4941_Term_Project.Models;

namespace COMP4941_Term_Project.Controllers
{
    public class ContactsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Contacts
        public ActionResult Index(Guid? id)
        {
            Session["id"] = id;
            var contacts = db.Contacts.Where(x => x.Person.ID == id).Include(x => x.Name).Include(c => c.Branch).ToList();

            return View(contacts);
        }

        // GET: Contacts/Details/5
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
        public ActionResult Create([Bind(Include = "ID,BranchID,Picture,RelationPrimary,RelationSecondary,Description")] Contact contact,
                                    [Bind(Include = "Title, FirstName, LastName")] FullName name,
                                    [Bind(Include = "Street, City, Province, Country, PostalCode")] FullAddress ha)
        {
            if (ModelState.IsValid)
            {
                var personID = (Guid)Session["id"];
                var person = db.People.Find(personID);
                contact.ID = Guid.NewGuid();
                name.ID = Guid.NewGuid();
                ha.ID = Guid.NewGuid();
                contact.Person = person;
                contact.Name = name;
                contact.HomeAddress = ha;
                Guid? branchID = contact.BranchID;
                // People table references Branch table for FK, but Branch table doesn't exist in the BranchDbContext
                // which causes INSERT error. Thus BranchID for this employee is set to null in the Branch's DB,
                // but not in AspNetUsers table
                contact.BranchID = null;
                BranchContext branchDb = new BranchContext("b-" + branchID);
                db.People.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = personID });
            }

            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", contact.BranchID);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(Guid? id)
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
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", contact.BranchID);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BranchID,Picture,RelationPrimary,RelationSecondary,Description")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", contact.BranchID);
            return View(contact);
        }

        // GET: Contacts/Delete/5
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
            Contact contact = db.Contacts.Find(id);
            db.People.Remove(contact);
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
