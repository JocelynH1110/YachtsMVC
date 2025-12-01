using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yachts.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "新型")]
        public bool IsLatest { get; set; } = false;

        [Display(Name ="遊艇名稱")]
        [Required(ErrorMessage ="請輸入遊艇名稱")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "遊艇尺寸")]
        [StringLength(65535)]
        public string Dimensions { get; set; }

        [Display(Name = "結構圖")]
        [StringLength(65535)]
        public string Structual { get; set; }

        [Display(Name = "規格書")]
        [StringLength(65535)]
        public string Specification { get; set; }

        [Display(Name = "建立時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "更新時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}