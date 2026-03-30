using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yachts.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        [Index("IX_Product_SortOrder", 0, IsUnique = true)] // 複合索引
        public int ProductId {  get; set; } // FK

        [Required]
        public string ImageUrl { get; set; }
        public bool IsCover {  get; set; }=false;

        [Index("IX_Product_SortOrder", 1, IsUnique =true)]   // 複合索引
        public int SortOrder { get; set; } = 0; // todo 射程唯一
        public Product Product { get; set; }
    }
}