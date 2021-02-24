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
    /// DayIncome_Load 的摘要描述
    /// </summary>
    public class DayIncome_Load : IHttpHandler
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
            if (HttpContext.Current.Session["cs_code"] != null)
            {
                string cs_code = HttpContext.Current.Session["cs_code"].ToString();
                DataTable Dt_Result = new DataTable();
                switch (Action)
                {
                    case "DayCome":
                        DataTable Dt_DayCome = objCP.GetCompanyShopTodayCome(cs_code, DateTime.Now.ToString("yyyy/MM/dd"));

                        Dt_Result.Columns.Add("tot_output_sum");
                        Dt_Result.Columns.Add("tot_daily_expend");
                        Dt_Result.Columns.Add("tot_patch_change");
                        Dt_Result.Columns.Add("tot_sum");

                        if (Dt_DayCome.Rows.Count > 0)
                        {
                            Dt_Result.Rows.Add(Dt_DayCome.Rows[0]["csd_ic_sum"].ToString()
                                , Dt_DayCome.Rows[0]["csd_oc_total"].ToString()
                                , Dt_DayCome.Rows[0]["csd_patch_change"].ToString()
                                , Dt_DayCome.Rows[0]["csd_ic_sum"].ToString()
                            );
                        }

                        break;
                    case "OutCome":
                        DataTable Dt_OutCome = objCP.GetCompanyShopTodayCome(cs_code, DateTime.Now.ToString("yyyy/MM/dd"));

                        Dt_Result.Columns.Add("Expend_seq");
                        Dt_Result.Columns.Add("Expend_cateqory");
                        Dt_Result.Columns.Add("Expend_cat_items");
                        Dt_Result.Columns.Add("Expend_amt");

                        for(int i =0;i<Dt_OutCome.Rows.Count;i++)
                        {
                            Dt_Result.Rows.Add(Dt_OutCome.Rows[0]["csd_ic_sum"].ToString()
                                , Dt_OutCome.Rows[0]["it_code"].ToString()
                                , Dt_OutCome.Rows[0]["i_code"].ToString()
                                , Dt_OutCome.Rows[0]["oc_total"].ToString()
                            );
                        }

                        break;
                }
                
                /*
                tot_output_sum: "20000",
                tot_daily_expend: "3000",
                tot_patch_change: "13500",
                tot_sum: "9500",
                input_1000: "10000",
                input_100: "5000",
                input_50: "3000",
                input_10: "1000",
                input_5: "1000", 
                Expend_Items: 
                [{ 
                Expend_seq: "1", 
                Expend_cateqory: "高麗菜",
                Expend_cat_items: "菜阿姨", 
                Expend_amt: "2000" }, { Expend_seq: "1", Expend_cateqory: "高麗菜", Expend_cat_items: "菜阿姨", Expend_amt: "1000" }]
                */

            }
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
        }
    }
}