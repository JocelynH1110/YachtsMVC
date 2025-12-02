using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yachts.Models
{
    public class ProductSize
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "尺寸名稱")]
        [StringLength(30)]
        public string DimensionName { get; set; }

        [Display(Name = "尺寸大小")]
        [StringLength(30)]
        public string DimensionValue { get; set; }
        
        // FK
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}