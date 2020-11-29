using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
namespace Accounting
{
    public partial class TestHtmlList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FindFile("C:\\code\\Accounting\\");
        }

        private void FindFile(string dir)
        {
            //在指定目錄下查詢文件，若符合查詢條件，將檔案寫入lsFile控制元件
            DirectoryInfo Dir = new DirectoryInfo(dir);
            try
            {
                foreach (DirectoryInfo d in Dir.GetDirectories())//查詢子目錄    
                {
                    FindFile(Dir + d.ToString() + "\\");
                }
                foreach (FileInfo f in Dir.GetFiles("*.html"))//查詢附檔名為xls的文件  
                {
                    lit_Menu.Text += "<div><a href=\""+f.ToString()+"\">"+ f.ToString() + "<a></div>";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    
}