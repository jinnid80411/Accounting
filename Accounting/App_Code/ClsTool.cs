using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accounting.App_Code
{
    public class ClsTool
    {
        public string DateChange(string input,string Format)
        {
            DateTime output = new DateTime();
            if (DateTime.TryParse(input, out output))
            {
                return output.ToString(Format);
            }
            else
                return "";
            
        }
    }
}