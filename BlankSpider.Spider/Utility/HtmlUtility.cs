using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BlankSpider.Spider.Utility
{
    public class HtmlUtility
    {
        public static List<string> GetListTag(string str)
        {
            try
            {
                if (str == null || str.Trim() == "")
                    return null;

                Match mt = (new Regex("'<.*?>'")).Match(str);
                while (mt.Success)
                {
                    str = str.Replace(mt.Value, "");
                    mt = mt.NextMatch();
                }

                mt = (new Regex("<(?<tag>.*?)>")).Match(str);
                List<string> listTag = new List<string>();
                while (mt.Success)
                {
                    listTag.Add(mt.Value);
                    mt = mt.NextMatch();
                }
                if (listTag.Count > 0)
                    return listTag;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RemoveTagComment(ref string html)
        {
            try
            {
                if (html == null || html.Trim() == "")
                    return string.Empty;

                return Regex.Replace(html, "<!--(.|\n)*?-->", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RemoveJavaScipt(ref string html)
        {
            try
            {
                if (html == null || html.Trim() == "")
                    return string.Empty;

                return Regex.Replace(html, "<[^>/]*([jJ]ava)?[sS]cript[^>]*>(?<text>(.|\n)*?)<[^>]*([jJ]ava)?[sS]cript[^>]*>", "", RegexOptions.Singleline);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RemoveAllTag(string html)
        {
            try
            {
                if (html == null || html.Trim() == "")
                    return string.Empty;

                List<string> listTag = GetListTag(html);
                if (listTag != null && listTag.Count > 0)
                    foreach (string item in listTag)
                    {
                        if (item.ToLower().Contains("<br"))
                            html = html.Replace(item, "\r\n");
                        else if (item.ToLower().Contains("</p"))
                            html = html.Replace(item, "\r\n");
                        else if (item.ToLower().Contains("</div"))
                            html = html.Replace(item, "\r\n");
                        else if (item.ToLower().Contains("</tr"))
                            html = html.Replace(item, "\r\n");
                        else if (item.ToLower().Contains("</td"))
                            html = html.Replace(item, " ");
                        else if (html.Contains(item))
                            html = html.Replace(item, "");
                    }
                html = html.Replace("\r\n\r\n\r\n", "\r\n\r\n");
                html = html.Replace("\n\n\n", "\n\n");
                return html.Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RemoveAllTagParagraph(ref string html)
        {
            try
            {
                if (html == null || html.Trim() == "")
                    return string.Empty;

                html = Regex.Replace(html, "<!--.*?-->", "");
                List<string> listTag = GetListTag(html);
                if (listTag != null && listTag.Count > 0)
                    foreach (string item in listTag)
                    {
                        if (item.ToLower().StartsWith("<p"))
                            continue;
                        if (item.ToLower().StartsWith("<br"))
                            continue;
                        else if (html.Contains(item))
                            html = html.Replace(item, "");
                    }
                return html.Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RemoveDuplicateWords(ref string inputValue)
        {
            try
            {
                if (inputValue == null || inputValue.Trim() == "")
                    return inputValue;

                string[] arrValue = inputValue.Split('.', '!');
                if (arrValue == null || arrValue.Length == 0 || arrValue.Length == 1)
                    return inputValue;

                string strReturn = "";
                List<string> listValue = new List<string>();
                foreach (string item in arrValue)
                {
                    if (item.Trim() != "")
                    {
                        string strKhongdau = Utility.ConvertToNoSign(item.ToLower().Trim());
                        if (!listValue.Contains(strKhongdau))
                        {
                            listValue.Add(strKhongdau);
                            strReturn += item.Trim() + ".";
                        }
                    }
                }
                if (strReturn.EndsWith("."))
                    strReturn = strReturn.Substring(0, strReturn.Length - 1);
                return RemoveTagComment(ref strReturn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
