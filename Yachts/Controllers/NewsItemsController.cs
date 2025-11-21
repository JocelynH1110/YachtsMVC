using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yachts.Models;

namespace Yachts.Controllers
{
    public class NewsItemsController : Controller
    {
        private readonly DBModelContext _db=new DBModelContext();
 
        // GET: NewsItems
        public ActionResult Index(int? page, int? pageSize)
        {
            // 一頁幾筆
            if (!pageSize.HasValue)
            {
                pageSize = 4;
            }

            // 目前頁數
            if (!page.HasValue)
            {
                page = 1;
            }
            var newsItems = _db.NewsItems.AsQueryable();

            var result = newsItems.OrderByDescending(d => d.CreatedAt).ToPagedList(page.Value - 1, pageSize.Value);
           
            ViewBag.Count = _db.NewsItems.Count();
            ViewBag.CurrentPage = result.PageNumber;
            ViewBag.LastPage = result.PageCount;
            return View(result);
        }

        public ActionResult Content(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsItem newsItem = _db.NewsItems.Find(id);
            if (newsItem == null)
            {
                return HttpNotFound();
            }
            return View(newsItem);
        }
    }
}