using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirov_X
{
    public class Response
    {
        public Intent intent = new Intent();
        public Results[] results;
    }
    public class Intent
    {
        public int code;
    }
    public class Results
    {
        public int groupType;
        public string resultType;
        public Values values;
    }
    public class Values
    {
        public string text;
    }
}
