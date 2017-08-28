using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PendingListItems.DataContext;
using PendingListItems.Models;
using PendingListItems.BusinessClasses;

namespace PendingListItems.Controllers
{
    public class ConceptController : Controller
    {
        private DataContext.DataContext db = new DataContext.DataContext();
        private Concepts concepts = new Concepts();

        // GET: Concept
        public ActionResult Index(int? SummaryId)
        {
            var concept = db.Concept.Include(c => c.Summary).Where(t => t.SummaryId == SummaryId).ToList();
            ViewBag.SummaryId = SummaryId;
            ViewBag.SummaryName = db.Summary.Where(t => t.SummaryId == SummaryId).FirstOrDefault().SummaryName ?? "No name";
            return View(concept);
        }

        // GET: Concept/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConceptModel conceptModel = db.Concept.Find(id);
            if (conceptModel == null)
            {
                return HttpNotFound();
            }
            return View(conceptModel);
        }

        // GET: Concept/Create
        public ActionResult Create(int? SummaryId)
        {
            ViewBag.SummaryId = new SelectList(db.Summary.Where(t => t.SummaryId == SummaryId), "SummaryId", "SummaryName");
            return View();
        }

        // POST: Concept/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConceptId,ConceptName,Amount,Payd,Prepayment,AmountPayable,PayDate,CreationDate,LastModificationDate,SummaryId")] ConceptModel conceptModel)
        {
            if (ModelState.IsValid)
            {
                conceptModel.CreationDate = DateTime.Now;
                conceptModel.LastModificationDate = DateTime.Now;
                db.Concept.Add(conceptModel);
                db.SaveChanges();

                concepts.UpdateAmountsSummary(conceptModel);

                return RedirectToAction("Index", new { SummaryId = conceptModel.SummaryId });
            }

            ViewBag.SummaryId = new SelectList(db.Summary, "SummaryId", "SummaryName", conceptModel.SummaryId);
            return View(conceptModel);
        }

        // GET: Concept/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConceptModel conceptModel = db.Concept.Find(id);
            if (conceptModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Summaries = new SelectList(db.Summary, "SummaryId", "SummaryName", conceptModel.SummaryId);
            ViewBag.SummaryId = conceptModel.SummaryId;
            return View(conceptModel);
        }

        // POST: Concept/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConceptId,ConceptName,Amount,Payd,Prepayment,AmountPayable,PayDate,CreationDate,LastModificationDate,SummaryId")] ConceptModel conceptModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conceptModel).State = EntityState.Modified;
                db.SaveChanges();

                concepts.UpdateAmountsSummary(conceptModel);

                return RedirectToAction("Index", new { SummaryId = conceptModel.SummaryId });
            }
            ViewBag.SummaryId = new SelectList(db.Summary, "SummaryId", "SummaryName", conceptModel.SummaryId);
            return View(conceptModel);
        }

        // GET: Concept/Delete/5
        public ActionResult Delete(int? id, int? SummaryId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConceptModel conceptModel = db.Concept.Find(id);
            if (conceptModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.SummaryId = SummaryId;
            return View(conceptModel);
        }

        // POST: Concept/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? SummaryId)
        {
            ConceptModel conceptModel = db.Concept.Find(id);
            db.Concept.Remove(conceptModel);
            db.SaveChanges();
            return RedirectToAction("Index", new { SummaryId = SummaryId });
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
