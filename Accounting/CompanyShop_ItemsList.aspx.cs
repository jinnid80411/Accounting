using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accounting.App_Code;

namespace Accounting
{
    public partial class CompanyShop_ItemsList : System.Web.UI.Page
    {
        ClsItems objCL = new ClsItems();
        public string cs_code = "";
        public string UserNo = "";
        public string dp_unit = "";
        public string it_code = "";
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
            it_code = HttpUtility.HtmlEncode(Session["it_code"].ToString().Trim());
            dp_unit = objCL.CreateUnitSelect(true,"form-control","","","","");
        }
        
    }
}