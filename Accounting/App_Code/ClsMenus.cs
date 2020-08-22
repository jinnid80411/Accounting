using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Accounting.App_Code
{
    public class ClsMenus
    {

        public DataTable MenuControl()
        {
            DataTable menuList = new DataTable();

            menuList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            menuList.Columns.Add("PageUrl", System.Type.GetType("System.String"));
            menuList.Columns.Add("PageName", System.Type.GetType("System.String"));
            menuList.Columns.Add("IsMaster", System.Type.GetType("System.Boolean"));
            menuList.Columns.Add("PageMasterID", System.Type.GetType("System.Int32"));

            menuList.Rows.Add(1, "", "總覽", true, 0);
            menuList.Rows.Add(2, "", "每日收入", false, 1);
            menuList.Rows.Add(3, "", "每日支出", false, 1);
            menuList.Rows.Add(4, "", "支出品項管理", false, 1);
            menuList.Rows.Add(5, "", "統計", false, 1);
            menuList.Rows.Add(6, "", "總薪資項目覽", true, 0);
            menuList.Rows.Add(7, "", "員工打卡", false, 6);
            menuList.Rows.Add(8, "", "員工出勤紀錄", false, 6);
            menuList.Rows.Add(9, "", "薪資計算設定", false, 6);


            return menuList;
        }


    }
}