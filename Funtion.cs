using System;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace Kirov_X
{
    static class Funtion
    {
        static String dir = @"\";
        static String userName = "root";
        static String apilink = "http://openapi.tuling123.com/openapi/api/v2";
        static String funtions = "help;echo;tuling;cls;mkdir;cd;speak;";
        static char[] space = { ' ' };
        public static BaiduTTSAPI baidu = new BaiduTTSAPI();

        public static async void ConsoleInit(TextBox t)
        {
            MainWindow.isLoading = true;
            t.Text = "";
            Logger("Kirov-X Kernel Booting",t);
            await Task.Delay(300);
            Logger("Loading Files",t);
            await Task.Delay(300);
            Logger("Synchronizing Critical Files", t);
            await Task.Delay(300);
            Logger("Mounting Kernel to Main Thread",t);
            await Task.Delay(300);
            Logger("Pre-Boot Process Completed",t);
            await Task.Delay(300);
            Logger("Initializing System",t);
            await Task.Delay(300);
            Logger("System Booted,Now Starting User Interface",t);
            await Task.Delay(2000);
            t.Text = "";
            t.Text += userName + "@" + dir + ">";
            await Task.Run(new Action(SetFlag));
            
        }

        private static async void SetFlag()
        {
            //Thread.Sleep(3800);
            MainWindow.isLoading = false;
        }

        public static void ConsoleRun(string s,TextBox t)
        {
            t.Text += s + "\n";
            string[] arr = s.Split(space, options: StringSplitOptions.RemoveEmptyEntries);
            if (arr[0] == "help")
            {
                help(s,t);
            }
            else if (arr[0] == "echo")
            {
                echo(s,t);
            }
            else if (arr[0] == "tuling")
            {
                tuling(s,t);
            }
            else if (arr[0] == "cls")
            {
                cls(t);
            }
            else if (arr[0] == "mkdir")
            {
                mkdir(s,t);
            }
            else if (arr[0] == "cd")
            {
                cd(s, t);
            }
            else if (arr[0] == "speak")
            {
                speak(s, t);
            }
            else
            {
                ConsoleDisplay("Command Not Found,Use \"help\" For More Infomations",t);
            }
            return;
        }

        public static void ConsoleDisplay(string s,TextBox t)
        {
            t.Text += s + "\n";
            t.Text += userName + "@" + dir + ">";
            return;
        }

        public static void Logger(string s,TextBox t)
        {
            t.Text += "[" + DateTime.Now.ToString("hh:mm:ss") + "]    " + s + "\n";
        }

        private static void help(string s,TextBox t)
        {
            s = s.Remove(0, 4);
            string[] arr = s.Split(space, options: StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 0)
            {
                foreach (string a in funtions.Split(';'))
                {
                    t.Text += a + "\n";
                }
                ConsoleDisplay("", t);
            }
            else
            {
                if (arr[0] == "help")
                {
                    ConsoleDisplay("显示帮助信息\nUsage:help [Command]", t);
                }
                else if (arr[0] == "echo")
                {
                    ConsoleDisplay("显示一段文本\nUsage:echo [Text]", t);
                }
                else if (arr[0] == "cls")
                {
                    ConsoleDisplay("清空屏幕\nUsage:cls", t);
                }
                else if (arr[0] == "mkdir")
                {
                    ConsoleDisplay("创建一个目录\nUsage:mkdir [Name]", t);
                }
                else if (arr[0] == "cd")
                {
                    ConsoleDisplay("进入一个目录\nUsage:cd [Name]", t);
                }
                else if (arr[0] == "tuling")
                {
                    ConsoleDisplay("引用图灵机器人进行对话\nUsage:tuling [Text]", t);
                }
                else if (arr[0] == "speak")
                {
                    ConsoleDisplay("引用百度TTS引擎进行语音合成\nUsage:speak [Text]", t);
                }
                
            }
        }

        private static void echo(string s,TextBox t)
        {
            if (s.Length == 4)
            {
                ConsoleDisplay("",t);
                return;
            }
            s = s.Remove(0,5);
            ConsoleDisplay(s,t);
        }

        private static void cls(TextBox t)
        {
            t.Text = "";
            t.Text += userName + "@" + dir + ">";
        }
        
        private static void mkdir(string s,TextBox t)
        {
            s = s.Remove(0,5);
            string[] arr = s.Split(space, options: StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 0)
            {
                ConsoleDisplay("Invalid Argument",t);
            }
            else
            {
                if (System.IO.Directory.Exists(arr[0]))
                {
                    ConsoleDisplay("Error:Directory Already Exists",t);
                    return;
                }
                System.IO.Directory.CreateDirectory(arr[0]);
                ConsoleDisplay("Directory \"" + arr[0] + "\" Created Successfully",t);
            }
        }

        private static void cd(string s, TextBox t)
        {
            s = s.Remove(0, 2);
            string[] arr = s.Split(space, options: StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 0)
            {
                ConsoleDisplay("Invalid Argument", t);
            }
            else
            {
                if (!System.IO.Directory.Exists(arr[0]))
                {
                    ConsoleDisplay("Error:Directory Not Exists", t);
                    return;
                }
                dir += arr[0];
                ConsoleDisplay("", t);
            }
        }

        private static void tuling(String para = null, TextBox t = null)
        {
            string text,get;
            API api = new API();
            para = para.Remove(0, 7);
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
            //t.Text += res.results[0].values.text;
            speak("speak" + res.results[0].values.text, t);
            return;
        }

        private static void speak(string s,TextBox t)
        {
            s = s.Remove(0, 5);
            Funtion.baidu.token = Funtion.baidu.getToken();
            Funtion.baidu.getAudio(Funtion.baidu.token, s);
            //baidu.getAudio("24.bb6c896c1b9e79fbab2b5f496681ac9a.2592000.1584161326.282335-18455211","这是一段测试语音");
            Funtion.baidu.playSound();
            ConsoleDisplay(s,t);
        }
    }
}
