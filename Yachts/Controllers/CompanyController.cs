using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yachts.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Certificate()
        {
            return View();
        }
    }
}