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

        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100)]
        public string Contact { get; set; }

        public string Address { get; set; }

        [Required]
        [StringLength(30)]
        public string Tel { get; set; }

        [StringLength(30)]
        public string Fax { get; set; }

        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(200)]
        [Url]
        public string Website { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int SortOrder { get; set; } = 0;

        [Required]
        [StringLength(2)]
        public string CountryCode { get; set; }
    }
}