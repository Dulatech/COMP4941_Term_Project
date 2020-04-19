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
    [ErrorLogFilter]
    [CustomActionFilter]
    public class BranchesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Branches
        [CustomActionFilter]
        public ActionResult Index()
        {
            ViewBag.Parents = db.Branches.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View("Index", db.Branches.ToList());
        }

        [CustomActionFilter]
        public ActionResult SubBranchIndex(Guid? id)
        {
            ViewBag.Parents = db.Branches.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View("Index", db.Branches.Where(b => b.ParentID == id).ToList());
        }

        // GET: Branches/Details/5
        [CustomActionFilter]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Include(b => b.SubBranches).SingleOrDefault(b => b.ID == id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View("Details", branch);
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
        public ActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                branch.ID = Guid.NewGuid();
                db.Branches.Add(branch);
                db.SaveChanges();
                BranchContext branchDB = new BranchContext("b-" + branch.ID);
                branchDB.Branches.Add(branch);
                branchDB.SaveChanges();
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
            ViewBag.PossibleParents = db.Branches.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                // connect with database instance for this branch
                BranchContext branchContext = new BranchContext("b-" + branch.ID);
                branchContext.Entry(branch).State = EntityState.Modified; // set modified state
                branchContext.SaveChanges();
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
