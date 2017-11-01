using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;
using System.IO;

namespace BlankSpider.Spider.Utility
{
    public class ImageUtility
    {
        public static bool IsUrlImage(string urlImage)
        {
            string[] ExtArray = { ".gif", ".jpg", ".png", ".jpeg", ".bmp" };
            bool found = false;
            foreach (string ext in ExtArray)
                if (urlImage.ToLower().Trim().EndsWith(ext))
                {
                    found = true;
                    break;
                }

            return found;
        }

        public static string GetUrlImage(string urlImage)
        {
            if (!urlImage.StartsWith("http://"))
                urlImage = "http://" + urlImage;
            if (urlImage.Contains("fptmobile") && !urlImage.Contains("www."))
                urlImage = urlImage.Replace("http://", "http://www.");

            return urlImage;
        }

        
    }
}
