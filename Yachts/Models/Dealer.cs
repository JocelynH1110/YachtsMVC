using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yachts.Models
{
    public class Dealer
    {
        public int DealerId { get; set; }

        [Required(ErrorMessage = "請輸入公司名稱")]
        [StringLength(200,ErrorMessage = "名稱不可超過200個字")]
        [Display(Name = "公司名稱")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "請輸入聯絡人姓名")]
        [StringLength(100)]
        [Display(Name = "聯絡人")]
        public string Contact { get; set; }
        [Display(Name = "公司地址")]
        public string Address { get; set; }

        [Required(ErrorMessage = "請輸入電話號碼")]
        [StringLength(30)]
        [Display(Name = "電話")]
        public string Tel { get; set; }

        [StringLength(30)]
        [Display(Name = "傳真")]
        public string Fax { get; set; }

        [StringLength(200)]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [StringLength(200)]
        [Url]
        [Display(Name = "公司網站")]
        public string Website { get; set; }
        [Display(Name = "建立時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [Display(Name = "更新時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        [Display(Name = "排列順序")]
        public int SortOrder { get; set; } = 0;

        [Required]
        [StringLength(2, ErrorMessage = "國家代碼必須是2個字母")]
        [Display(Name = "國家代碼")]
        public string CountryCode { get; set; }
    }
}