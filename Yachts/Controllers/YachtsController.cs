using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachts.Models;
using Yachts.Repositories;

namespace Yachts.Controllers
{
    public class YachtsController : Controller
    {
        private readonly YachtRepository _repo;

        public YachtsController()
        {
            _repo=new YachtRepository(new DBModelContext());
        }

        // GET: Yachts
        public ActionResult Index(string product)
        {
            var result = _repo.GetProducts(product);

            ViewBag.Product = product;
            ViewBag.Products = _repo.ListYachts();
           
            return View(result);
        }
    }
}