using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Accounting.App_Code;


namespace Accounting.App_Code
{
    public class ClsItems
    {
        FileTrace ft = new FileTrace();
        #region==CompanyShop==
        #region==ItemsType==
        public DataTable GetItemsType_CompanyShopData(string cs_code,string it_code,string it_name)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select distinct c.it_code,i.it_name,c.cs_code from [Items_CompanyShop] c with (nolock)   
                        left outer join ItemsType i on  i.it_code = c.it_code
                    Where 1=1 and c.cs_code=@cs_code";
            if (it_code != "")
                strSQL += " and c.it_code = @it_code";
            if (it_name != "")
                strSQL += " and i.it_name = @it_name";
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cs_code", cs_code);
                if (it_code != "")
                    cmd.Parameters.AddWithValue("@it_code", it_code);
                if (it_name != "")
                    cmd.Parameters.AddWithValue("@it_name", it_name);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }

        public bool ItemsType_CompanyShopCRUD(string CRUD, string cs_code, string it_name,string it_code,string createuser)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString());
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand comm = new SqlCommand("", conn);
            comm.Transaction = tran;
            try
            {

                string strSQL = @"";
                switch (CRUD)
                {
                    case "C":
                        strSQL += @" EXEC [dbo].[Run_ItemsType_CompanyShop] @cs_code,@it_name,@createuser ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cs_code", cs_code);
                        comm.Parameters.AddWithValue("@it_name", it_name.Trim());
                        comm.Parameters.AddWithValue("@createuser", createuser);
                        comm.ExecuteNonQuery();
                        break;
                    case "U":
                        strSQL += @" 
                                    EXEC [dbo].[Run_ItemsType_CompanyShop_Delete] @cs_code,@it_code
                                    EXEC [dbo].[Run_ItemsType_CompanyShop] @cs_code,@it_name,@createuser ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cs_code", cs_code);
                        comm.Parameters.AddWithValue("@it_name", it_name.Trim());
                        comm.Parameters.AddWithValue("@it_code", it_code.Trim());
                        comm.Parameters.AddWithValue("@createuser", createuser);
                        comm.ExecuteNonQuery();
                        break;
                    case "D":
                        strSQL += @" EXEC [dbo].[Run_ItemsType_CompanyShop_Delete] @cs_code,@it_code ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cs_code", cs_code);
                        comm.Parameters.AddWithValue("@it_code", it_code.Trim());
                        comm.ExecuteNonQuery();
                        break;
                }
                
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ft.ftrace(ex.ToString());
                return false;
            }
            finally
            {
                conn.Close();
                comm.Dispose();
            }
        }
        #endregion
        #region==Items==


        public DataTable Get_vw_Items_CompanyShopData(string cs_code, string it_code, string it_name, string i_code)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [vw_Items_CompanyShop] c with (nolock)   
                    Where 1=1 and c.cs_code=@cs_code";
            if (it_code != "")
                strSQL += " and c.it_code = @it_code";
            if (i_code != "")
                strSQL += " and c.i_code = @i_code";
            if (it_name != "")
                strSQL += " and c.it_name = @it_name";
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cs_code", cs_code);
                if (it_code != "")
                    cmd.Parameters.AddWithValue("@it_code", it_code);
                if (i_code != "")
                    cmd.Parameters.AddWithValue("@i_code", i_code);
                if (it_name != "")
                    cmd.Parameters.AddWithValue("@it_name", it_name);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }

        public DataTable GetItems_CompanyShopData(string cs_code, string it_code, string i_name,string i_code)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [Items_CompanyShop] c with (nolock)   
                        left outer join Items i on  i.i_code = c.i_code
                    Where 1=1 and c.cs_code=@cs_code";
            if (it_code != "")
                strSQL += " and c.it_code = @it_code";
            if (i_code != "")
                strSQL += " and c.i_code = @i_code";
            if (i_name != "")
                strSQL += " and i.i_name = @i_name";
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cs_code", cs_code);
                if (it_code != "")
                    cmd.Parameters.AddWithValue("@it_code", it_code);
                if (i_code != "")
                    cmd.Parameters.AddWithValue("@i_code", i_code);
                if (i_name != "")
                    cmd.Parameters.AddWithValue("@it_name", i_name);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }

        public bool Items_CompanyShopCRUD(string CRUD, string cs_code, string i_name, string it_code,string vd_code, string createuser,string i_code)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString());
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand comm = new SqlCommand("", conn);
            comm.Transaction = tran;
            try
            {

                string strSQL = @"";
                switch (CRUD)
                {
                    case "C":
                        strSQL += @" EXEC [dbo].[Run_Items_CompanyShop] @cs_code,@it_code,@i_name,@vd_code,@createuser ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cs_code", cs_code);
                        comm.Parameters.AddWithValue("@it_code", cs_code);
                        comm.Parameters.AddWithValue("@i_name", i_name.Trim());
                        comm.Parameters.AddWithValue("@vd_code", vd_code);
                        comm.Parameters.AddWithValue("@createuser", createuser);
                        comm.ExecuteNonQuery();
                        break;
                    case "U":
                        strSQL += @" 
                                    EXEC [dbo].[Run_Items_CompanyShop_Delete] @cs_code,@i_code,@createuser
                                    EXEC [dbo].[Run_Items_CompanyShop] @cs_code,@it_code,@i_name,@vd_code,@createuser ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cs_code", cs_code);
                        comm.Parameters.AddWithValue("@i_name", i_name.Trim());
                        comm.Parameters.AddWithValue("@it_code", it_code.Trim());
                        comm.Parameters.AddWithValue("@vd_code", vd_code.Trim());
                        comm.Parameters.AddWithValue("@createuser", createuser);
                        comm.ExecuteNonQuery();
                        break;
                    case "D":
                        strSQL += @" EXEC [dbo].[Run_Items_CompanyShop_Delete] @cs_code,@i_code,@createuser ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cs_code", cs_code);
                        comm.Parameters.AddWithValue("@i_code", i_code.Trim());
                        comm.Parameters.AddWithValue("@createuser", createuser.Trim());
                        comm.ExecuteNonQuery();
                        break;
                }

                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ft.ftrace(ex.ToString());
                return false;
            }
            finally
            {
                conn.Close();
                comm.Dispose();
            }
        }
        #endregion


        #endregion
        #region==Unit==
        public DataTable GetUnit_isUse(string ut_type,string ut_code)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [vw_Unit_isUse] c with (nolock)   
                    Where 1=1 ";
            if (ut_type != "")
                strSQL += " and c.ut_type = @ut_type";
            if (ut_code != "")
                strSQL += " and c.ut_code = @ut_code";
            strSQL += " order by order by c.[ut_type] asc";
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                if (ut_type != "")
                    cmd.Parameters.AddWithValue("@ut_type", ut_type);
                if (ut_code != "")
                    cmd.Parameters.AddWithValue("@ut_code", ut_code);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }
        public string CreateUnitSelect(bool IsFormat,string classname,string style,string id,string value,string otherStatus)
        {
            string control = "";

            DataTable Dt = GetUnit_isUse("","");

            if (classname != "")
                classname = @"class=""" + classname + @"""";

            if (style != "")
                style = @"style=""" + style + @"""";

            if (IsFormat)
            {
                id = @"id=""{0}""";
            }
            else
            {
                if (id != "")
                    id = @"id=""" + id + @"""";
            }
            control = @"<select "+ classname + " "+ style+" "+ id +" "+ otherStatus + ">";
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                string selected = "";
                if (Dt.Rows[i]["ut_code"].ToString().Trim() == value.Trim())
                    selected = "selected";
                control += @"<option "+ selected + @" value="""+ Dt.Rows[i]["ut_code"].ToString() + @""">" + Dt.Rows[i]["ut_name_all"].ToString() + @" </option>";
            }
            control += "</select>";
            return control;

        }
        #endregion
    }
}
