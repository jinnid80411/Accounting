using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using Accounting.App_Code;
using System.Web.SessionState;

namespace Accounting.xml
{
    /// <summary>
    /// DayIncome 的摘要描述
    /// </summary>
    public class DayIncome : IHttpHandler, IRequiresSessionState
    {
        string id = "";
        string cs_code = "";
        ClsCompany objCP = new ClsCompany();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            Info objInfo = JsonConvert.DeserializeObject<Info>(strJson); // Deserialize<Info>(strJson);
            string Action = "";
            int input_1000 = 0;
            int input_100 = 0;
            int input_50 = 0;
            int input_10 = 0;
            int input_5 = 0;

            DataTable ResultDt = new DataTable();
            ResultDt.Columns.Add("result");
            ResultDt.Columns.Add("Msg");
            ResultDt.Columns.Add("input_1000");
            ResultDt.Columns.Add("input_100");
            ResultDt.Columns.Add("input_50");
            ResultDt.Columns.Add("input_10");
            ResultDt.Columns.Add("input_5");

            if (HttpContext.Current.Session["cs_code"] != null)
            {
                cs_code = HttpContext.Current.Session["cs_code"].ToString();
                if (objInfo != null)
                {
                    Action = objInfo.Action;
                    Int32.TryParse(objInfo.input_1000, out input_1000);
                    Int32.TryParse(objInfo.input_1000, out input_100);
                    Int32.TryParse(objInfo.input_1000, out input_50);
                    Int32.TryParse(objInfo.input_1000, out input_10);
                    Int32.TryParse(objInfo.input_1000, out input_5);
                }

                string result = "";
                string ResultMsg = "";
                /*
                Info.cg_code = cg_code;
                Info.CRUD = "";
                Info.c_name = "";*/
                switch (Action)
                {
                    case "Save":
                        if (objCP.InsertTodayInCome(cs_code, input_1000, input_100, input_50, input_10, input_5))
                        {
                            result = "OK";
                            ResultMsg = "編輯成功!";
                        }
                        else
                        {
                            result = "Error";
                            ResultMsg = "編輯失敗!";
                        }
                        break;

                    case "Read":
                        result = "OK";
                        ResultMsg = "讀取成功!";
                        break;
                }
                DataTable Dt = objCP.GetCompanyShopToday(cs_code);

                if (Dt.Rows.Count > 0)
                {
                    ResultDt.Rows.Add(result, ResultMsg
                        , Dt.Rows[0]["ic_1000"].ToString()
                        , Dt.Rows[0]["ic_100"].ToString()
                        , Dt.Rows[0]["ic_50"].ToString()
                        , Dt.Rows[0]["ic_10"].ToString()
                        , Dt.Rows[0]["ic_5"].ToString()
                        );
                }
                else
                {
                    ResultDt.Rows.Add("OK"
                        , ""
                        , ""
                        , ""
                        , ""
                        , ""
                        );
                }

            }
            else
            {
                ResultDt.Rows.Add("Error"
                    , "請先選擇店面!"
                    , ""
                    , ""
                    , ""
                    , ""
                    );
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
            public string input_1000 { set; get; }
            public string input_100 { set; get; }
            public string input_50 { set; get; }
            public string input_10 { set; get; }
            public string input_5 { set; get; }
        }
    }
}