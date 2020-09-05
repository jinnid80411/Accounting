using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using Accounting.App_Code;
namespace Accounting.xml
{
    /// <summary>
    /// CompanyList 的摘要描述
    /// </summary>
    public class CompanyList : IHttpHandler
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
            Info.c_name = "";*/
            DataTable ResultDt = new DataTable();
            ResultDt.Columns.Add("result");
            ResultDt.Columns.Add("Msg");
            DataTable Dt = new DataTable();
            switch (Action)
            {
                case "SendEdit":
                    if (objCP.CompanyCRUD(objInfo.CRUD, objInfo.c_code, objInfo.c_name, objInfo.c_id, objInfo.c_address, objInfo.c_telephone, objInfo.c_builddate, objInfo.cg_code))
                    {

                        if (objInfo.CRUD == "U")
                        {
                            Dt = objCP.GetCompanyData(objInfo.c_code, true, objInfo.cg_code);
                            ResultDt.Columns.Add("c_code");
                            ResultDt.Columns.Add("c_name");
                            ResultDt.Columns.Add("c_id");
                            ResultDt.Columns.Add("c_address");
                            ResultDt.Columns.Add("c_telephone");
                            ResultDt.Columns.Add("c_builddate");                            
                            for (int i = 0; i < Dt.Rows.Count; i++)
                            {
                                string c_builddate = "";
                                if (Dt.Rows[i]["c_builddate"].ToString() != "")
                                    c_builddate = Convert.ToDateTime(Dt.Rows[i]["c_builddate"]).ToString("yyyy/MM/dd");
                                ResultDt.Rows.Add("OK", "", Dt.Rows[i]["c_code"].ToString(), Dt.Rows[i]["c_name"].ToString()
                                    , Dt.Rows[i]["c_id"].ToString(), Dt.Rows[i]["c_address"].ToString()
                                    , Dt.Rows[i]["c_telephone"].ToString(), c_builddate);
                            }
                        }
                        else
                        {
                            Dt = objCP.GetCompanyData("", true, objInfo.cg_code);
                            if (objInfo.CRUD == "D" && Dt.Rows.Count == 0)
                            {
                                ResultDt.Rows.Add("NoData", "");

                            }
                            else
                            {
                                ResultDt.Columns.Add("c_code");
                                ResultDt.Columns.Add("c_name");
                                ResultDt.Columns.Add("c_id");
                                ResultDt.Columns.Add("c_address");
                                ResultDt.Columns.Add("c_telephone");
                                ResultDt.Columns.Add("c_builddate");
                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string c_builddate = "";
                                    if (Dt.Rows[i]["c_builddate"].ToString() != "")
                                        c_builddate = Convert.ToDateTime(Dt.Rows[i]["c_builddate"]).ToString("yyyy/MM/dd");
                                    ResultDt.Rows.Add("OK", "", Dt.Rows[i]["c_code"].ToString(), Dt.Rows[i]["c_name"].ToString()
                                        , Dt.Rows[i]["c_id"].ToString(), Dt.Rows[i]["c_address"].ToString()
                                        , Dt.Rows[i]["c_telephone"].ToString(), c_builddate);
                                }
                            }
                        }
                    }
                    else
                    {

                        ResultDt.Rows.Add("0", "編輯失敗");
                    }
                    break;
                case "GetData":
                    Dt = objCP.GetCompanyData(objInfo.c_code, true, objInfo.cg_code);
                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("c_code");
                        ResultDt.Columns.Add("c_name");
                        ResultDt.Columns.Add("c_id");
                        ResultDt.Columns.Add("c_address");
                        ResultDt.Columns.Add("c_telephone");
                        ResultDt.Columns.Add("c_builddate");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            string c_builddate = "";
                            if (Dt.Rows[i]["c_builddate"].ToString() != "")
                                c_builddate = Convert.ToDateTime(Dt.Rows[i]["c_builddate"]).ToString("yyyy/MM/dd");
                            ResultDt.Rows.Add("OK", "", Dt.Rows[i]["c_code"].ToString(), Dt.Rows[i]["c_name"].ToString()
                                , Dt.Rows[i]["c_id"].ToString(), Dt.Rows[i]["c_address"].ToString()
                                , Dt.Rows[i]["c_telephone"].ToString(), c_builddate);
                        }
                    }
                    else
                    {
                        ResultDt.Rows.Add("NoData", "");
                    }
                    break;
                case "ShowEdit":
                    Dt = objCP.GetCompanyData(objInfo.c_code, true, objInfo.cg_code);
                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("c_code");
                        ResultDt.Columns.Add("c_name");
                        ResultDt.Columns.Add("c_id");
                        ResultDt.Columns.Add("c_address");
                        ResultDt.Columns.Add("c_telephone");
                        ResultDt.Columns.Add("c_builddate");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            string c_builddate = "";
                            if (Dt.Rows[i]["c_builddate"].ToString() != "")
                                c_builddate = Convert.ToDateTime(Dt.Rows[i]["c_builddate"]).ToString("yyyy/MM/dd");
                            ResultDt.Rows.Add("OK", "", Dt.Rows[i]["c_code"].ToString(), Dt.Rows[i]["c_name"].ToString()
                                , Dt.Rows[i]["c_id"].ToString(), Dt.Rows[i]["c_address"].ToString()
                                , Dt.Rows[i]["c_telephone"].ToString(), c_builddate);
                        }
                    }
                    else
                    {
                        ResultDt.Rows.Add("NoData", "");
                    }
                    break;
                case "GetAll":
                    Dt = objCP.GetCompanyData("", true, objInfo.cg_code);
                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("c_code");
                        ResultDt.Columns.Add("c_name");
                        ResultDt.Columns.Add("c_id");
                        ResultDt.Columns.Add("c_address");
                        ResultDt.Columns.Add("c_telephone");
                        ResultDt.Columns.Add("c_builddate");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            string c_builddate = "";
                            if (Dt.Rows[i]["c_builddate"].ToString() != "")
                                c_builddate = Convert.ToDateTime(Dt.Rows[i]["c_builddate"]).ToString("yyyy/MM/dd");
                            ResultDt.Rows.Add("OK", "", Dt.Rows[i]["c_code"].ToString(), Dt.Rows[i]["c_name"].ToString()
                                , Dt.Rows[i]["c_id"].ToString(), Dt.Rows[i]["c_address"].ToString()
                                , Dt.Rows[i]["c_telephone"].ToString(), c_builddate);
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
            public string c_name { set; get; }
            public string c_code { set; get; }
            public string c_id { set; get; }
            public string c_address { set; get; }
            public string c_telephone { set; get; }
            public string c_builddate { set; get; }
        }
    }
}