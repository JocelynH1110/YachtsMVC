using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "建立時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "更新時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // 一筆新聞可以有多個附件
        public virtual ICollection<NewsAttachment> Attachments { get; set; }
    }
}