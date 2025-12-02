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

        public int ProductId { get; set; }

        public string DimensionValue { get; set; }

        public virtual Product Product { get; set; }
    }
}