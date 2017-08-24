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
    public class ListItemController : Controller
    {
        private DataContext.DataContext db = new DataContext.DataContext();

        // GET: ListItem
        public ActionResult Index()
        {
            var listItem = db.ListItem.Include(l => l.DefaultPriority);
            return View(listItem.ToList());
        }

        // GET: ListItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListItemModel listItemModel = db.ListItem.Find(id);
            if (listItemModel == null)
            {
                return HttpNotFound();
            }
            return View(listItemModel);
        }

        // GET: ListItem/Create
        public ActionResult Create()
        {
            ViewBag.PriorityId = new SelectList(db.Priority, "PriorityId", "Description");
            return View();
        }

        // POST: ListItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ListItemId,ItemName,PriorityId,Amount")] ListItemModel listItemModel)
        {
            if (ModelState.IsValid)
            {
                db.ListItem.Add(listItemModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PriorityId = new SelectList(db.Priority, "PriorityId", "Description", listItemModel.PriorityId);
            return View(listItemModel);
        }

        // GET: ListItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListItemModel listItemModel = db.ListItem.Find(id);
            if (listItemModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.PriorityId = new SelectList(db.Priority, "PriorityId", "Description", listItemModel.PriorityId);
            return View(listItemModel);
        }

        // POST: ListItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ListItemId,ItemName,PriorityId,Amount")] ListItemModel listItemModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listItemModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PriorityId = new SelectList(db.Priority, "PriorityId", "Description", listItemModel.PriorityId);
            return View(listItemModel);
        }

        // GET: ListItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListItemModel listItemModel = db.ListItem.Find(id);
            if (listItemModel == null)
            {
                return HttpNotFound();
            }
            return View(listItemModel);
        }

        // POST: ListItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListItemModel listItemModel = db.ListItem.Find(id);
            db.ListItem.Remove(listItemModel);
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
