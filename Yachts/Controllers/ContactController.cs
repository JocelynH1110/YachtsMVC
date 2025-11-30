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
            Debug.WriteLine("=== FORM HAS ARRIVED ===");
            if (ModelState.IsValid)
            {
                _db.ContactForms.Add(form);
                _db.SaveChanges();

                SendMailToAdmin(form);

                return Content("YES I AM HERE！");
            }

            return View("Contact", form);
        }


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