using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using VetAdminSystem.Models;

namespace VetAdminSystem.Controllers
{
    public class PatientsController : Controller
    {
        private VetAdminSystemDBEntities1 db = new VetAdminSystemDBEntities1();

        // GET: Patients
        public ActionResult Index(string SearchByList, string searchString)
        {
            var patients = db.Patients.Include(p => p.Client);
            ViewBag.SearchByList = new SelectList(Common.UsefulMethods.GetModelProperties<Patient>());

            if (!String.IsNullOrEmpty(searchString))
            {
                if (SearchByList == "Client")
                {
                    patients = patients.Where(p => p.Client.Name == searchString);
                }

                if (SearchByList == "ClientId")
                {
                    if (Int32.TryParse(searchString, out int x))
                    {
                        patients = patients.Where(p => p.ClientId == x);
                    }
                    else
                    {
                        patients = patients.Where(p => p.ClientId < 0);
                    }
                }

                if (SearchByList == "Age")
                {
                    if (Int32.TryParse(searchString, out int x))
                    {
                        patients = patients.Where(p => p.Age == x);
                    }
                    else
                    {
                        patients = patients.Where(p => p.Age < 0);
                    }
                }

                if (SearchByList == "Breed")
                {
                    patients = patients.Where(p => p.Breed == searchString);
                }

                if (SearchByList == "Name")
                {
                    patients = patients.Where(p => p.Name == searchString);
                }

                if (SearchByList == "PatientId")
                {
                    if (Int32.TryParse(searchString, out int x))
                    {
                        patients = patients.Where(p => p.PatientId == x);
                    }
                    else
                    {
                        patients = patients.Where(p => p.PatientId < 0);
                    }
                }

                if (SearchByList == "Perscription")
                {
                    patients = patients.Where(p => p.Perscriptions.Where(pers => pers.Medication.Name == searchString).Count() > 0);
                }
            }

            return View(patients.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,Name,Breed,Age")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", patient.ClientId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", patient.ClientId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientId,ClientId,Name,Breed,Age")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", patient.ClientId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
