using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcPaging;
using Yachts.Models;
using Yachts.Repositories;

namespace Yachts.Controllers
{
    public class DealersController : Controller
    {
        private readonly DBModelContext _db;
        private readonly DealerRepository _repo;

        public DealersController()
        {
            _db = new DBModelContext();
            _repo = new DealerRepository(_db);
        }

        // GET: Dealers
        public ActionResult Index(int? page, int? pageSize, string region)
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
            var dealers = _db.Dealers.AsQueryable();

            if (!string.IsNullOrEmpty(region))
            {
                dealers = dealers.Where(x => x.Region == region);
                ViewBag.Region = region;
                ViewBag.Count= dealers.Count();
            }

            var result = dealers.OrderBy(d => d.CountryCode).ToPagedList(page.Value - 1, pageSize.Value);
        
            ViewBag.CurrentPage = result.PageNumber;
            ViewBag.LastPage = result.PageCount;
            ViewBag.Regions = _repo.ListRegions();

            return View(result);
        }
    }
}