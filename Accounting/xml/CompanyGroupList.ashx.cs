using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using Accounting.App_Code;

namespace Accounting.xml
{
    /// <summary>
    /// CompanyGroupList 的摘要描述
    /// </summary>
    public class CompanyGroupList : IHttpHandler
    {
        ClsCompany objCP = new ClsCompany();
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

            /*
            Info.cg_code = cg_code;
            Info.CRUD = "";
            Info.cg_name = "";*/
            DataTable ResultDt = new DataTable();
            ResultDt.Columns.Add("result");
            ResultDt.Columns.Add("Msg");
            DataTable Dt = new DataTable();
            switch (Action)
            {
                case "SendEdit":
                    break;
                case "GetData":
                    break;
                case "ShowEdit":
                    break;
                case "GetAll":
                    Dt = objCP.GetCompanyGroupData("",true);
                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("cg_code");
                        ResultDt.Columns.Add("cg_name");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            ResultDt.Rows.Add("OK","", Dt.Rows[i]["cg_code"].ToString(), Dt.Rows[i]["cg_name"].ToString());
                        }
                    }
                    else
                    {
                        ResultDt.Rows.Add("NoData", "");
                    }
                    break;
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
            public string cg_code { set; get; }
            public string CRUD { set; get; }
            public string cg_name { set; get; }
        }
    }
}