using System;
using System.IO;
using System.Net;
using System.Text;

namespace _01WebReqAndWebRes
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetResponse();
            OpenRead();
            Console.Read();
        }

        #region 01.如何请求Web页并以流的形式检索结果
        static void OpenRead()
        {
            using (WebClient myClient = new WebClient())
            using (Stream response = myClient.OpenRead("http://www.baidu.com"))
            using (StreamReader reader = new StreamReader(response, Encoding.UTF8))
            {
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
            }
        }
        #endregion

        #region 02.如何使用WebRequest类请求数据
        static void GetResponse()
        {
            // 为指定的URI方案初始化一个WebRequest实例
            WebRequest request = WebRequest.Create("http://www.baidu.com");
            // 获取或设置用于对Internet资源请求进行身份验证的网络凭据
            request.Credentials = CredentialCache.DefaultCredentials;
            Encoding encode = Encoding.GetEncoding("utf-8");
            // 返回对Internet请求的响应
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream dataStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(dataStream, encode))
            {
                // 获取与响应一起返回的状态说明
                Console.WriteLine(response.StatusDescription);
                // 获取响应的状态
                Console.WriteLine(response.StatusCode);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
            }
        }
        #endregion

        #region 03.如何使用WebRequest类发送数据

        static void PostRequest()
        {
            WebRequest request = WebRequest.Create("http://www.baidu.com");
            request.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest) request).UserAgent = ".NET Framework Example Client";
            request.Method = "POST";
            string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            request.ContentType = "pplication/x-www-form-urlencoded";

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        #endregion
    }
}
