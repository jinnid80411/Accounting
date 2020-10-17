using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using Accounting.App_Code;


namespace Accounting.xml
{
    /// <summary>
    /// CompanyUsers 的摘要描述
    /// </summary>
    public class CompanyUsers : IHttpHandler
    {

        ClsCompany objCP = new ClsCompany();
        ClsTool objTL = new ClsTool();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            Info objInfo = JsonConvert.DeserializeObject<Info>(strJson); // Deserialize<Info>(strJson);
            string Action = "";          

            if (objInfo != null)
            {
                Action = objInfo.Action;
            }
            DataTable ResultDt = new DataTable();
            ResultDt.Columns.Add("result");
            ResultDt.Columns.Add("Msg");
            ResultDt.Columns.Add("u_code");
            ResultDt.Columns.Add("u_name");

            DataTable Dt = new DataTable();
            switch (Action)
            {
                case "1"://已選擇會員
                    break;
                case "2"://所有會員去除已選擇
                    break;
                case "3"://編輯
                    if (objCP.UpdateCompanyUsers(objInfo.type, objInfo.code, objInfo.u_code))
                    {
                        ResultDt.Rows.Add("OK", "", "","");
                    }
                    else
                        ResultDt.Rows.Add("Error", "編輯失敗", "", "");

                    break;
            }

            if (Action == "1" || Action == "2")
            {
                Dt = objCP.GetCompanyUsers(objInfo.type, objInfo.code, "");
                if (Action == "1")
                {
                    string u_code_array = "";
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        if (u_code_array != "")
                            u_code_array += ",";
                        u_code_array += Dt.Rows[i]["u_code"].ToString();
                    }

                    Dt = objCP.GetUsersDataNotSelect(u_code_array);
                }

                if(Dt.Rows.Count>0)
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {

                        ResultDt.Rows.Add("OK", "", Dt.Rows[i]["u_code"].ToString(), Dt.Rows[i]["u_name"].ToString());

                    }
                else
                    ResultDt.Rows.Add("NoData", "", "","");

            }
            string ajson = JsonConvert.SerializeObject(ResultDt, Formatting.Indented);
            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            context.Response.Write(ajson);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public class Info
        {
            public string Action { set; get; }
            public string type { set; get; }
            public string code { set; get; }
            public string u_code { set; get; }
        }
    }
}