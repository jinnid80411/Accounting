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
        #region==Company==
        public DataTable GetCompanyData(string c_code, bool IsUse,string cg_code)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [Company] with (nolock)                    
                    Where 1=1";
            if (c_code != "")
                strSQL += " and c_code=@c_code ";
            if (cg_code != "")
                strSQL += " and cg_code=@cg_code ";
            if (IsUse)
                strSQL += " and isUse = 'Y'";
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                if (c_code != "")
                    cmd.Parameters.AddWithValue("@c_code", c_code);
                if (cg_code != "")
                    cmd.Parameters.AddWithValue("@cg_code", cg_code);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }

        public bool CompanyCRUD(string CRUD, string c_code, string c_name,string c_id,string c_address,string c_telephone,string c_builddate,string cg_code)
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
                        strSQL += @" insert into Company (c_name,cg_code,c_id,c_address,c_telephone,c_builddate) values (@c_name,@cg_code,@c_id,@c_address,@c_telephone,@c_builddate) ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@c_name", c_name);
                        comm.Parameters.AddWithValue("@cg_code", cg_code);
                        comm.Parameters.AddWithValue("@c_id", c_id);
                        comm.Parameters.AddWithValue("@c_address", c_address);
                        comm.Parameters.AddWithValue("@c_telephone", c_telephone);
                        comm.Parameters.AddWithValue("@c_builddate", c_builddate);
                        comm.ExecuteNonQuery();
                        break;
                    case "U":
                        strSQL += @" Update Company Set c_name=@c_name,c_id=@c_id,c_address=@c_address,c_telephone=@c_telephone,c_builddate=@c_builddate Where c_code=@c_code ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@c_name", c_name);
                        comm.Parameters.AddWithValue("@c_code", c_code);
                        comm.Parameters.AddWithValue("@c_id", c_id);
                        comm.Parameters.AddWithValue("@c_address", c_address);
                        comm.Parameters.AddWithValue("@c_telephone", c_telephone);
                        comm.Parameters.AddWithValue("@c_builddate", c_builddate);
                        comm.ExecuteNonQuery();
                        break;
                    case "D":
                        strSQL += @" Update Company Set isUse='N' Where c_code=@c_code ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@c_code", c_code);
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