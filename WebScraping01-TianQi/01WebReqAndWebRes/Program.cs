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

        /// <summary>
        /// 方法一
        /// </summary>
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

        /// <summary>
        /// 请求Web页并以流的形式检索结果
        /// </summary>
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
    }
}
