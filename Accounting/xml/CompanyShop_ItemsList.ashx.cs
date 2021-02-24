using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using Accounting.App_Code;

namespace Accounting.xml
{
    /// <summary>
    /// CompanyShop_ItemsList 的摘要描述
    /// </summary>
    public class CompanyShop_ItemsList : IHttpHandler
    {

        FileTrace ft = new FileTrace();
        ClsItems objIT = new ClsItems();
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
            string[] ColumnsControl = { "it_code", "it_name" };
            switch (Action)
            {
                case "SendEdit":
                    if (objIT.Items_CompanyShopCRUD(objInfo.CRUD, objInfo.cs_code, objInfo.i_name, objInfo.it_code,objInfo.vd_code, objInfo.createuser,objInfo.i_code))
                    {

                        if (objInfo.CRUD == "U")
                        {
                            Dt = objIT.GetItems_CompanyShopData(objInfo.cs_code, "", objInfo.i_name,objInfo.i_code);
                            for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                            {
                                ResultDt.Columns.Add(ColumnsControl[i2]);
                            }
                            for (int i = 0; i < Dt.Rows.Count; i++)
                            {
                                DataRow row = ResultDt.NewRow();
                                row["result"] = "OK";
                                row["Msg"] = "";
                                for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                                {
                                    row[ColumnsControl[i2].ToString().Trim()] = Dt.Rows[i][ColumnsControl[i2].ToString().Trim()].ToString();
                                }
                                ResultDt.Rows.Add(row);
                            }
                        }
                        else
                        {
                            Dt = objIT.GetItems_CompanyShopData(objInfo.cs_code, "", objInfo.i_name,objInfo.i_code);
                            if (objInfo.CRUD == "D" && Dt.Rows.Count == 0)
                            {
                                ResultDt.Rows.Add("NoData", "");

                            }
                            else
                            {
                                for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                                {
                                    ResultDt.Columns.Add(ColumnsControl[i2]);
                                }
                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    DataRow row = ResultDt.NewRow();
                                    row["result"] = "OK";
                                    row["Msg"] = "";
                                    for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                                    {
                                        row[ColumnsControl[i2].ToString().Trim()] = Dt.Rows[i][ColumnsControl[i2].ToString().Trim()].ToString();
                                    }
                                    ResultDt.Rows.Add(row);
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
                    Dt = objIT.GetItems_CompanyShopData(objInfo.cs_code, objInfo.it_code, "",objInfo.i_code);
                    if (Dt.Rows.Count > 0)
                    {
                        for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                        {
                            ResultDt.Columns.Add(ColumnsControl[i2]);
                        }
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            DataRow row = ResultDt.NewRow();
                            row["result"] = "OK";
                            row["Msg"] = "";
                            for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                            {
                                row[ColumnsControl[i2].ToString().Trim()] = Dt.Rows[i][ColumnsControl[i2].ToString().Trim()].ToString();
                            }
                            ResultDt.Rows.Add(row);
                        }
                    }
                    else
                    {
                        ResultDt.Rows.Add("NoData", "");
                    }
                    break;
                case "ShowEdit":
                    //Dt = objIT.GetItems_CompanyShopData(objInfo.cs_code, objInfo.it_code, "");
                    if (Dt.Rows.Count > 0)
                    {
                        for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                        {
                            ResultDt.Columns.Add(ColumnsControl[i2]);
                        }
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            DataRow row = ResultDt.NewRow();
                            row["result"] = "OK";
                            row["Msg"] = "";
                            for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                            {
                                row[ColumnsControl[i2].ToString().Trim()] = Dt.Rows[i][ColumnsControl[i2].ToString().Trim()].ToString();
                            }
                            ResultDt.Rows.Add(row);
                        }
                    }
                    else
                    {
                        ResultDt.Rows.Add("NoData", "");
                    }
                    break;
                case "GetAll":
                    //Dt = objIT.GetItems_CompanyShopData(objInfo.cs_code, "", "");
                    if (Dt.Rows.Count > 0)
                    {
                        for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                        {
                            ResultDt.Columns.Add(ColumnsControl[i2]);
                        }
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            DataRow row = ResultDt.NewRow();
                            row["result"] = "OK";
                            row["Msg"] = "";
                            for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                            {
                                row[ColumnsControl[i2].ToString().Trim()] = Dt.Rows[i][ColumnsControl[i2].ToString().Trim()].ToString();
                            }
                            ResultDt.Rows.Add(row);
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
            public string cs_code { set; get; }
            public string CRUD { set; get; }
            public string i_name { set; get; }
            public string i_code { set; get; }
            public string vd_code { set; get; }
            public string createuser { set; get; }
            public string it_code { set; get; }
        }
    }
}