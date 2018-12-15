using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementProject2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryManagementProject2.Controllers
{
    [Authorize]
   
    //[Authorize(Roles = "Administrator,Users")]
    public class BooksController : Controller
    {
        ApplicationDbContext db1 = new ApplicationDbContext();
        private BooksDb2 db = new BooksDb2();

        // GET: Books
        public ActionResult Index2(string bkname, string authname, string sortOrder)
        {
            ViewBag.Auth = isAdminUser();

            var books = from b in db.Mybooks
                        where b.qty != 0
                        select b;
            if (!string.IsNullOrEmpty(bkname))
            {
                books = books.Where(x => x.Title.Contains(bkname));

            }

            if (!string.IsNullOrEmpty(authname))
            {
                books = books.Where(x => x.Author.Contains(authname));

            }

            ViewBag.TitleSortParam = sortOrder == "Title" ? "Title_desc" : "Title";
            ViewBag.AuthorParam = sortOrder == "Author" ? "Author_desc" : "Author";
            switch (sortOrder)
            {
                case "Title_desc":
                    books = books.OrderByDescending(x => x.Title);
                    break;

                case "Title":
                    books = books.OrderBy(x => x.Title);
                    break;

                case "Author":
                    books = books.OrderBy(x=>x.Author);
                    break;
                case "Author_desc":
                    books = books.OrderByDescending(x => x.Author);
                    break;

            }

            return View(books);

        }
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db1));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                    return (true);
                else
                    return (false);
            }
            return (false);
         }
        
        public ActionResult Index()
        {
            //var num = "1";
            //var users = db1.Roles.Where(x=>x.Id == num);
            ViewBag.Auth = isAdminUser(); 
            return View(db.Mybooks.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Mybooks.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,SerialNumber,Author,Publisher,qty,IsAvailable")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Mybooks.Add(books);
                db.SaveChanges();
                return RedirectToAction("Index2");
            }

            return View(books);
        }
        //[Authorize(Roles = "Administrator")]
        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Mybooks.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Title,SerialNumber,Author,Publisher,qty,IsAvailable")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Mybooks.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Books books = db.Mybooks.Find(id);
            db.Mybooks.Remove(books);
            db.SaveChanges();
            return RedirectToAction("Index2");
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
