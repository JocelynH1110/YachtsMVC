using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Yachts.Helpers
{
    public static class StringExtensions
    {
        public static string Truncate(this string value,int length)
        {
            if(string.IsNullOrEmpty(value)) return value;

            // 去除 HTML tag
            var plain = Regex.Replace(value, "<.*?>", "");
            return plain.Length<=length ? plain : plain.Substring(0,length)+"...";
        }
    }
}