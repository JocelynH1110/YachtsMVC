using Nager.Country;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yachts.Services
{
    public class CountryService
    {
        private readonly CountryProvider _provider = new CountryProvider();

        // 取得國家清單
        public List<CountryItem> GetCountries()
        {
            var countries = _provider.GetCountries();
            return countries
                .Select(c => new CountryItem
                {
                    Code = c.Alpha2Code.ToString(),
                    Name = c.CommonName
                })
                .OrderBy(c => c.Name)
                .ToList();
        }

        // 取得所有地區清單
        public List<string> GetRegions()
        {
            var countries = _provider.GetCountries();
            return countries
                .Select(c => c.Region.ToString())
                .Distinct()
                .OrderBy(r => r)
                .ToList();

        
        }

        public IEnumerable<object> GetCountriesByRegion(string region)
        {        
            if (!Enum.TryParse(region, true, out Region regionEnum))
            {
                return new List<object>();
            }

            return _provider.GetCountries()
                .Where(c => c.Region == regionEnum)
                .Select(c => new
                {
                    Code = c.Alpha2Code,
                    Name = c.CommonName
                })
                .OrderBy(c => c.Name)
                .ToList();
        }

    }

    // 建立 SelectList 的模型
    public class CountryItem
    {
        public string Name { get; set; }
        public string Code
        {
            get; set;
        }
    }
}