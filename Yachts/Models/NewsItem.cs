using System;
using System.ComponentModel.DataAnnotations;

namespace Yachts.Models
{
    public class NewsItem
    {
        [Key] public int NewsId { get; set; }

        [Display(Name = "標題")]
        [Required(ErrorMessage = "請輸入標題")]
        [StringLength(255)]
        public string Title { get; set; }

        [Display(Name = "內容")]
        [Required(ErrorMessage = "請輸入內容")]
        [StringLength(65535)]
        public string Content { get; set; }

        [Display(Name = "置頂")] public bool Pinned { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}