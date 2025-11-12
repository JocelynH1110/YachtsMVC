using System.Linq;
using System.Web.Mvc;
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

            var result = _db.Dealers.OrderBy(d => d.CountryCode).ToPagedList(page.Value - 1, pageSize.Value);

            ViewBag.Regions = _repo.ListRegions();
            return View(result);
        }
    }
}