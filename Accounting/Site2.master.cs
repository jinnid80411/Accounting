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
    public partial class Site2 : System.Web.UI.MasterPage
    {
        ClsCompany objCP = new ClsCompany();
        string UserNo = "";
        string cs_code = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            string retUrl = "";
            retUrl = Request.ServerVariables["Script_Name"].ToString();
            string qstring = Request.QueryString.ToString();
            if (Request.QueryString.ToString() != "")
                retUrl += "?" + Request.QueryString.ToString();
            // 保留登入前網址
            Session["retUrl_CompanyShop"] = Server.UrlEncode(retUrl);

            if (Session["cs_code"] == null || Session["cs_code"].ToString()=="")
            {
                DataTable Dt_CompanyShop = objCP.GetUsersCompanyShopData(UserNo,"",true);
                if (Dt_CompanyShop.Rows.Count > 0)
                    Session["cs_code"] = Dt_CompanyShop.Rows[0]["code"].ToString();
                else
                {
                    Response.Redirect("CompanyShopSelect.aspx");
                    Response.End();
                }
            }

            UserNo = HttpUtility.HtmlEncode(Session["UserNo"].ToString().Trim());
        }
    }
}