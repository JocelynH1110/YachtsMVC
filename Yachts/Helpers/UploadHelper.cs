using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Yachts.Helpers
{
    public static class UploadHelper
    {
        public static List<string> SaveFiles(IEnumerable<HttpPostedFileBase > files, string folder)
        {
            var results=new List<string>();

            if (files == null)
                return results;

            string uploadPath = HttpContext.Current.Server.MapPath(folder);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // 檢查傳進來的檔案們
            foreach (var file in files)
            {
                if (file == null || file.ContentLength == 0)
                    continue;

                // 檢查副檔名只接受 PDF（可依需求擴充）
                var ext = Path.GetExtension(file.FileName).ToLower();
                //if (ext != ".pdf")
                //    continue;

                // 使用 GUID 防止檔名重複
                string newFileName = $"{Guid.NewGuid()}{ext}";
                string fullPath = Path.Combine(uploadPath, newFileName);

                file.SaveAs(fullPath);

                // 回傳儲存後的路徑
                results.Add($"{folder}/{newFileName}");
            }

            return results;
        }
    }
}