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
    public class DealersController : Controller
    {
        private DBModelContext db = new DBModelContext();
      
        // GET: Admin/Dealers
        public ActionResult Index(int? page,int? pageSize,string searchByCompany)
        {
            // 一頁幾筆
            if (!pageSize.HasValue)
            {
                pageSize = 20;
            }
            ViewBag.PageSize=pageSize;

            // 目前頁數
            if (!page.HasValue) 
            { 
                page = 1;
            }

            var dealers = db.Dealers.AsQueryable();
          
            if (!string.IsNullOrEmpty(searchByCompany))
            {
                dealers=dealers.Where(d=>d.CompanyName.Contains(searchByCompany));
                ViewBag.SearchByCompany=searchByCompany;
            }
            var result = dealers.OrderByDescending(d => d.CreatedAt).ToPagedList(page.Value - 1, pageSize.Value);

            return View(result);
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
            return View();
        }

        // POST: Admin/Dealers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyName,Contact,Address,Tel,Fax,Email,Website,SortOrder,CountryCode,PhotoFile,Region")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                dealer.CreatedAt = DateTime.Now;
                dealer.UpdatedAt = null;

                // 如果有上傳檔案
                if (dealer.PhotoFile != null && dealer.PhotoFile.ContentLength > 0)
                {
                    // 取得檔名（避免重名）
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(dealer.PhotoFile.FileName);
                    string uploadDir = Server.MapPath("~/Uploads/Dealers");

                    // 如果資料夾不存在就建立
                    if (!System.IO.Directory.Exists(uploadDir))
                        System.IO.Directory.CreateDirectory(uploadDir);

                    string filePath = System.IO.Path.Combine(uploadDir, fileName);
                    dealer.PhotoFile.SaveAs(filePath);

                    // 存相對路徑到 DB
                    dealer.PhotoPath = "/Uploads/Dealers/" + fileName;
                }

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
        public ActionResult Edit([Bind(Include = "DealerId,CompanyName,Contact,Address,Tel,Fax,Email,Website,SortOrder,CountryCode,PhotoFile,Region")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                // dealer.UpdatedAt = DateTime.Now;
                // db.Entry(dealer).State = EntityState.Modified;
                var dbDealer = db.Dealers.Find(dealer.DealerId);
                if (dbDealer == null)
                    return HttpNotFound();

                dbDealer.CompanyName = dealer.CompanyName;
                dbDealer.Contact = dealer.Contact;
                dbDealer.Address = dealer.Address;
                dbDealer.Tel = dealer.Tel;
                dbDealer.Email = dealer.Email;
                dbDealer.Website = dealer.Website;
                dbDealer.Fax = dealer.Fax;
                dbDealer.UpdatedAt = DateTime.Now;
                dbDealer.SortOrder = dealer.SortOrder;
                dbDealer.CountryCode = dealer.CountryCode;

                // 如果有重新上傳新照片
                if (dealer.PhotoFile != null && dealer.PhotoFile.ContentLength > 0)
                {
                    try { 
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(dealer.PhotoFile.FileName);
                    string uploadDir = Server.MapPath("~/Uploads/Dealers");

                    if (!System.IO.Directory.Exists(uploadDir))
                        System.IO.Directory.CreateDirectory(uploadDir);

                    string filePath = System.IO.Path.Combine(uploadDir, fileName);
                    dealer.PhotoFile.SaveAs(filePath);

                    dbDealer.PhotoPath = "/Uploads/Dealers/" + fileName;
                    }
                    catch(Exception ex) 
                    {

                        ModelState.AddModelError("", "圖片上傳失敗：" + ex.Message);
                    }
                }
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
    }
}
