using Ganss.Xss;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Yachts.Helpers;
using Yachts.Models;

namespace Yachts.Areas.Admin.Controllers
{
    public class NewsItemsController : Controller
    {
        private DBModelContext db = new DBModelContext();

        // GET: Admin/NewsItems
        public ActionResult Index(int? page, int? pageSize, string searchByTitle)
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

            if (!string.IsNullOrEmpty(searchByTitle))
            {
                newsItems = newsItems.Where(n => n.Title.Contains(searchByTitle));
                ViewBag.SearchByTitle = searchByTitle;
            }

            var result = newsItems.OrderByDescending(n => n.CreatedAt).ThenByDescending(n => n.UpdatedAt).ToPagedList(page.Value - 1, pageSize.Value);

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
        [ValidateInput(false)]    // 允許 HTML（但請在儲存前 sanitize）
        public ActionResult Create([Bind(Include = "NewsId,Title,Content,Pinned,CoverPhotoFile,CoverPhotoPath")] NewsItem newsItem, IEnumerable<HttpPostedFileBase> Files)
        {
            var f1 = Request.Files;
            var f2 = Request.Files["CoverPhotoFile"];
            var f3 = newsItem.CoverPhotoFile;

            if (ModelState.IsValid)
            {
                newsItem.CreatedAt = DateTime.Now;
                newsItem.UpdatedAt = null;

                //上傳檔案
                // 建立附件集合，不然會報 NullReferenceException
                newsItem.Attachments = new List<NewsAttachment>();

                // 1.呼叫 Helper 儲存檔案
                var savedPaths = UploadHelper.SaveFiles(Files, "~/Uploads/News/Attachments/");

                // 2. 每個檔案建立一個 NewsAttachment
                foreach (var path in savedPaths)
                {
                    newsItem.Attachments.Add(new NewsAttachment
                    {
                        FileName = Path.GetFileName(path),
                        FilePath = path
                    });
                }

                var sanitizer = new HtmlSanitizer();

                // CKEditor 內容進行過濾
                newsItem.Content = sanitizer.Sanitize(newsItem.Content);

                // 上傳封面照片
                if (newsItem.CoverPhotoFile != null && newsItem.CoverPhotoFile.ContentLength > 0)
                {
                    // 取得檔名（避免重名）
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(newsItem.CoverPhotoFile.FileName);
                    string uploadDir = Server.MapPath("~/Uploads/News/CoverPhotos");

                    // 如果資料夾不存在就建立
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string filePath = Path.Combine(uploadDir, fileName);
                    newsItem.CoverPhotoFile.SaveAs(filePath);

                    // 存相對路徑到 DB
                    newsItem.CoverPhotoPath = "/Uploads/News/CoverPhotos/" + fileName;
                }

                db.NewsItems.Add(newsItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsItem);
        }

        // 圖片上傳 API
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase uplaod,string CKEditorFuncNum,string CKEditor, string langCode)
        {
            if ((uplaod != null) && uplaod.ContentLength > 0)
            {
                string fileName = Path.GetFileName(uplaod.FileName);
                string path = Path.Combine(Server.MapPath("~/Uploads/News/"), fileName);
                uplaod.SaveAs(path);

                var url=Url.Content(path);
                var msg = "上傳成功";

                return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + url + "', '" + msg + "');</script>");
                //// 回傳給 CKEditor 的格式固定如下：
                //return Json(new
                //{
                //    uploaded = 1,
                //    fileName = fileName,
                //    url = Url.Content("~/Uploads/News/" + fileName)
                //});
            }
            return HttpNotFound();
            // 上傳失敗時
            //return Json(new { uploaded = 0, error = new { message = "上傳失敗" } });
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
        public ActionResult Edit([Bind(Include = "NewsId,Title,Content,Pinned,CoverPhotoFile")] NewsItem newsItem,IEnumerable<HttpPostedFileBase> Files, int[] DeleteAttachmentIds)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(newsItem).State = EntityState.Modified;

                var dbnewsItem = db.NewsItems.Find(newsItem.NewsId);
                if (dbnewsItem == null)
                    return HttpNotFound();

                dbnewsItem.Title = newsItem.Title;
                dbnewsItem.Content = newsItem.Content;
                dbnewsItem.Pinned = newsItem.Pinned;
                dbnewsItem.UpdatedAt = DateTime.Now;         

                // 如果有重新上傳新照片
                if (newsItem.CoverPhotoFile != null && newsItem.CoverPhotoFile.ContentLength > 0)
                {
                    try
                    {
                        string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(newsItem.CoverPhotoFile.FileName);
                        string uploadDir = Server.MapPath("~/Uploads/News/CoverPhotos");

                        if (!System.IO.Directory.Exists(uploadDir))
                            System.IO.Directory.CreateDirectory(uploadDir);

                        string filePath = System.IO.Path.Combine(uploadDir, fileName);
                        newsItem.CoverPhotoFile.SaveAs(filePath);

                        dbnewsItem.CoverPhotoPath = "/Uploads/News/CoverPhotos/" + fileName;
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError("", "圖片上傳失敗：" + ex.Message);
                    }
                }

                /* 1. 刪除附件 */
                if (DeleteAttachmentIds != null)
                {
                    foreach (var id in DeleteAttachmentIds)
                    {
                        var att = dbnewsItem.Attachments.FirstOrDefault(a => a.AttachmentId == id);
                        if (att != null)
                        {
                            // 刪除實體檔案
                            var fullPath = Server.MapPath(att.FilePath);
                            if (System.IO.File.Exists(fullPath))
                                System.IO.File.Delete(fullPath);

                            // 刪除資料
                            db.Entry(att).State=EntityState.Deleted;
                        }
                    }
                }
                /* 2. 新增新上傳附件 */
                if (Files != null)
                {
                    string uploadPath = Server.MapPath("~/Uploads/News/Attachments/");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    foreach (var file in Files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                            string fullPath = Path.Combine(uploadPath, fileName);

                            file.SaveAs(fullPath);

                            dbnewsItem.Attachments.Add(new NewsAttachment
                            {
                                FileName = file.FileName,
                                FilePath = "/Uploads/News/Attachments/" + fileName
                            });
                        }
                    }
                }

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
