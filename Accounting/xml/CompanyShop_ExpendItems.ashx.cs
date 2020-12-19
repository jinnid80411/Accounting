using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using Accounting.App_Code;

namespace Accounting.xml
{
    /// <summary>
    /// CompanyShop_ExpendItems 的摘要描述
    /// </summary>
    public class CompanyShop_ExpendItems : IHttpHandler
    {
        FileTrace ft = new FileTrace();
        ClsItems objIT = new ClsItems();
        int i = 0;
        public void ProcessRequest(HttpContext context)
        {
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
            DataTable Dt = new DataTable();
            string[] ColumnsControl = { "it_code", "it_name"};

            switch (Action)
            {
                case "GetAll":

                    Dt = objIT.GetItemsType_CompanyShopData(objInfo.cs_code, "", "");

                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("No");
                        for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                        {
                            ResultDt.Columns.Add(ColumnsControl[i2]);
                        }
                        ResultDt.Columns.Add("Items");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            string StrGUID = Guid.NewGuid().ToString();

                            DataRow row = ResultDt.NewRow();
                            row["result"] = "OK";
                            row["Msg"] = "";
                            row["No"] = (i + 1).ToString();
                            for (int i2 = 0; i2 < ColumnsControl.Length; i2++)
                            {
                                row[ColumnsControl[i2].ToString().Trim()] = Dt.Rows[i][ColumnsControl[i2].ToString().Trim()].ToString();
                            }
                            row["Items"] = StrGUID;
                            ResultDt.Rows.Add(row);                            
                        }

                    }
                    else
                    {
                        //ResultDt.Rows.Add("NoData", "");
                    }
                    break;
            }

            string ajson = JsonConvert.SerializeObject(ResultDt, Formatting.Indented);

            switch (Action)
            {
                case "GetAll":
                    for (int i = 0; i < ResultDt.Rows.Count; i++)
                    {
                        DataTable Dt_Items = objIT.Get_vw_Items_CompanyShopData(objInfo.cs_code, ResultDt.Rows[i]["it_code"].ToString(), "","");

                        string ajson_items = JsonConvert.SerializeObject(Dt_Items, Formatting.Indented);
                        if(Dt_Items.Rows.Count>0)
                            ajson = ajson.Replace(ResultDt.Rows[i]["Items"].ToString(), ajson_items);
                        else
                            ajson = ajson.Replace(ResultDt.Rows[i]["Items"].ToString(), "");
                    }


                    break;
            }

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
            public string it_name { set; get; }
            public string createuser { set; get; }
            public string it_code { set; get; }
        }
    }
}