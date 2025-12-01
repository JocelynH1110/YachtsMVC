using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Yachts.Models;

namespace Yachts.Controllers
{
    public class ContactController : Controller
    {
        private static readonly string SmtpUsername = Environment.GetEnvironmentVariable("SMTP_USER") ??
                                                      throw new InvalidOperationException(
                                                          "Error: Environment variable SMTP_USER is not set!");

        private static readonly string SmtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ??
                                                      throw new InvalidOperationException(
                                                          "Error: Environment variable SMTP_PASSWORD is not set!");

        private readonly DBModelContext _db = new DBModelContext();

        public ActionResult Contact()
        {
            //SendMailToAdmin(form);
            ViewBag.CountryList = GetCountryList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitContact(ContactForm form)
        {
            ViewBag.CountryList = GetCountryList();

            var ip = Request.UserHostAddress;

            if (IsFrequentlySubmit(ip))
            {
                return Content("你送出太頻繁了，請稍候再試");
            }

            if (ModelState.IsValid)
            {
                _db.ContactForms.Add(form);
                _db.SaveChanges();

                SendMailToAdmin(form);
                TempData["Message"] = "Successfully sent";    // 這樣才可跨越一次可以跨一次 Redirec至Contact()
                TempData["Status"] = "success";
                return RedirectToAction("Contact");
            }
            TempData["Message"] = "Sending failed";
            TempData["Status"] = "error";

            return View("Contact", form);   // 送出表單失敗會保留當下填寫的資料
        }

        // 送出次數限制（Rate Limiting）
        public static Dictionary<string, DateTime> submitRecord = new Dictionary<string, DateTime>();
        private bool IsFrequentlySubmit(string ip)
        {
            if (!submitRecord.ContainsKey(ip)) return false;
            return (DateTime.Now - submitRecord[ip]).TotalSeconds < 30;
        }

        // 管理者收到表單
        public void SendMailToAdmin(ContactForm form)
        {
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(SmtpUsername, SmtpPassword)
            };

            var mail = new MailMessage();
            mail.From = new MailAddress(SmtpUsername);
            mail.To.Add("jocelyn.huang1110@gmail.com");
            mail.Subject = "遊艇諮詢表單";
            mail.IsBodyHtml = true;
            mail.Body = $@"
                詢問者填寫資料如下:<br>
                諮詢者姓名:{form.Name}<br>
                國家:{form.Country}<br>
                信箱:{form.Email}<br>
                詢問船型:{form.Yachts}<br>
                訊息:{form.comments}<br>
            ";
            smtp.Send(mail);
        }

        private List<SelectListItem> GetCountryList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Taiwan", Text = "台灣" },
                new SelectListItem { Value = "Japan", Text = "日本" },
                new SelectListItem { Value = "USA", Text = "美國" },
                new SelectListItem { Value = "UK", Text = "英國" },
                new SelectListItem { Value = "Germany", Text = "德國" }
            };
        }
    }
}