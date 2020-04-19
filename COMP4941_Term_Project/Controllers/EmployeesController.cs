using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using COMP4941_Term_Project.Models;
using COMP4941_Term_Project.Models.ViewModels;
using COMP4941_Term_Project.Filters;
using Microsoft.AspNet.Identity.Owin;

namespace COMP4941_Term_Project.Controllers
{
    [ErrorLogFilter]
    [CustomActionFilter]
    public class EmployeesController : Controller
    {
        private AppContext db;

        public EmployeesController()
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

        // GET: Employees
        [CustomActionFilter]
        public ActionResult Index()
        {
            // in case a user is logged in but the session variable is lost
            if (db == null) return RedirectToAction("Logout", "Account");

            List<Employee> people;
            if (db.GetType() == typeof(AppContext))
            { // connected to application DB and not branch specific DB instance (admin account)
                people = new List<Employee>();
                foreach (var branchID in db.Branches.Select(b => b.ID))
                {
                    BranchContext branchDB = new BranchContext("b-" + branchID);
                    var employeesInBranch = branchDB.Employees
                        .Include(e => e.Branch)
                        .Include(e => e.Name)
                        .Include(e => e.ReportRecipient).ToList();
                    people.AddRange(employeesInBranch);
                }
                return View("ViewAllEmployee", people);
            }
            else
            {
                people = db.Employees.Include(e => e.Branch).Include(e => e.Name).Include(e => e.HomeAddress).Include(e => e.ReportRecipient).ToList();
            }
            return View(people);
        }

        // GET: Employees/Details/5
        [CustomActionFilter]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Include(c => c.Branch).Include(c => c.Name).Include(c => c.HomeAddress).Include(e => e.ReportRecipient).SingleOrDefault(c => c.ID == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        [CustomActionFilter]
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name");
            ViewBag.ReportRecipientID = new SelectList(db.Employees, "ID", "Role");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeCreateViewModel viewModel, FullName name, FullAddress ha, bool[] checkBoxes, System.Web.HttpPostedFileBase image)
        {
            Employee employee = viewModel.Employee;
            RegisterViewModel registerViewModel = viewModel.RegisterViewModel;
            if (ModelState.IsValid)
            {
                employee.ID = Guid.NewGuid();
                ha.ID = Guid.NewGuid();
                employee.Name = name;
                employee.Email = registerViewModel.Email;
                employee.HomeAddress = ha;
                ha.PersonID = employee.ID;
                string authorizedActions = "";
                for (int i = 0; i < checkBoxes.Length; i++)
                {
                    if (checkBoxes[i])
                        authorizedActions += "." + ActionList.LIST[i];
                }
                if (authorizedActions.Length != 0)
                    employee.AuthorizedActions = authorizedActions.Substring(1);
                System.Diagnostics.Debug.WriteLine("Authorized: " + employee.AuthorizedActions);

                if (image != null)
                {
                    System.IO.Stream imgStream = image.InputStream;
                    int size = (int)imgStream.Length;
                    byte[] imgBytes = new byte[size];
                    imgStream.Read(imgBytes, 0, size);
                    employee.Picture = imgBytes;
                }
                else
                {
                    employee.Picture = new byte[] { 0 };
                }

                BranchContext branchDb = new BranchContext("b-" + employee.BranchID);
                branchDb.Employees.Add(employee);
                branchDb.FullAddresses.Add(ha);
                branchDb.SaveChanges();

                ApplicationUser user = new ApplicationUser { Id = employee.ID.ToString(),
                                                             UserName = employee.Email,
                                                             Email = employee.Email,
                                                             BranchID = employee.BranchID };
                var result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().CreateAsync(user, registerViewModel.Password).Result;
                if (result.Succeeded)
                {
                    System.Diagnostics.Debug.WriteLine("user added");
                }

                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", employee.BranchID);
            ViewBag.ReportRecipientID = new SelectList(db.Employees, "ID", "Role", employee.ReportRecipientID);
            EmployeeCreateViewModel model = new EmployeeCreateViewModel { Employee = employee, RegisterViewModel = new RegisterViewModel() };
            return View(model);
        }

        // GET: Employees/Edit/5
        [CustomActionFilter]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Include(c => c.Branch).Include(c => c.Name).Include(c => c.HomeAddress).Include(e => e.ReportRecipient).SingleOrDefault(c => c.ID == id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", employee.BranchID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "Title", employee.ID);
            ViewBag.ReportRecipientID = new SelectList(db.Employees, "ID", "Role", employee.ReportRecipientID);
           
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee, FullNameEdit name, FullAddressEdit ha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(name.UpdateFullName(db.FullNames.Find(name.fnID))).State = EntityState.Modified;
                db.Entry(ha.UpdateFullAddress(db.FullAddresses.Find(ha.faID))).State = EntityState.Modified;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
         
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", employee.BranchID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "Title", employee.ID);
            ViewBag.ReportRecipientID = new SelectList(db.Employees, "ID", "Role", employee.ReportRecipientID);
            employee = db.Employees.Include(e => e.Name).Include(e => e.HomeAddress).SingleOrDefault(e => e.ID == employee.ID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        [CustomActionFilter]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Include(c => c.Branch).Include(c => c.Name).Include(c => c.HomeAddress).Include(e => e.ReportRecipient).SingleOrDefault(c => c.ID == id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Employee employee = (Employee)db.People.Find(id);
            db.Entry(employee).Reference(e => e.Name).Load();
            db.People.Remove(employee);
            db.FullAddresses.RemoveRange(db.FullAddresses.Where(a => a.PersonID == employee.ID));
            db.SaveChanges();
            ApplicationUserManager manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindByNameAsync(employee.Email).Result;
            var result = manager.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                System.Diagnostics.Debug.WriteLine("User account with email: \"" + employee.Email + "\" deleted.");
            } else
            {
                System.Diagnostics.Debug.WriteLine("Unable to delete user.");
            }
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
