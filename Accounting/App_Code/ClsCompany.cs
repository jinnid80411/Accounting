using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Accounting.App_Code;

namespace Accounting.App_Code
{
    
    public class ClsCompany
    {
        FileTrace ft = new FileTrace();        
        #region==CompanyGroup==
        public DataTable GetCompanyGroupData(string cg_code,bool IsUse)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [CompanyGroup] with (nolock)                    
                    Where 1=1";
            if (cg_code != "")
                strSQL += " and cg_code=@cg_code ";
            if(IsUse)
                strSQL += " and isUse = 'Y'";
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                if(cg_code!="")
                    cmd.Parameters.AddWithValue("@cg_code", cg_code);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }

        public bool CompanyGroupCRUD(string CRUD, string cg_code,string cg_name)
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
                        strSQL += @" insert into CompanyGroup (cg_name) values (@cg_name) ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cg_name", cg_name);                     
                        comm.ExecuteNonQuery();
                        break;
                    case "U":
                        strSQL += @" Update CompanyGroup Set cg_name=@cg_name Where cg_code=@cg_code ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cg_name", cg_name);
                        comm.Parameters.AddWithValue("@cg_code", cg_code);
                        comm.ExecuteNonQuery();
                        break;
                    case "D":
                        strSQL += @" Update CompanyGroup Set isUse='N' Where cg_code=@cg_code ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cg_code", cg_code);
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
    }


}