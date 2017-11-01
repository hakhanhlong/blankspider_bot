using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BlankSpider.Spider.Utility
{
    public class RegexUtility
    {
        public static string RegexGetAll(ref string strValue, string regexValue)
        {
            try
            {
                if (Utility.IsStringNullOrEmpty(strValue) || Utility.IsStringNullOrEmpty(regexValue))
                    return Utility.InitializeString;

                Match mt = (new Regex(regexValue)).Match(strValue);
                if (mt.Success)
                    return mt.Value;

                return Utility.InitializeString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RegexGetText(ref string strValue, string regexValue)
        {
            try
            {
                if (Utility.IsStringNullOrEmpty(strValue) || Utility.IsStringNullOrEmpty(regexValue))
                    return Utility.InitializeString;

                Match mt = (new Regex(regexValue)).Match(strValue);
                if (mt.Success)
                    return mt.Groups["text"].Value;

                return Utility.InitializeString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<string> RegexGetListAll(ref string strValue, string regexValue)
        {
            try
            {
                if (Utility.IsStringNullOrEmpty(strValue) || Utility.IsStringNullOrEmpty(regexValue))
                    return null;

                MatchCollection mtCol = (new Regex(regexValue)).Matches(strValue);
                if (mtCol != null && mtCol.Count > 0)
                {
                    List<string> listValue = new List<string>();
                    foreach (Match mt in mtCol)
                        listValue.Add(mt.Value);
                    return listValue;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<string> RegexGetListText(ref string strValue, string regexValue)
        {
            try
            {
                if (Utility.IsStringNullOrEmpty(strValue) || Utility.IsStringNullOrEmpty(regexValue))
                    return null;

                MatchCollection mtCol = (new Regex(regexValue)).Matches(strValue);
                if (mtCol != null && mtCol.Count > 0)
                {
                    List<string> listValue = new List<string>();
                    foreach (Match mt in mtCol)
                        listValue.Add(mt.Groups["text"].Value);
                    return listValue;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
