﻿using System;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.AbsolutePath.ToString().Trim();
            string pagename = "";
            
            DataTable Dt_Menu = objMU.MenuControl();
            lit_Menu.Text = "";

            for (int i = 0; i < Dt_Menu.Rows.Count; i++)
            {
                bool IsMaster = Convert.ToBoolean(Dt_Menu.Rows[i]["IsMaster"]);
                string Active = "";
                if (Dt_Menu.Rows[i]["PageUrl"].ToString().Trim() == url)
                {
                    Active = " active";
                    lit_PageName.Text = Dt_Menu.Rows[i]["PageName"].ToString().Trim();
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