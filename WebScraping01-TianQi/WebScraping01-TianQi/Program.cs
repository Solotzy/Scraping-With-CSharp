using System;
using HtmlAgilityPack;
using WebScraping01_TianQi.Util;

namespace WebScraping01_TianQi
{
    class Program
    {
        static void Main(string[] args)
        {
            ParsePageByArea("hubei");
            Console.WriteLine("**********************************");
            ParsePageByCityMonth("wuhan", 2016, 10);
            Console.Read();
        }

        public static void ParsePageByArea(string cityCode)
        {
            //更改链接格式和省份代码构造URL
            string url = String.Format("http://www.tianqihoubao.com/lishi/{0}.htm", cityCode);
            //下载网页源代码
            var docText = HtmlHelper.GetWebClient(url);
            //加载源代码，获取文档对象
            var doc = new HtmlDocument();
            doc.LoadHtml(docText);
            //更改xpath获取总的对象，如果不为空，就继续选择dl标签
            var res = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[6]/div[1]/div[1]/div[3]");
            if (res != null)
            {
                var list = res.SelectNodes("dl"); //选择标签数组
                if(list.Count < 1) return;
                foreach (var item in list)
                {
                    var dd = item.SelectSingleNode("dd").SelectNodes("a");
                    foreach (var node in dd)
                    {
                        var text = node.InnerText.Trim();
                        //拼音代码要从href属性中进行分割提取
                        var href = node.Attributes["href"].Value.Trim().Split('/', '.');
                        Console.WriteLine("{0}:{1}", text, href[href.Length - 2]);
                    }
                }
            }
        }

        public static void ParsePageByCityMonth(string cityCode, int year, int month)
        {
            //更改拼音代码，月份信息构造URL
            string url = String.Format("http://www.tianqihoubao.com/lishi/{0}/month/{1}{2:D2}.html",
                cityCode, year, month);
            //获取该链接的源代码
            var docText = HtmlHelper.GetWebClient(url);
            //加载源代码，获取页面结构对象
            var doc = new HtmlDocument();
            doc.LoadHtml(docText);
            //根据xpath获取表格对象
            var res = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[6]/div[1]/div[1]/table[1]");
            if (res != null)
            {
                //获取所有行
                var list = res.SelectNodes("tr");
                list.Remove(0); //第一行是表头
                //遍历每一行，获取日期，以及天气状况等信息
                foreach (var item in list)
                {
                    var dd = item.SelectNodes("td");
                    // 日期 -  - 气温 - 风力风向
                    if (dd.Count != 4) continue;
                    //获取当前行日期
                    var date1 = dd[0].InnerText.Replace("\r\n", "").Replace(" ", "").Trim();
                    //获取当前行天气状况
                    var tq = dd[1].InnerText.Replace("\r\n", "").Replace(" ", "").Trim();
                    //获取当前行气温
                    var qw = dd[2].InnerText.Replace("\r\n", "").Replace(" ", "").Trim();
                    //获取当前行风力风向
                    var fx = dd[3].InnerText.Replace("\r\n", "").Replace(" ", "").Trim();
                    //输出
                    Console.WriteLine("{0}:{1},{2},{3}", date1, tq, qw, fx);
                }
            }
        }
    }
}
