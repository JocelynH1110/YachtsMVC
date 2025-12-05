using MvcPaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Yachts.Models;

namespace Yachts.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private DBModelContext db = new DBModelContext();

        // GET: Admin/Products
        public ActionResult Index(int? page, int? pageSize, string searchByName)
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

            var products = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchByName))
            {
                products = products.Where(n => n.Name.Contains(searchByName));
                ViewBag.SearchByName = searchByName;
            }

            var result = products.OrderByDescending(n => n.CreatedAt).ThenByDescending(n => n.UpdatedAt).ToPagedList(page.Value - 1, pageSize.Value);

            return View(result);
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsLatest,Name,Sizes,Structual,Specification,CreatedAt,UpdatedAt")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products
                    .Include("Sizes")
                    .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            // 若第一次進編輯頁面，但 Sizes = null，就初始化避免 null 錯誤
            if (product.Sizes == null)
                product.Sizes = new List<ProductSize>();

            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Sizes == null)
                    product.Sizes = new List<ProductSize>();  // 防 Null 因為我的尺寸可以為空

                // ========== 第一步：先過濾掉空的 Sizes ==========
                if (product.Sizes != null)
                {
                    product.Sizes = product.Sizes
                        .Where(s => !string.IsNullOrWhiteSpace(s.DimensionName)
                                 && !string.IsNullOrWhiteSpace(s.DimensionValue))
                        .ToList();
                }

                var existing = db.Products.Include(p => p.Sizes).FirstOrDefault(p => p.Id == product.Id);

                if (existing == null)
                    return HttpNotFound();

                // 更新產品資料
                existing.Name = product.Name;
                existing.IsLatest = product.IsLatest;
                existing.Structual = product.Structual;
                existing.Specification = product.Specification;
                existing.UpdatedAt = DateTime.Now;

                // ========== 第二步：處理尺寸變更 ==========
                // 刪除被使用者移除的尺寸（包括空欄位）
                var postedIds = product.Sizes.Select(s => s.Id).ToList();
                var toDelete = existing.Sizes
                    .Where(s => !postedIds.Contains(s.Id))
                    .ToList();

                foreach (var del in toDelete)
                    db.ProductSizes.Remove(del);

                // 更新 + 新增尺寸
                foreach (var size in product.Sizes)
                {
                    if (size.Id > 0)
                    {
                        // 更新舊尺寸
                        var oldSize = existing.Sizes.First(s => s.Id == size.Id);
                        oldSize.DimensionName = size.DimensionName;
                        oldSize.DimensionValue = size.DimensionValue;
                    }
                    else
                    {
                        // 新增尺寸
                        size.ProductId = existing.Id;
                        db.ProductSizes.Add(size);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // 把附表的資料載入
            Product product = db.Products.Include(s=>s.Sizes).FirstOrDefault(s=>s.Id==id);

            db.Products.Remove(product);
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
