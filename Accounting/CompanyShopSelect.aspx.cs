using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accounting.App_Code;

namespace Accounting
{
    public partial class CompanyShopSelect : System.Web.UI.Page
    {
        ClsCompany objCP = new ClsCompany();
        string UserNo = "";
        string cs_code = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserNo"] == null || Session["UserNo"].ToString().Trim() == "")
            {
                string retUrl = "";
                retUrl = Request.ServerVariables["Script_Name"].ToString();
                string qstring = Request.QueryString.ToString();
                if (Request.QueryString.ToString() != "")
                    retUrl += "?" + Request.QueryString.ToString();
                // 保留登入前網址
                Session["retUrl"] = Server.UrlEncode(retUrl);
                Response.Write("<script>alert('您離開系統時間太久，請重新登入!!');</script>");
                Response.Redirect("~/SignIn.aspx");
                Response.End();
            }
            else
            {

                UserNo = HttpUtility.HtmlEncode(Session["UserNo"].ToString().Trim());
            }
            if (Request.QueryString["cs_code"] != null)
                cs_code = HttpUtility.HtmlEncode(Request.QueryString["cs_code"].ToString().Trim());
            if (cs_code != "")
            {
                DataTable Dt = objCP.GetUsersCompanyShopData(UserNo,cs_code,false);
                if (Dt.Rows.Count > 0)
                {
                    if (objCP.InsertCompanyUseLog("CompanyShop", UserNo, cs_code))
                    {
                        Session["cs_code"] = cs_code;
                        if (Session["retUrl_CompanyShop"] != null && Session["retUrl_CompanyShop"].ToString() != "")
                        {
                            string retUrl = HttpContext.Current.Session["retUrl_CompanyShop"].ToString();
                            Response.Redirect(retUrl);
                            Response.End();
                        }
                    }
                    else
                    {
                        Literal1.Text = "操作失敗，請聯絡管理員";
                    }
                }
                else
                {
                    Literal1.Text = "您無操作此權限，請聯絡管理員";
                }
            }
            else
            {
                Literal1.Text = "請選擇右上角的店家";
            }
        }
    }
}