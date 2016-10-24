using System.IO;
using System.Net;
using System.Text;

namespace WebScraping01_TianQi.Util
{
    public class HtmlHelper
    {
        /// <summary>
        /// 获取页面源代码HTML
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetWebClient(string url)
        {
            string strHTML = "";
            using (WebClient myWebClient = new WebClient())
            using (Stream myStream = myWebClient.OpenRead(url))
            {
                if (myStream != null)
                {
                    StreamReader sr = new StreamReader(myStream, Encoding.Default); //注意编码
                    strHTML = sr.ReadToEnd();
                }
            }
            return strHTML;

        }
    }
}