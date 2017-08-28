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

namespace PendingListItems.Controllers
{
    public class SummaryController : Controller
    {
        private DataContext.DataContext db = new DataContext.DataContext();

        // GET: Summary
        public ActionResult Index()
        {
            var summary = db.Summary.Include(s => s.Periods).ToList();
            return View(summary);
        }

        // GET: Summary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SummaryModel summaryModel = db.Summary.Find(id);
            if (summaryModel == null)
            {
                return HttpNotFound();
            }
            return View(summaryModel);
        }

        // GET: Summary/Create
        public ActionResult Create()
        {
            ViewBag.PeriodId = new SelectList(db.Period, "PeriodId", "PeriodName");
            return View();
        }

        // POST: Summary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SummaryId,SummaryName,UpdatedAmount,PendingAmount,TotalAmount,CreationDate,LastModificationDate,PeriodId")] SummaryModel summaryModel)
        {
            if (ModelState.IsValid)
            {
                summaryModel.CreationDate = DateTime.Now;
                summaryModel.LastModificationDate = DateTime.Now;
                db.Summary.Add(summaryModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PeriodId = new SelectList(db.Period, "PeriodId", "PeriodName", summaryModel.PeriodId);
            return View(summaryModel);
        }

        // GET: Summary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SummaryModel summaryModel = db.Summary.Find(id);
            if (summaryModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.PeriodId = new SelectList(db.Period, "PeriodId", "PeriodName", summaryModel.PeriodId);
            return View(summaryModel);
        }

        // POST: Summary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SummaryId,SummaryName,LastModificationDate,PeriodId")] SummaryModel summaryModel)
        {
            if (ModelState.IsValid)
            {
                summaryModel.LastModificationDate = DateTime.Now;
                db.Summary.Attach(summaryModel);
                db.Entry(summaryModel).Property(x => x.SummaryName).IsModified = true;
                db.Entry(summaryModel).Property(x => x.LastModificationDate).IsModified = true;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.PeriodId = new SelectList(db.Period, "PeriodId", "PeriodName", summaryModel.PeriodId);
            return View(summaryModel);
        }

        // GET: Summary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SummaryModel summaryModel = db.Summary.Find(id);
            if (summaryModel == null)
            {
                return HttpNotFound();
            }
            return View(summaryModel);
        }

        // POST: Summary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SummaryModel summaryModel = db.Summary.Find(id);
            db.Summary.Remove(summaryModel);
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
