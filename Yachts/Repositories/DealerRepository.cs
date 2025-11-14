using System.Collections.Generic;
using System.Linq;
using Yachts.Models;

namespace Yachts.Repositories
{
    public class DealerRepository
    {
        private readonly DBModelContext _db;

        public DealerRepository(DBModelContext dbModelContext)
        {
            _db = dbModelContext;
        }

        public IEnumerable<string> ListRegions()
        {
            return _db.Dealers.Where(d => !string.IsNullOrEmpty(d.Region)).Select(d => d.Region).Distinct()
                .ToList();
        }
    }
}