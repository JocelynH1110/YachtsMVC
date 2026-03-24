using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yachts.ViewModels.Yacht
{    
    // 遊艇詳細頁(遊艇頁右方)
    public class YachtDetailViewModel
    {
        public string Name { get; set; }
        public string Structual { get; set; }
        public string Specification { get; set; }

        public List<YachtSizeViewModel> Sizes { get; set; }
    }
}