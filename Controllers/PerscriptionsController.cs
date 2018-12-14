using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VetAdminSystem.Models;

namespace VetAdminSystem.Controllers
{
    public class PerscriptionsController : Controller
    {
        private VetAdminSystemDBEntities1 db = new VetAdminSystemDBEntities1();

        // GET: Perscriptions
        public ActionResult Index()
        {
            var perscriptions = db.Perscriptions.Include(p => p.Medication).Include(p => p.Patient);
            return View(perscriptions.ToList());
        }

        // GET: Perscriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perscription perscription = db.Perscriptions.Find(id);
            if (perscription == null)
            {
                return HttpNotFound();
            }
            return View(perscription);
        }

        // GET: Perscriptions/Create
        public ActionResult Create()
        {
            ViewBag.MedicationId = new SelectList(db.Medications, "Id", "Name");
            ViewBag.PetId = new SelectList(db.Patients, "PatientId", "Name");
            return View();
        }

        // POST: Perscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetId,MedicationId,Quantity,Direction,Id")] Perscription perscription)
        {
            if (ModelState.IsValid)
            {
                db.Perscriptions.Add(perscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MedicationId = new SelectList(db.Medications, "Id", "Name", perscription.MedicationId);
            ViewBag.PetId = new SelectList(db.Patients, "PatientId", "Name", perscription.PetId);
            return View(perscription);
        }

        // GET: Perscriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perscription perscription = db.Perscriptions.Find(id);
            if (perscription == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicationId = new SelectList(db.Medications, "Id", "Name", perscription.MedicationId);
            ViewBag.PetId = new SelectList(db.Patients, "PatientId", "Name", perscription.PetId);
            return View(perscription);
        }

        // POST: Perscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetId,MedicationId,Quantity,Direction,Id")] Perscription perscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicationId = new SelectList(db.Medications, "Id", "Name", perscription.MedicationId);
            ViewBag.PetId = new SelectList(db.Patients, "PatientId", "Name", perscription.PetId);
            return View(perscription);
        }

        // GET: Perscriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perscription perscription = db.Perscriptions.Find(id);
            if (perscription == null)
            {
                return HttpNotFound();
            }
            return View(perscription);
        }

        // POST: Perscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Perscription perscription = db.Perscriptions.Find(id);
            db.Perscriptions.Remove(perscription);
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
