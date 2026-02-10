using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yachts.Models;

namespace Yachts.Repositories
{
    public class YachtRepository
    {
        private readonly DBModelContext _db;

        public YachtRepository(DBModelContext dBModelContext)
        {
            _db = dBModelContext;
        }

        // 取得全部
        public IQueryable<Product> GetProducts(string productName = null)
        {
            var query = _db.Products.AsQueryable();

            // productName 有值就做篩選;沒值 = null 就傳回全部資料
            if (!string.IsNullOrEmpty(productName))
            {
                query=query.Where(p=>p.Name == productName);
            }
            return query.OrderBy(p=>p.Name);
        }
        public IEnumerable<string> ListYachts()
        {
            return _db.Products.Where(p=>!string.IsNullOrEmpty(p.Name)).Select(p=>p.Name).Distinct().ToList();
        }
    }
}