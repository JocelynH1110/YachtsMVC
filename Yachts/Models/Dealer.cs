using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yachts.Models
{
    public class Dealer
    {
        [Key]
        public int DealerId { get; set; }

        [Display(Name = "公司名稱")]
        [Required(ErrorMessage = "請輸入公司名稱")]
        [StringLength(200,ErrorMessage = "名稱不可超過200個字")]
        public string CompanyName { get; set; }

        [Display(Name = "聯絡人")]
        [Required(ErrorMessage = "請輸入聯絡人姓名")]
        [StringLength(100)]  
        public string Contact { get; set; }

        [Display(Name = "公司地址")]
        public string Address { get; set; }

        [Display(Name = "電話")]
        [Required(ErrorMessage = "請輸入電話號碼")]
        [StringLength(30)]
        public string Tel { get; set; }

        [Display(Name = "傳真")]
        [StringLength(30)]
        public string Fax { get; set; }

        [Display(Name = "電子郵件")]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "公司網站")]
        [StringLength(200)]
        [Url]
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
        [Display(Name = "地區")]
        public string Region { get; set; }

        [Required]
        [Display(Name = "國家")]
        public string CountryCode { get; set; }

        // 儲存在資料庫的圖片檔路徑
        [Display(Name = "照片")]
        [StringLength(255)]
        public string PhotoPath { get; set; }

        // 不存進資料庫，用來接收使用者上傳檔案
        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }
    }
}