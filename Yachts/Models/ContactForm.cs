using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Yachts.Models
{
    public class ContactForm
    {
        [Key]
        public int InquirerId { get; set; }

        [Required]
        [Display(Name = "諮詢者名字")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "信箱")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "電話")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name="國家")]
        public string Country {  get; set; }

        [Required]
        [Display(Name = "遊艇型號")]
        public string Yachts { get; set; }

        [Display(Name = "留言")]
        public string comments { get; set; }

        [Display(Name="建立時間")]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd HH:mm}",ApplyFormatInEditMode =false)]
        public DateTime CreatedAt {  get; set; }= DateTime.Now;
    }
}