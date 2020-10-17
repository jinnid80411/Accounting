using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using Accounting.App_Code;

namespace Accounting.xml
{
    /// <summary>
    /// CompanyShopList 的摘要描述
    /// </summary>
    public class CompanyShopList : IHttpHandler
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
                    if (objCP.CompanyShopCRUD(objInfo.CRUD, objInfo.cs_code, objInfo.cs_name, objInfo.cs_address, objInfo.cs_telephone, objInfo.cg_code, objInfo.c_code))
                    {

                        if (objInfo.CRUD == "U")
                        {
                            Dt = objCP.GetCompanyShopData(objInfo.cs_code, true, objInfo.c_code, objInfo.cg_code);
                            ResultDt.Columns.Add("cg_code");
                            ResultDt.Columns.Add("c_code");
                            ResultDt.Columns.Add("cs_code");
                            ResultDt.Columns.Add("cs_name");
                            ResultDt.Columns.Add("cs_address");
                            ResultDt.Columns.Add("cs_telephone");
                            for (int i = 0; i < Dt.Rows.Count; i++)
                            {
                                ResultDt.Rows.Add("OK", "", Dt.Rows[i]["cg_code"].ToString(), Dt.Rows[i]["c_code"].ToString(), Dt.Rows[i]["cs_code"].ToString(), Dt.Rows[i]["cs_name"].ToString()
                                    , Dt.Rows[i]["cs_address"].ToString(), Dt.Rows[i]["cs_telephone"].ToString());
                            }
                        }
                        else
                        {
                            Dt = objCP.GetCompanyShopData("", true, objInfo.c_code, objInfo.cg_code);
                            if (objInfo.CRUD == "D" && Dt.Rows.Count == 0)
                            {
                                ResultDt.Rows.Add("NoData", "");

                            }
                            else
                            {
                                ResultDt.Columns.Add("cg_code");
                                ResultDt.Columns.Add("c_code");
                                ResultDt.Columns.Add("cs_code");
                                ResultDt.Columns.Add("cs_name");
                                ResultDt.Columns.Add("cs_address");
                                ResultDt.Columns.Add("cs_telephone");
                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    ResultDt.Rows.Add("OK", "", Dt.Rows[i]["cg_code"].ToString(), Dt.Rows[i]["c_code"].ToString(), Dt.Rows[i]["cs_code"].ToString(), Dt.Rows[i]["cs_name"].ToString()
                                    , Dt.Rows[i]["cs_address"].ToString(), Dt.Rows[i]["cs_telephone"].ToString());
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
                    Dt = objCP.GetCompanyShopData(objInfo.cs_code, true, objInfo.c_code, objInfo.cg_code);
                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("cg_code");
                        ResultDt.Columns.Add("c_code");
                        ResultDt.Columns.Add("cs_code");
                        ResultDt.Columns.Add("cs_name");
                        ResultDt.Columns.Add("cs_address");
                        ResultDt.Columns.Add("cs_telephone");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            ResultDt.Rows.Add("OK", "", Dt.Rows[i]["cg_code"].ToString(), Dt.Rows[i]["c_code"].ToString(), Dt.Rows[i]["cs_code"].ToString(), Dt.Rows[i]["cs_name"].ToString()
                                    , Dt.Rows[i]["cs_address"].ToString(), Dt.Rows[i]["cs_telephone"].ToString());
                        }
                    }
                    else
                    {
                        ResultDt.Rows.Add("NoData", "");
                    }
                    break;
                case "ShowEdit":
                    Dt = objCP.GetCompanyShopData(objInfo.cs_code, true, objInfo.c_code, objInfo.cg_code);
                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("cg_code");
                        ResultDt.Columns.Add("c_code");
                        ResultDt.Columns.Add("cs_code");
                        ResultDt.Columns.Add("cs_name");
                        ResultDt.Columns.Add("cs_address");
                        ResultDt.Columns.Add("cs_telephone");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            ResultDt.Rows.Add("OK", "", Dt.Rows[i]["cg_code"].ToString(), Dt.Rows[i]["c_code"].ToString(), Dt.Rows[i]["cs_code"].ToString(), Dt.Rows[i]["cs_name"].ToString()
                                    , Dt.Rows[i]["cs_address"].ToString(), Dt.Rows[i]["cs_telephone"].ToString());
                        }
                    }
                    else
                    {
                        ResultDt.Rows.Add("NoData", "");
                    }
                    break;
                case "GetAll":
                    Dt = objCP.GetCompanyShopData("", true, objInfo.c_code, objInfo.cg_code);
                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("cg_code");
                        ResultDt.Columns.Add("c_code");
                        ResultDt.Columns.Add("cs_code");
                        ResultDt.Columns.Add("cs_name");
                        ResultDt.Columns.Add("cs_address");
                        ResultDt.Columns.Add("cs_telephone");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            ResultDt.Rows.Add("OK", "", Dt.Rows[i]["cg_code"].ToString(), Dt.Rows[i]["c_code"].ToString(), Dt.Rows[i]["cs_code"].ToString(), Dt.Rows[i]["cs_name"].ToString()
                                    , Dt.Rows[i]["cs_address"].ToString(), Dt.Rows[i]["cs_telephone"].ToString());
                        }
                    }
                    else
                    {
                        ResultDt.Rows.Add("NoData", "");
                    }
                    break;
                case "":

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
            public string c_code { set; get; }
            public string CRUD { set; get; }
            public string cs_name { set; get; }
            public string cs_code { set; get; }
            public string cs_address { set; get; }
            public string cs_telephone { set; get; }
        }
    }
}