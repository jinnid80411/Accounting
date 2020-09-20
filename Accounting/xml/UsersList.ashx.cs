using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using Accounting.App_Code;


namespace Accounting.xml
{
    /// <summary>
    /// UsersList 的摘要描述
    /// </summary>
    public class UsersList : IHttpHandler
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
            Info.u_code = u_code;
            Info.CRUD = "";
            Info.cg_name = "";*/
            DataTable ResultDt = new DataTable();
            ResultDt.Columns.Add("result");
            ResultDt.Columns.Add("Msg");
            DataTable Dt = new DataTable();
            switch (Action)
            {
                case "SendEdit":
                    if (objCP.UsersListCRUD(objInfo.CRUD, objInfo.u_code, objInfo.u_name,objInfo.u_id,objInfo.u_password))
                    {

                        if (objInfo.CRUD == "U")
                        {
                            Dt = objCP.GetUsersListData(objInfo.u_code, true);
                            ResultDt.Columns.Add("u_code");
                            ResultDt.Columns.Add("u_name");
                            ResultDt.Columns.Add("u_id");
                            ResultDt.Columns.Add("u_password");
                            ResultDt.Columns.Add("u_level");
                            ResultDt.Columns.Add("createdate");
                            for (int i = 0; i < Dt.Rows.Count; i++)
                            {
                                string createdate = objTL.DateChange(Dt.Rows[i]["createdate"].ToString(), "yyyy/MM/dd");
                                ResultDt.Rows.Add("OK", "", Dt.Rows[i]["u_code"].ToString(), Dt.Rows[i]["u_name"].ToString(), Dt.Rows[i]["u_id"].ToString(), Dt.Rows[i]["u_password"].ToString(), Dt.Rows[i]["u_level"].ToString(), createdate);
                            }
                        }
                        else
                        {
                            Dt = objCP.GetUsersListData("", true);
                            if (objInfo.CRUD == "D" && Dt.Rows.Count == 0)
                            {
                                ResultDt.Rows.Add("NoData", "");

                            }
                            else
                            {
                                ResultDt.Columns.Add("u_code");
                                ResultDt.Columns.Add("u_name");
                                ResultDt.Columns.Add("u_id");
                                ResultDt.Columns.Add("u_password");
                                ResultDt.Columns.Add("u_level");
                                ResultDt.Columns.Add("createdate");
                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    string createdate = objTL.DateChange(Dt.Rows[i]["createdate"].ToString(), "yyyy/MM/dd");
                                    ResultDt.Rows.Add("OK", "", Dt.Rows[i]["u_code"].ToString(), Dt.Rows[i]["u_name"].ToString(), Dt.Rows[i]["u_id"].ToString(), Dt.Rows[i]["u_password"].ToString(), Dt.Rows[i]["u_level"].ToString(), createdate);
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
                    Dt = objCP.GetUsersListData(objInfo.u_code, true);
                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("u_code");
                        ResultDt.Columns.Add("u_name");
                        ResultDt.Columns.Add("u_id");
                        ResultDt.Columns.Add("u_password");
                        ResultDt.Columns.Add("u_level");
                        ResultDt.Columns.Add("createdate");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            string createdate = objTL.DateChange(Dt.Rows[i]["createdate"].ToString(), "yyyy/MM/dd");
                            ResultDt.Rows.Add("OK", "", Dt.Rows[i]["u_code"].ToString(), Dt.Rows[i]["u_name"].ToString(), Dt.Rows[i]["u_id"].ToString(), Dt.Rows[i]["u_password"].ToString(), Dt.Rows[i]["u_level"].ToString(), createdate);
                        }
                    }
                    else
                    {
                        ResultDt.Rows.Add("NoData", "");
                    }
                    break;
                case "ShowEdit":
                    Dt = objCP.GetUsersListData(objInfo.u_code, true);
                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("u_code");
                        ResultDt.Columns.Add("u_name");
                        ResultDt.Columns.Add("u_id");
                        ResultDt.Columns.Add("u_password");
                        ResultDt.Columns.Add("u_level");
                        ResultDt.Columns.Add("createdate");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            string createdate = objTL.DateChange(Dt.Rows[i]["createdate"].ToString(), "yyyy/MM/dd");
                            ResultDt.Rows.Add("OK", "", Dt.Rows[i]["u_code"].ToString(), Dt.Rows[i]["u_name"].ToString(), Dt.Rows[i]["u_id"].ToString(), Dt.Rows[i]["u_password"].ToString(), createdate);
                        }
                    }
                    else
                    {
                        ResultDt.Rows.Add("NoData", "");
                    }
                    break;
                case "GetAll":
                    Dt = objCP.GetUsersListData("", true);
                    if (Dt.Rows.Count > 0)
                    {
                        ResultDt.Columns.Add("u_code");
                        ResultDt.Columns.Add("u_name");
                        ResultDt.Columns.Add("u_id");
                        ResultDt.Columns.Add("u_password");
                        ResultDt.Columns.Add("u_level");
                        ResultDt.Columns.Add("createdate");
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            string createdate = objTL.DateChange(Dt.Rows[i]["createdate"].ToString(), "yyyy/MM/dd");
                            ResultDt.Rows.Add("OK", "", Dt.Rows[i]["u_code"].ToString(), Dt.Rows[i]["u_name"].ToString(), Dt.Rows[i]["u_id"].ToString(), Dt.Rows[i]["u_password"].ToString(), Dt.Rows[i]["u_level"].ToString(), createdate);
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
            public string u_code { set; get; }
            public string CRUD { set; get; }
            public string u_name { set; get; }
            public string u_id { set; get; }
            public string u_password { set; get; }
        }
    }
}