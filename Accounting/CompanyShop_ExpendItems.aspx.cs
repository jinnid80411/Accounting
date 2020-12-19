using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Accounting
{
    public partial class CompanyShop_ExpendItems : System.Web.UI.Page
    {
        public string cs_code = "";
        public string UserNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cs_code"] != null)
                cs_code = HttpUtility.HtmlEncode(Session["cs_code"].ToString().Trim());
            else
            {
                Response.Redirect("CompanyShopSelect.aspx");
                Response.End();
            }
            UserNo = HttpUtility.HtmlEncode(Session["UserNo"].ToString().Trim());
        }
    }
}