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


        #endregion
    }
}
