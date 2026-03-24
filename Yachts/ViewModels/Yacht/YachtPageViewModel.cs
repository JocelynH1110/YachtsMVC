using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yachts.ViewModels.Yacht
{
    // 遊艇主頁
    public class YachtPageViewModel
    {
        public List<YachtListViewModel> Yachts {  get; set; }
        public YachtDetailViewModel SelectedYacht {  get; set; }
    }
}