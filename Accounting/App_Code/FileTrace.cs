using System;
using System.Data;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;

using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;



namespace Accounting.App_Code
{
    public class FileTrace
    {
        public System.Text.Encoding Encoding = System.Text.Encoding.UTF8;
        // 將訊息寫入file
        public void ftrace(string value, string name)
        {
            System.Collections.Specialized.NameValueCollection SvrVars = System.Web.HttpContext.Current.Request.ServerVariables;

            string SCRIPT_NAME;
            SCRIPT_NAME = System.Web.HttpContext.Current.Request.ServerVariables["Script_Name"].ToString();
            string file = SCRIPT_NAME.Replace("/", "_") + ".txt";
            string mesg;
            if (name != null)
            {
                mesg = name + " : " + value;
            }
            else
            {
                mesg = value;
            }
            dumpCustomFile(file, mesg);
        }

        public void ftrace(string value)
        {
            ftrace(value, null);
        }

        private void dumpCustomFile(string filename, string mesg)
        {
            string file_path;

            file_path = HttpContext.Current.Server.MapPath("~/DeBug/" + filename).ToString();

            if (!System.IO.File.Exists(file_path))
            {
                // create a new file
            }

            StreamWriter sw = new StreamWriter(file_path, true, System.Text.Encoding.UTF8);
            sw.WriteLine(getCurDateTimeStr() + mesg);
            sw.Close();
        }

        /* Date/Time Functions */
        private string getCurDateTimeStr()
        {
            DateTime d = DateTime.Now;

            return (d.ToString("yyyy/MM/dd HH:mm:ss") + " ");
        }


        private string SqlQuote(string sql)
        {
            return (sql.Replace("'", "''"));
        }

    }
}