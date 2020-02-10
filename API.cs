using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirov_X
{
    public class API
    {
        public int reqType = 0;
        public Perception perception = new Perception();
        public UserInfo userInfo = new UserInfo();
    }
    public class Perception
    {
        public InputText inputText = new InputText();
    }
    public class InputText
    {
        public string text = "";
    }
    public class UserInfo
    {
        public string apiKey = "1bd910e8b9a14254941bdc03da63b3d5";
        public string userId = "B23CA07A276181A4"; 
    }
}
