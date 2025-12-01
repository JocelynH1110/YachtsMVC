using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yachts.Models
{
    public class NewsAttachment
    {
        [Key]
        public int AttachmentId { get; set; }

        [Display(Name = "檔案名稱")]
        [StringLength(255)]
        public string FileName { get; set; }

        [Display(Name = "檔案路徑")]
        [StringLength(500)]
        public string FilePath { get; set; }

        [Display(Name = "上傳時間")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public int NewsId { get; set; }

        // 導覽屬性(可以直接叫出新聞 model 裡的欄位)
        public virtual NewsItem NewsItem { get; set; }

        // 不進 DB，用來接收檔案
        [NotMapped]
        public HttpPostedFileBase File { get; set; }

    }
}