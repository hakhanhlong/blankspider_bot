using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlankSpider.Spider.Utility
{
    public class StringHelpers
    {
        #region GenerateKey

        public static string UniqueKey(int maxSize)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        public static string MD5Hash(string str)
        {
            byte[] buffer = MD5.Create().ComputeHash(Encoding.Default.GetBytes(str.ToLower()));
            var builder = new StringBuilder();
            foreach (byte t in buffer)
            {
                builder.AppendFormat("{0:x2}", t);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Encrypts a string using the SHA256 (Secure Hash Algorithm) algorithm.
        /// Details: http://www.itl.nist.gov/fipspubs/fip180-1.htm
        /// This works in the same manner as MD5, providing however 256bit encryption.
        /// </summary>
        /// <param name="data">A string containing the data to encrypt.</param>
        /// <returns>A string containing the string, encrypted with the SHA256 algorithm.</returns>
        public static string SHA256Hash(string data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Encrypts a string using the SHA384(Secure Hash Algorithm) algorithm.
        /// This works in the same manner as MD5, providing 384bit encryption.
        /// </summary>
        /// <param name="data">A string containing the data to encrypt.</param>
        /// <returns>A string containing the string, encrypted with the SHA384 algorithm.</returns>
        public static string SHA384Hash(string data)
        {
            SHA384 sha = new SHA384Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }


        /// <summary>
        /// Encrypts a string using the SHA512 (Secure Hash Algorithm) algorithm.
        /// This works in the same manner as MD5, providing 512bit encryption.
        /// </summary>
        /// <param name="data">A string containing the data to encrypt.</param>
        /// <returns>A string containing the string, encrypted with the SHA512 algorithm.</returns>
        public static string SHA512Hash(string data)
        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
        #endregion



        public static string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            var r = new Regex("(?:[^a-z0-9 -]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            string output = r.Replace(input, String.Empty);
            return output;
        }

        public static string GetStringBetween(string Str, string Seq, string SeqEnd)
        {
            string Orgi = Str;
            try
            {
                Str = Str.ToLower();
                Seq = Seq.ToLower();
                SeqEnd = SeqEnd.ToLower();

                int i = Str.IndexOf(Seq);

                if (i < 0)
                    return "";

                i = i + Seq.Length;

                int j = Str.IndexOf(SeqEnd, i);
                int end;

                if (j > 0) end = j - i;
                else end = Str.Length - i;

                return Orgi.Substring(i, end);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static List<string> GetListStringBetween(string input, string start, string end)
        {
            string orgInput = input;
            input = input.ToLower();
            start = start.ToLower();
            end = end.ToLower();
            List<string> list = new List<string>();
            try
            {
                int index = 0;
                while (index >= 0)
                {
                    index = input.IndexOf(start);
                    if (index < 0)
                        break;

                    index += start.Length;
                    int length;

                    int j = input.IndexOf(end, index);
                    if (j > 0)
                    {
                        length = j - index;
                        list.Add(orgInput.Substring(index, length));
                        if (j + end.Length < input.Length)
                        {
                            input = input.Substring(j + end.Length);
                            orgInput = orgInput.Substring(j + end.Length);
                        }
                        else break;
                    }
                    else
                        break;
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        public static string ReplaceCharacter(string word)
        {
            word = word.Replace(" ", "-");
            word = word.Replace("*", "");
            word = word.Replace("|", "");
            word = word.Replace("!", "");
            word = word.Replace("$", "");
            word = word.Replace("%", "");
            word = word.Replace("&", "");
            word = word.Replace("#", "");
            word = word.Replace("?", "");
            word = word.Replace(":", "");
            word = word.Replace("：", "");
            word = word.Replace(";", "");
            word = word.Replace("/", "");
            word = word.Replace(".", "");
            word = word.Replace(",", "");
            word = word.Replace("~", "");
            word = word.Replace("`", "");
            word = word.Replace("(", "");
            word = word.Replace(")", "");
            word = word.Replace("{", "");
            word = word.Replace("}", "");
            word = word.Replace("[", "");
            word = word.Replace("]", "");
            word = word.Replace("'", "");
            word = word.Replace("\"", "");
            word = word.Replace("  ", "");
            word = Regex.Replace(word, "([^A-Za-z0-9-]+)", String.Empty);
            //word = HttpUtility.HtmlEncode(word);
            return word;
        }

        public static string ReplaceCharacter2(string word)
        {
            //word = word.Replace(" ", "");
            word = word.Replace("&nbsp;", " ");
            word = word.Replace("&nbsp", " ");
            word = word.Replace("*", "");
            word = word.Replace("|", "-");
            word = word.Replace("!", "");
            word = word.Replace("$", "");
            word = word.Replace("%", "");
            word = word.Replace("&", "");
            word = word.Replace("#", "");
            word = word.Replace("?", "");
            //word = word.Replace(":", "");
            //word = word.Replace("：", "");
            word = word.Replace(";", "");
            //word = word.Replace("/", "");
            word = word.Replace(".", "");
            word = word.Replace(",", "");
            word = word.Replace("~", "");
            word = word.Replace("`", "");
            word = word.Replace("(", "");
            word = word.Replace(")", "");
            word = word.Replace("{", "");
            word = word.Replace("}", "");
            word = word.Replace("[", "");
            word = word.Replace("]", "");
            word = word.Replace("'", "");
            word = word.Replace("\"", "");
            word = word.Replace("  ", "");
            
            //word = Regex.Replace(word, "([^A-Za-z0-9-]+)", String.Empty);
            //word = HttpUtility.HtmlEncode(word);
            return word;
        }


        public static string ConvertToKD(string data)
        {
            if (string.IsNullOrEmpty(data)) return "";

            string xmau;
            string xketqua;
            xmau = "áàảãạâấấầẩẫậăắằẳẵặéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠÂẤẤẦẨẪẬĂẮẰẲẴẶÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";
            xketqua = "aaaaaaaaaaaaaaaaaaeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyydAAAAAAAAAAAAAAAAAAEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYD";

            if (data == "") return "";
            string kq = "";
            for (int i = 0; i < data.Length; i++)
            {
                int j = xmau.IndexOf(data[i]);
                if (j == -1) kq += data[i];
                else kq += xketqua[j];
            }

            //kq = ReplaceCharacter(kq);
            kq = HtmlUtility.RemoveAllTag(kq);
            return kq;

        }

        public static string ConvertToKhongDau(string data)
        {
            if (string.IsNullOrEmpty(data)) return "";

            string xmau;
            string xketqua;
            xmau = "áàảãạâấấầẩẫậăắằẳẵặéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠÂẤẤẦẨẪẬĂẮẰẲẴẶÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";
            xketqua = "aaaaaaaaaaaaaaaaaaeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyydAAAAAAAAAAAAAAAAAAEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYD";

            if (data == "") return "";
            string kq = "";
            for (int i = 0; i < data.Length; i++)
            {
                int j = xmau.IndexOf(data[i]);
                if (j == -1) kq += data[i];
                else kq += xketqua[j];
            }

            kq = ReplaceCharacter(kq);
            return kq;

        }


        public static string GetExtensionFiles(string filename)
        {

            return filename.Substring(filename.LastIndexOf('.') + 1);
        }

        public static string Base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData.Replace("+", "-").Replace("/", "_");
            }
            catch (Exception e)
            {
                return "";
            }
        }


        public static string Base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                data = data.Replace("-", "+").Replace("_", "/");
                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static string QueryString(IDictionary<string, object> dict)
        {
            var list = new List<string>();
            foreach (var item in dict)
            {
                list.Add(item.Key + "=" + item.Value);
            }
            return string.Join("&", list);
        }
    }
}
