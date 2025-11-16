using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yachts.Models;
using MvcPaging;

namespace Yachts.Areas.Admin.Controllers
{
    public class NewsItemsController : Controller
    {
        private DBModelContext db = new DBModelContext();

        // GET: Admin/NewsItems
        public ActionResult Index(int? page,int?pageSize,string searchByTitle)
        {
            // 目前頁數
            if (!page.HasValue)
            {
                page = 1;
            }

            // 一頁幾筆
            if (!pageSize.HasValue)
            {
                pageSize = 10;
            }
            ViewBag.PageSize = pageSize.Value;

            var newsItems = db.NewsItems.AsQueryable();

            if(!string.IsNullOrEmpty(searchByTitle))
            {
                newsItems=newsItems.Where(n=>n.Title.Contains(searchByTitle));
                ViewBag.SearchByTitle = searchByTitle;
            }

            var result=newsItems.OrderByDescending(n=>n.CreatedAt).ThenByDescending(n=>n.UpdatedAt).ToPagedList(page.Value-1,pageSize.Value);

            return View(result);
        }

        // GET: Admin/NewsItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsItem newsItem = db.NewsItems.Find(id);
            if (newsItem == null)
            {
                return HttpNotFound();
            }
            return View(newsItem);
        }

        // GET: Admin/NewsItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewsItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,Title,Content,Pinned")] NewsItem newsItem)
        {
            newsItem.CreatedAt=DateTime.Now;
            newsItem.UpdatedAt=null;

            if (ModelState.IsValid)
            {
                db.NewsItems.Add(newsItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsItem);
        }

        // GET: Admin/NewsItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsItem newsItem = db.NewsItems.Find(id);
            if (newsItem == null)
            {
                return HttpNotFound();
            }
            return View(newsItem);
        }

        // POST: Admin/NewsItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,Title,Content,Pinned,CreatedAt,UpdatedAt")] NewsItem newsItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsItem);
        }

        // GET: Admin/NewsItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsItem newsItem = db.NewsItems.Find(id);
            if (newsItem == null)
            {
                return HttpNotFound();
            }
            return View(newsItem);
        }

        // POST: Admin/NewsItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsItem newsItem = db.NewsItems.Find(id);
            db.NewsItems.Remove(newsItem);
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
