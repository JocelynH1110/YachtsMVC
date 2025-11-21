using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace Yachts.Models
{
    public class NewsItem
    {
        [Key] public int NewsId { get; set; }

        [Display(Name = "標題")]
        [Required(ErrorMessage = "請輸入標題")]
        [StringLength(255)]
        public string Title { get; set; }

        [AllowHtml]
        [Display(Name = "內容")]
        [Required(ErrorMessage = "請輸入內容")]
        [StringLength(65535)]
        public string Content { get; set; }

        [Display(Name = "置頂")] public bool Pinned { get; set; }

        // 儲存在資料庫的圖片檔路徑
        [Display(Name = "封面照片")]
        [StringLength(255)]
        public string CoverPhotoPath { get; set; }

        // 不存進資料庫，用來接收使用者上傳檔案
        [NotMapped] public HttpPostedFileBase CoverPhotoFile { get; set; }

        [Display(Name = "建立時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "更新時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // 一筆新聞可以有多個附件
        [Display(Name = "附件")]
        public virtual ICollection<NewsAttachment> Attachments { get; set; }
    }
}