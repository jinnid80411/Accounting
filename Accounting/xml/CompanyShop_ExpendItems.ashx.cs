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
            ResultDt.Columns.Add("ItemsType");
            ResultDt.Columns.Add("Items");
            string[] ColumnsControl_ItemsType = { "it_code", "it_name"};
            string[] ColumnsControl_Items = { "it_code","i_code","i_name", "vendor_name" };
            DataTable ResultDt_ItemsType = new DataTable();
            for (int i2 = 0; i2 < ColumnsControl_ItemsType.Length; i2++)
            {
                ResultDt_ItemsType.Columns.Add(ColumnsControl_ItemsType[i2]);
            }
            DataTable ResultDt_Items = new DataTable();
            for (int i2 = 0; i2 < ColumnsControl_Items.Length; i2++)
            {
                ResultDt_Items.Columns.Add(ColumnsControl_Items[i2]);
            }
            DataTable Dt_ItemsType = new DataTable();
            DataTable Dt_Items = new DataTable();


            string ItemsType_GUID = Guid.NewGuid().ToString();
            string Items_GUID = Guid.NewGuid().ToString();

            switch (Action)
            {
                case "GetAll":

                    DataRow row = ResultDt.NewRow();
                    row["result"] = "OK";
                    row["Msg"] = "";
                    row["ItemsType"] = ItemsType_GUID;
                    row["Items"] = Items_GUID;
                    ResultDt.Rows.Add(row);
                    #region==ItemsType==
                    string it_code_all = "";
                    Dt_ItemsType = objIT.GetItemsType_CompanyShopData(objInfo.cs_code, "", "");    
                    for (int i = 0; i < Dt_ItemsType.Rows.Count; i++)
                    {
                        DataRow row_ItemsType = ResultDt_ItemsType.NewRow();
                        for (int i2 = 0; i2 < ColumnsControl_ItemsType.Length; i2++)
                        {
                            string columns_name = ColumnsControl_ItemsType[i2];
                            row_ItemsType[columns_name] = Dt_ItemsType.Rows[i][columns_name].ToString();
                        }
                        ResultDt_ItemsType.Rows.Add(row_ItemsType);

                        if (it_code_all != "")
                            it_code_all += ",";
                        it_code_all += Dt_ItemsType.Rows[i]["it_code"].ToString();
                    }
                    #endregion
                    #region==Items==
                    Dt_Items = objIT.Get_vw_Items_CompanyShopData(objInfo.cs_code, it_code_all,"", "");
                    for (int i = 0; i < Dt_Items.Rows.Count; i++)
                    {
                        DataRow row_Items = ResultDt_Items.NewRow();
                        for (int i2 = 0; i2 < ColumnsControl_Items.Length; i2++)
                        {
                            string columns_name = ColumnsControl_Items[i2];
                            row_Items[columns_name] = Dt_Items.Rows[i][columns_name].ToString();
                        }
                        ResultDt_Items.Rows.Add(row_Items);
                    }
                    #endregion
                    break;

            }

            string ajson = JsonConvert.SerializeObject(ResultDt, Formatting.Indented);
            switch (Action)
            {
                case "GetAll":

                    string ajson_itemstype = JsonConvert.SerializeObject(ResultDt_ItemsType, Formatting.Indented);
                    string ajson_items = JsonConvert.SerializeObject(ResultDt_Items, Formatting.Indented);                       
                    ajson = ajson.Replace("\""+ItemsType_GUID+ "\"", ajson_itemstype);                    
                    ajson = ajson.Replace("\""+Items_GUID+ "\"", ajson_items);
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