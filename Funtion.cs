using System;
using System.Windows.Controls;
using Newtonsoft.Json;
using Kirov_X;
using System.Net;
using System.IO;

namespace Kirov_X
{
    static class Funtion
    {
        static String apilink = "http://openapi.tuling123.com/openapi/api/v2";
        static String funtions = "help;echo;";

        public static String Run(string s,TextBox t)
        {
            string[] arr = s.Split(' ');
            if (arr[0].Equals("echo"))
            {
                return Echo(s,t);
            }
            else if (arr[0].Equals("help"))
            {
                return Help(s, t);
            }
            else if (arr[0].Equals("say"))
            {
                Say(s,t);
                return null;
            }
            else
            {
                return "Command Not Found";
            }
        }

        private static String Echo(String para = null,TextBox t = null)
        {
            String text = null;
            para = para.Replace("echo ","");
            text = para;
            t.Text += text;
            return text;
        }

        private static String Help(String para = null,TextBox t = null)
        {
            string ret = null;
            if(para == null)
            {
                foreach(string s in (funtions.Split(';')))
                {
                    ret += s + "\n";
                    t.Text += ret;
                }
            }
            else
            {
                para = para.Replace("help ", "");
                if(para == "help")
                {
                    ret = "Usage:help [Command]";
                    t.Text += ret;
                }
                else if (para == "echo")
                {
                    ret = "Usage:echo [Text]";
                    t.Text += ret;
                }
                return ret;
            }
            return ret;
        }

        private static void Say(String para = null, TextBox t = null)
        {
            string text,get;
            API api = new API();
            para = para.Replace("connect ", "");
            api.perception.inputText.text = para;
            text = JsonConvert.SerializeObject(api);
            HttpWebRequest webRequest = WebRequest.Create(apilink) as HttpWebRequest;
            webRequest.Method = "POST";
            Stream newStream = webRequest.GetRequestStream();
            byte[] decBytes = System.Text.Encoding.UTF8.GetBytes(text);
            newStream.Write(decBytes, 0, decBytes.Length);
            newStream.Flush();
            using (WebResponse wr = webRequest.GetResponse())
            {
                using (StreamReader sr = new StreamReader(wr.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    get = sr.ReadToEnd().ToString();
                }
            }
            Response res = JsonConvert.DeserializeObject<Response>(get);

            System.IO.File.WriteAllText(@"post.json", text);
            System.IO.File.WriteAllText(@"get.json", get);
            t.Text += res.results[0].values.text;
            return;
        }
    }
}
