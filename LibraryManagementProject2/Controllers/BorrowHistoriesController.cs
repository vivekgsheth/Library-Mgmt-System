using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementProject2.Models;

namespace LibraryManagementProject2.Controllers
{
    [Authorize]
    public class BorrowHistoriesController : Controller
    {
        private BooksDb2 db = new BooksDb2();

        // GET: BorrowHistories
        public ActionResult Index()
        {
            //var borrowHistories = db.BorrowHistories.Include(b => b.Books).Include(b => b.User);
            //return View(borrowHistories.ToList());

            var bh = db.BorrowHistories.Where(x => x.);

        }

        // GET: BorrowHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
        }

        // GET: BorrowHistories/Create
        public ActionResult Create()
        {

            ViewBag.BookId = new SelectList(db.Mybooks, "BookId", "BookId");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserId");
            return View();
        }

        // POST: BorrowHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BorrowHistoryId,BookId,UserId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.BorrowHistories.Add(borrowHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Mybooks, "BookId", "BookId", borrowHistory.BookId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserId", borrowHistory.UserId);
            return View(borrowHistory);
        }
        [Authorize(Roles ="Admin")]
        // GET: BorrowHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Mybooks, "BookId", "Title", borrowHistory.BookId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", borrowHistory.UserId);
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "BorrowHistoryId,BookId,UserId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrowHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Mybooks, "BookId", "Title", borrowHistory.BookId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", borrowHistory.UserId);
            return View(borrowHistory);
        }

        [Authorize(Roles = "Admin")]
        // GET: BorrowHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            db.BorrowHistories.Remove(borrowHistory);
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
