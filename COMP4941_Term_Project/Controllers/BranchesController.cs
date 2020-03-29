using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using COMP4941_Term_Project.Models;
using COMP4941_Term_Project.Filters;

namespace COMP4941_Term_Project.Controllers
{
    [CustomActionFilter]
    public class BranchesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Branches
        [CustomActionFilter]
        public ActionResult Index()
        {
            ViewBag.Parents = db.Branches.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View(db.Branches.ToList());
        }

        // GET: Branches/Details/5
        [CustomActionFilter]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Branches/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            ViewBag.PossibleParents = db.Branches.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ParentID,Name,Street,City,Province,Country,PostalCode,Phone,Fax,Email,Website")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                branch.ID = Guid.NewGuid();
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        // GET: Branches/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ParentID,Name,Street,City,Province,Country,PostalCode,Phone,Fax,Email,Website")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        // GET: Branches/Delete/5
        [CustomActionFilter]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
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
