using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yachts.Models;
using ISO3166;

namespace Yachts.Areas.Admin.Controllers
{
    public class DealersController : Controller
    {
        private DBModelContext db = new DBModelContext();

        // GET: Admin/Dealers
        public ActionResult Index()
        {
            return View(db.Dealers.ToList());
        }

        // GET: Admin/Dealers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealer dealer = db.Dealers.Find(id);
            if (dealer == null)
            {
                return HttpNotFound();
            }
            return View(dealer);
        }

        // GET: Admin/Dealers/Create
        public ActionResult Create()
        {
            ViewBag.CountryCode = GetCountryList();
            return View();
        }

        // POST: Admin/Dealers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DealerId,CompanyName,Contact,Address,Tel,Fax,Email,Website,CreatedAt,UpdatedAt,SortOrder,CountryCode")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                dealer.CreatedAt = DateTime.Now;
                dealer.UpdatedAt = null;
                db.Dealers.Add(dealer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dealer);
        }

        // GET: Admin/Dealers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealer dealer = db.Dealers.Find(id);
            if (dealer == null)
            {
                return HttpNotFound();
            }
            return View(dealer);
        }

        // POST: Admin/Dealers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DealerId,CompanyName,Contact,Address,Tel,Fax,Email,Website,CreatedAt,UpdatedAt,SortOrder,CountryCode")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                dealer.UpdatedAt = DateTime.Now;
                db.Entry(dealer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dealer);
        }

        // GET: Admin/Dealers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealer dealer = db.Dealers.Find(id);
            if (dealer == null)
            {
                return HttpNotFound();
            }
            return View(dealer);
        }

        // POST: Admin/Dealers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dealer dealer = db.Dealers.Find(id);
            db.Dealers.Remove(dealer);
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
        private SelectList GetCountryList()
        {
            var countries = Country.List
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.TwoLetterCode,  // 兩字母代碼: TW
                    Text = c.Name          // 國家名稱: Taiwan
                })
                .ToList();

            countries.Insert(0, new SelectListItem { Value = "", Text = "請選擇國家" });

            return new SelectList(countries, "Value", "Text");
        }

    }
}
