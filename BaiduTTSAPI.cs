using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kirov_X
{
    class BaiduTTSAPI
    {
        string APIKey = "4mRKhi0BMOwMLbhFKpacvoZe";
        string SecretKey = "CFWYBh896yLXKNeKankcAuPKqOtZQ7zi";
        string API = @"https://openapi.baidu.com/oauth/2.0/token?grant_type=client_credentials&client_id=[API_KEY]&client_secret=[SECRET_KEY]";
        string API2 = @"https://tsn.baidu.com/text2audio?tex=[TEX]&lan=zh&cuid=B23CA07A276181A4&ctp=1&tok=[TOKEN]&per=111&aue=6&pit=7&spd=4";
        public string token = "";

        public string getToken()
        {
            string get;
            API = API.Replace("[API_KEY]", APIKey);
            API = API.Replace("[SECRET_KEY]", SecretKey);
            HttpWebRequest webRequest = WebRequest.Create(API) as HttpWebRequest;
            webRequest.Method = "GET";
            using (WebResponse wr = webRequest.GetResponse())
            {
                using (StreamReader sr = new StreamReader(wr.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    get = sr.ReadToEnd().ToString();
                }
            }
            //System.IO.File.WriteAllText(@"tts_post.json", text);
            System.IO.File.WriteAllText(@"tts_get.json", get);
            var result = JObject.Parse(get);
            JToken value = result.GetValue("access_token");
            get = value.ToString();
            return get;
        }

        public bool getAudio(string token,string text)
        {
            string encode = HttpUtility.UrlEncode(HttpUtility.UrlEncode(text));
            string post = API2.Replace("[TEX]", encode);
            post = post.Replace("[TOKEN]", token);
            try
            {
                HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(post);
                HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                Stream st = myrp.GetResponseStream();
                Stream so = new System.IO.FileStream("audioCache.wav", System.IO.FileMode.Create);
                byte[] by = new byte[102400];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
                myrp.Close();
                Myrq.Abort();
                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        public void playSound()
        {
            //try
            //{
                SoundPlayer player = new SoundPlayer();
                FileStream fs = new FileStream(@"audioCache.wav", FileMode.Open, FileAccess.Read);
                //player.SoundLocation = @"audioCache.wav";
                player.Stream = fs;
                player.Load();
                player.Play();
            //}catch (Exception e)
            //{
            //    return;
            //}

        }
    }
}
