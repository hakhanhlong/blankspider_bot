using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BlankSpider.Spider.Utility
{
    /// <summary>
    /// Utility for Solution.
    /// </summary>
    public class Utility
    {


        #region Functions Utility for Numberic
        public static bool IsInteger(object obj)
        {
            try
            {
                Convert.ToInt32(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsIntegerNull(object obj)
        {
            try
            {
                if (obj == null) return true;
                if (IsInteger(obj) && obj.ToString().Trim() == "") return true;
                if (IsInteger(obj) && obj.ToString().Trim() == "0") return true;
                if (IsInteger(obj) && obj.ToString().Trim() == "-1") return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static int InitializeInteger
        {
            get
            {
                return -1;
            }
        }


        public static bool IsDouble(object obj)
        {
            try
            {
                Convert.ToDouble(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static int InitializeDouble
        {
            get
            {
                return 0;
            }
        }
        #endregion

        #region Functions Utility for DateTime


        static string[] _arrDateTimeRemove = {
            "chủ nhật",
            "thứ hai",
            "thứ ba",
            "thứ tư",
            "thứ năm",
            "thứ sáu",
            "thứ bẩy",
            "thứ bảy",
            "gmt+7"
        };


        
        


        public static bool IsDateTime(object obj)
        {
            try
            {
                Convert.ToDateTime(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsDateTimeNull(object obj)
        {
            try
            {
                if (obj == null) return true;
                if (IsDateTime(obj) && obj.ToString().Trim() == "") return true;
                if (IsDateTime(obj) && Convert.ToDateTime(obj) == DateTime.MinValue) return true;
                if (IsDateTime(obj) && obj.ToString().IndexOf("1/1/1900") > -1) return true;
                if (IsDateTime(obj) && obj.ToString().IndexOf("01/01/1900") > -1) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static DateTime InitializeDateTime
        {
            get
            {
                return DateTime.Parse("1/1/1753");
            }
        }
        public static int ConvertMonthENToVN(string strDateTime)
        {
            strDateTime = strDateTime.ToLower();
            if (strDateTime.Contains("jan"))
                return 1;
            else if (strDateTime.Contains("feb"))
                return 2;
            else if (strDateTime.Contains("mar"))
                return 3;
            else if (strDateTime.Contains("apr"))
                return 4;
            else if (strDateTime.Contains("may"))
                return 5;
            else if (strDateTime.Contains("jun"))
                return 6;
            else if (strDateTime.Contains("jul"))
                return 7;
            else if (strDateTime.Contains("aug"))
                return 8;
            else if (strDateTime.Contains("sep"))
                return 9;
            else if (strDateTime.Contains("oct"))
                return 10;
            else if (strDateTime.Contains("nov"))
                return 11;
            else if (strDateTime.Contains("dec"))
                return 12;

            return 0;
        }


        public static string ConvertToTrueDateTimeString(string strDateTime)
        {
            strDateTime = strDateTime.ToLower();
            if (strDateTime.Contains("/1/"))
                strDateTime = strDateTime.Replace("/1/", "/01/");
            else if (strDateTime.Contains("/2/"))
                strDateTime = strDateTime.Replace("/2/", "/02/");
            else if (strDateTime.Contains("/3/"))
                strDateTime = strDateTime.Replace("/3/", "/03/");
            else if (strDateTime.Contains("/4/"))
                strDateTime = strDateTime.Replace("/4/", "/04/");
            else if (strDateTime.Contains("/5/"))
                strDateTime = strDateTime.Replace("/5/", "/05/");
            else if (strDateTime.Contains("/6/"))
                strDateTime = strDateTime.Replace("/6/", "/06/");
            else if (strDateTime.Contains("/7/"))
                strDateTime = strDateTime.Replace("/7/", "/07/");
            else if (strDateTime.Contains("/8/"))
                strDateTime = strDateTime.Replace("/8/", "/08/");
            else if (strDateTime.Contains("/9/"))
                strDateTime = strDateTime.Replace("/9/", "/09/");


            try
            {
                string startString = strDateTime.Substring(0, strDateTime.IndexOf("/")); //process day
                if(startString.Length == 1)
                {
                    int day = Convert.ToInt32(startString);
                    if (day < 10)
                    {
                        strDateTime = "0" + strDateTime;
                    }
                }
                
            }
            catch{}
          



            return strDateTime;
        }

        public static string OnlyDateTime(string strDateTime)
        {
            strDateTime = strDateTime.ToLower();

            // filter datetime ###############################
            foreach(var item in _arrDateTimeRemove)
            {
                strDateTime = strDateTime.Replace(item, "");
            }
            //#################################################
            strDateTime = StringHelpers.ReplaceCharacter2(strDateTime).Trim();
            strDateTime = strDateTime.Substring(0, 10);

         
            strDateTime = ConvertToTrueDateTimeString(strDateTime).Trim();
            if (strDateTime.LastIndexOf(" -") > 0)
            {
                strDateTime = strDateTime.Replace("-", "").Trim();
            }


            //dd/MM/YYYY fornmate
            Match match = Regex.Match(strDateTime, @"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Value;
            }
            match = Regex.Match(strDateTime, @"^(?ni:(?=\d)((?'year'((1[6-9])|([2-9]\d))\d\d)(?'sep'[/.-])(?'month'0?[1-9]|1[012])\2(?'day'((?<!(\2((0?[2469])|11)\2))31)|(?<!\2(0?2)\2)(29|30)|((?<=((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(16|[2468][048]|[3579][26])00)\2\3\2)29)|((0?[1-9])|(1\d)|(2[0-8])))(?:(?=\x20\d)\x20|$))?((?<time>((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2}))?)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Value;
            }

            return string.Empty;
        }


        public static DateTime ConvertDateTimeStringForArchived(string strDatetime, out bool checkDateTime)
        {


            DateTime dateTime = new DateTime();
            checkDateTime = false;
            try
            {
                strDatetime = OnlyDateTime(strDatetime).Replace("\r\n", "");
                string[] _format_datetime = { "dd/MM/yyyy", "yyyy/MM/dd", "yyyy-MM-dd" };
                foreach (string format in _format_datetime)
                {
                    try
                    {
                        dateTime = DateTime.ParseExact(strDatetime, format, null);
                        checkDateTime = true;
                        break;
                    }
                    catch
                    {
                        checkDateTime = false;
                    }

                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
                checkDateTime = false;
            }


            return dateTime;



        }


        //public static DateTime ConvertDateTimeStringForArchived(string strDatetime, out bool checkDateTime)
        //{


        //    DateTime dateTime = new DateTime();
        //    string __date = "";
        //    try
        //    {
        //        strDatetime = OnlyDateTime(strDatetime).Replace("\r\n", "");

        //        string[] splitDateTime = strDatetime.Split(new char[] { '/' }, StringSplitOptions.None);
        //        string[] splitTime = splitDateTime[2].Split(new char[] { ':' }, StringSplitOptions.None);
        //        string year = splitTime[0].Split(new char[] { '-' }, StringSplitOptions.None)[0];

        //        //string strTime = splitDateTime[2].Replace(year + "-", "");


        //        //splitTime = strTime.Split(new char[] { ':' }, StringSplitOptions.None);


        //        try
        //        {
        //            //string _datetime = string.Format("{0}/{1}/{2} {3}:{4}", splitDateTime[1].Trim(), splitDateTime[0].Trim(), year.Trim(), splitTime[0].Trim(), splitTime[1].Trim());
        //            string _datetime = string.Format("{0}/{1}/{2}", splitDateTime[1].Trim(), splitDateTime[0].Trim(), year.Trim());
        //            __date = _datetime;
        //            dateTime = DateTime.ParseExact(_datetime, "M/d/yyyy", null);
        //            checkDateTime = true;
        //        }
        //        catch(Exception ex)
        //        {
        //            string test = __date;
        //            string msg = ex.Message;
        //            checkDateTime = false;
        //        }


        //    }
        //    catch(Exception ex)
        //    {

        //        string msg = ex.Message;
        //        checkDateTime = false;
        //    }


        //    return dateTime;



        //}

        #endregion

        #region Functions Utility for String
        public static bool IsStringNullOrEmpty(object obj)
        {
            try
            {
                if (obj == null) return true;
                if (obj.ToString().Trim() == string.Empty) return true;
                if (obj.ToString().Trim() == "") return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static string InitializeString
        {
            get
            {
                return string.Empty;
            }
        }
        #endregion

        #region Functions Utility for Boolean
        public static bool InitializeBool
        {
            get
            {
                return false;
            }
        }
        #endregion

        public static string ConvertToNoSign(string strValue)
        {
            try
            {
                const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
                const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
                int index = -1;
                char[] arrChar = FindText.ToCharArray();
                while ((index = strValue.IndexOfAny(arrChar)) != -1)
                {
                    int index2 = FindText.IndexOf(strValue[index]);
                    strValue = strValue.Replace(strValue[index], ReplText[index2]);
                }
                return strValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public static Encoding GetEncoding(EncodingType encodingType)
        {
            try
            {
                switch (encodingType)
                {
                    case EncodingType.ASCII:
                        return Encoding.ASCII;
                    case EncodingType.Default:
                        return Encoding.Default;
                    case EncodingType.Unicode:
                        return Encoding.Unicode;
                    case EncodingType.UTF32:
                        return Encoding.UTF32;
                    case EncodingType.UTF7:
                        return Encoding.UTF7;
                    case EncodingType.UTF8:
                        return Encoding.UTF8;
                    default:
                        return Encoding.UTF8;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Encoding GetEncoding(int encodingType)
        {
            try
            {
                switch (encodingType)
                {
                    case 0:
                        return Encoding.ASCII;
                    case 1:
                        return Encoding.Default;
                    case 2:
                        return Encoding.Unicode;
                    case 3:
                        return Encoding.UTF32;
                    case 4:
                        return Encoding.UTF7;
                    case 5:
                        return Encoding.UTF8;
                    default:
                        return Encoding.UTF8;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LogError(string functionName, Exception ex)
        {
            try
            {
                string split = "--------------------------------------------------------------------------------------------------------";
                string strLog = Utility.InitializeString;
                if (ex != null)
                    strLog = DateTime.Now + Environment.NewLine + functionName + Environment.NewLine + ex.ToString() + Environment.NewLine + split;
                else
                    strLog = DateTime.Now + Environment.NewLine + functionName + Environment.NewLine + split;
                string fileName = "log_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".txt";
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(strLog);
                    sw.Close();
                }
            }
            catch
            {
            }
        }

        public static void LogError(string fileName, string functionName, Exception ex)
        {
            try
            {
                string split = "--------------------------------------------------------------------------------------------------------";
                string strLog = Utility.InitializeString;
                if (ex != null)
                    strLog = DateTime.Now + Environment.NewLine + functionName + Environment.NewLine + ex.ToString() + Environment.NewLine + split;
                else
                    strLog = DateTime.Now + Environment.NewLine + functionName + Environment.NewLine + split;
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(strLog);
                    sw.Close();
                }
            }
            catch
            {
            }
        }

        public static void SetProperty(PropertyInfo pInfo, string propertyValue, object objectToSetValue)
        {
            try
            {
                if (pInfo == null)
                    return;

                System.Type pType = pInfo.PropertyType;

                if (pType == typeof(string))
                    pInfo.SetValue(objectToSetValue, propertyValue, null);
                else if (pType == typeof(bool))
                    pInfo.SetValue(objectToSetValue, Convert.ToBoolean(propertyValue), null);
                else if (pType == typeof(DateTime))
                    pInfo.SetValue(objectToSetValue, Convert.ToDateTime(propertyValue), null);
                else if (pType == typeof(short))
                    pInfo.SetValue(objectToSetValue, Convert.ToInt16(propertyValue), null);
                else if (pType == typeof(int))
                    pInfo.SetValue(objectToSetValue, Convert.ToInt32(propertyValue), null);
                else if (pType == typeof(long))
                    pInfo.SetValue(objectToSetValue, Convert.ToInt64(propertyValue), null);
                else if (pType == typeof(float))
                    pInfo.SetValue(objectToSetValue, Convert.ToSingle(propertyValue), null);
                else if (pType == typeof(double))
                    pInfo.SetValue(objectToSetValue, Convert.ToDouble(propertyValue), null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Normalize(ref string strURL)
        {
            if(!strURL.Contains("http://") && !strURL.Contains("https://"))
            {
                if (strURL.StartsWith("http://") == false)
                    strURL = "http://" + strURL;

                if (strURL.StartsWith("https://") == false)
                    strURL = "https://" + strURL;
            }

            strURL = strURL.Replace("&amp;", "&");
        }

        public static List<string> SortListRemoveWord(string[] arrList)
        {
            for (int i = 0; i < arrList.Length - 1; i++)
            {
                for (int j = i + 1; j < arrList.Length; j++)
                {
                    if (arrList[j].Length > arrList[i].Length)
                    {
                        string temp = arrList[i];
                        arrList[i] = arrList[j];
                        arrList[j] = temp;
                    }
                }
            }
            List<string> listSort = new List<string>();
            foreach (string item in arrList)
            {
                listSort.Add(item);
            }
            return listSort;
        }

        public static bool IsUrl(string url)
        {
            if (Utility.IsStringNullOrEmpty(url))
                return false;

            if (url.ToLower().Contains("http://") || url.ToLower().Contains("www."))
                return true;
            return false;
        }
    }
}
