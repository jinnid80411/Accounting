using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accounting.App_Code;

namespace Accounting
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        ClsMenus objMU = new ClsMenus();
        ClsCompany objCP = new ClsCompany();
        public string TopComanyShopMenu = @"";
        string TopCompanyShopFormat = @"<span style=""font-size:18px;color:white;font-weight:bold;""><a  href=""CompanyShopSelect.aspx?cs_code={1}"">{0}</a></span>";

        string MasterFormat = @"<li class=""nav-item"">
                                    <a class=""nav-link{2}"" href=""{0}""  style=""font-weight: bold;font-size: 16px;"">
                                        {1}
                                    </a>
                                </li>";
        string DetailFormat = @"<li class=""nav-item"">
                                        <a class=""nav-link{2}"" href=""{0}"">
                                            <span data-feather=""file-text""></span>
                                            {1}
                                        </a>
                                    </li>";
        string UserNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.AbsolutePath.ToString().Trim();
            string pagename = "";

            #region==Session身分判定==

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

            #endregion

            #region==店家切換==

            DataTable Dt_CompanyShop = objCP.GetUsersCompanyShopData(UserNo, "", false);
            for (int i = 0; i < Dt_CompanyShop.Rows.Count; i++)
            {
                TopComanyShopMenu += string.Format(TopCompanyShopFormat,Dt_CompanyShop.Rows[i]["cs_name"].ToString(),Dt_CompanyShop.Rows[i]["code"].ToString());
            }


            #endregion

            DataTable Dt_Menu = objMU.MenuControl(UserNo);
            lit_Menu.Text = "";

            for (int i = 0; i < Dt_Menu.Rows.Count; i++)
            {
                bool IsMaster = Convert.ToBoolean(Dt_Menu.Rows[i]["IsMaster"]);
                string Active = "";
                if (Dt_Menu.Rows[i]["PageUrl"].ToString().Trim() == url)
                {
                    Active = " active";
                    //lit_PageName.Text = Dt_Menu.Rows[i]["PageName"].ToString().Trim();
                }
                pagename = Dt_Menu.Rows[i]["PageName"].ToString();
                if (IsMaster)
                {
                    if (i != 0)
                    {

                        lit_Menu.Text += @"</ul><div style=""
						    padding-left: 10px;
						    padding-right: 10px;
					    ""><hr></div>";

                    }
                    lit_Menu.Text += @" <ul class=""nav flex-column"">";
                    lit_Menu.Text += string.Format(MasterFormat, Dt_Menu.Rows[i]["PageUrl"].ToString().Trim(), pagename,Active);
                }
                else
                {
                    lit_Menu.Text += string.Format(DetailFormat, Dt_Menu.Rows[i]["PageUrl"].ToString().Trim(), pagename, Active);

                }
                if (i == (Dt_Menu.Rows.Count - 1))
                {
                    lit_Menu.Text += @"</ul>";
                }
            }

            

        }
    }
}