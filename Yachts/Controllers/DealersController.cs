using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Yachts.Models;
using ISO3166;

namespace Yachts.Controllers
{
    public class DealersController : Controller
    {
        private DBModelContext db
            = new DBModelContext();
        // GET: Dealers
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
            var dealers=db.Dealers.AsQueryable();
            var result=dealers.OrderBy(d=>d.CountryCode).ToPagedList(page.Value, pageSize.Value);
            return View(result);
        }
    }
}