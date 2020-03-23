﻿using System;
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
    public class EmployeesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Employees
        public ActionResult Index()
        {
            //var people = db.People.Include(e => e.Branch).Include(e => e.HomeAddress).Include(e => e.Name).Include(e => e.WorkAddress).Include(e => e.EmergencyContact).Include(e => e.ReportRecipient);
            var people = db.People.Include(e => e.Branch).Include(e => e.HomeAddress).Include(e => e.Name).Include(e => e.WorkAddress);
            return View(people.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = (Employee)db.People.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Branches, "ID", "Name");
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "RoomNo");
            ViewBag.ID = new SelectList(db.FullNames, "ID", "Title");
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "RoomNo");
            ViewBag.EmergencyContactID = new SelectList(db.People, "ID", "RelationPrimary");
            ViewBag.ReportRecipientID = new SelectList(db.People, "ID", "Role");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BranchID,Picture,EmergencyContactID,ReportRecipientID,Role,JobTitle,EmploymentStatus,ReportsTo,Groups,Description,Password")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ID = Guid.NewGuid();
                db.People.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Branches, "ID", "Name", employee.ID);
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "RoomNo", employee.ID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "Title", employee.ID);
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "RoomNo", employee.ID);
            ViewBag.EmergencyContactID = new SelectList(db.People, "ID", "RelationPrimary", employee.EmergencyContactID);
            ViewBag.ReportRecipientID = new SelectList(db.People, "ID", "Role", employee.ReportRecipientID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = (Employee)db.People.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Branches, "ID", "Name", employee.ID);
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "RoomNo", employee.ID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "Title", employee.ID);
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "RoomNo", employee.ID);
            ViewBag.EmergencyContactID = new SelectList(db.People, "ID", "RelationPrimary", employee.EmergencyContactID);
            ViewBag.ReportRecipientID = new SelectList(db.People, "ID", "Role", employee.ReportRecipientID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BranchID,Picture,EmergencyContactID,ReportRecipientID,Role,JobTitle,EmploymentStatus,ReportsTo,Groups,Description,Password")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Branches, "ID", "Name", employee.ID);
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "RoomNo", employee.ID);
            ViewBag.ID = new SelectList(db.FullNames, "ID", "Title", employee.ID);
            ViewBag.ID = new SelectList(db.FullAddresses, "ID", "RoomNo", employee.ID);
            ViewBag.EmergencyContactID = new SelectList(db.People, "ID", "RelationPrimary", employee.EmergencyContactID);
            ViewBag.ReportRecipientID = new SelectList(db.People, "ID", "Role", employee.ReportRecipientID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = (Employee)db.People.Find(id);
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
            db.People.Remove(employee);
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
