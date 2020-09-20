using System;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Web.SessionState;
using Accounting.App_Code;

namespace Accounting.xml
{
    /// <summary>
    /// SignIn 的摘要描述
    /// </summary>
    public class SignIn : IHttpHandler, IRequiresSessionState
    {
        ClsCompany objCP = new ClsCompany();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            Info objInfo = JsonConvert.DeserializeObject<Info>(strJson); // Deserialize<Info>(strJson);
            string id = "";
            string password = "";
            string checkcode = "";
            string remember = "";
            if (objInfo != null)
            {
                id = HttpUtility.HtmlEncode(objInfo.id);
                password = HttpUtility.HtmlEncode(objInfo.password);
                checkcode = HttpUtility.HtmlEncode(objInfo.checkcode);
                remember = HttpUtility.HtmlEncode(objInfo.remember);
            }

            if (remember == "Y")
            {

            }
            else
            {


            }

            DataTable ResultDt = new DataTable();
            ResultDt.Columns.Add("result");
            ResultDt.Columns.Add("Msg");
            ResultDt.Columns.Add("url");

            if (HttpContext.Current.Session["AuthCode"] == null || HttpContext.Current.Session["AuthCode"].ToString() == "")
            {
                ResultDt.Rows.Add("Error", "驗證碼失效", "");
            }
            else
            {
                if (HttpContext.Current.Session["AuthCode"].ToString().Trim() == checkcode.Trim())
                {
                    DataTable Dt = objCP.GetLoginData(id);
                    if (Dt.Rows.Count > 0)
                    {
                        if (Dt.Rows[0]["u_password"].ToString().Trim() == password.Trim())
                        {
                            HttpContext.Current.Session["UserNo"] = Dt.Rows[0]["u_code"].ToString().Trim();

                            string retUrl = "Default.aspx";
                            #region==導回之前網址==
                            if (HttpContext.Current.Session["retUrl"] != null)
                            {
                                retUrl = HttpContext.Current.Session["retUrl"].ToString();
                            }

                            HttpContext.Current.Session["retUrl"] = "";
                            #endregion

                            ResultDt.Rows.Add("OK", "登入成功", retUrl);
                        }
                        else
                        {
                            ResultDt.Rows.Add("Error", "密碼錯誤", "");
                        }
                        
                    }
                    else
                    {
                        ResultDt.Rows.Add("Error", "查無此帳號", "");
                    }
                }
                else
                {
                    ResultDt.Rows.Add("Error", "請輸入正確驗證碼", "");
                }
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
            public string id { set; get; }
            public string password { set; get; }
            public string checkcode { set; get; }
            public string remember { set; get; }
        }
    }
}