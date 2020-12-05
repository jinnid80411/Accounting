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

        public bool CompanyCRUD(string CRUD, string c_code, string c_name,string c_id,string c_builddate,string cg_code)
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
                        strSQL += @" insert into Company (c_name,cg_code,c_id,c_builddate) values (@c_name,@cg_code,@c_id,@c_builddate) ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@c_name", c_name);
                        comm.Parameters.AddWithValue("@cg_code", cg_code);
                        comm.Parameters.AddWithValue("@c_id", c_id);
                        comm.Parameters.AddWithValue("@c_builddate", c_builddate);
                        comm.ExecuteNonQuery();
                        break;
                    case "U":
                        strSQL += @" Update Company Set c_name=@c_name,c_id=@c_id,c_builddate=@c_builddate Where c_code=@c_code ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@c_name", c_name);
                        comm.Parameters.AddWithValue("@c_code", c_code);
                        comm.Parameters.AddWithValue("@c_id", c_id);
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
        #region==CompanyShop==
        public DataTable GetCompanyShopData(string cs_code, bool IsUse, string c_code, string cg_code)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [CompanyShop] with (nolock)                    
                    Where 1=1";
            if (cs_code != "")
                strSQL += " and cs_code=@cs_code ";
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
                if (cs_code != "")
                    cmd.Parameters.AddWithValue("@cs_code", cs_code);
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

        public bool CompanyShopCRUD(string CRUD,string cs_code, string cs_name, string cs_address,string cs_telephone, string cg_code, string c_code)
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
                        strSQL += @" insert into CompanyShop (cs_name,c_code,cg_code,cs_address,cs_telephone) values (@cs_name,@c_code,@cg_code,@cs_address,@cs_telephone) ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cs_name", cs_name);
                        comm.Parameters.AddWithValue("@c_code", c_code);
                        comm.Parameters.AddWithValue("@cg_code", cg_code);
                        comm.Parameters.AddWithValue("@cs_address", cs_address);
                        comm.Parameters.AddWithValue("@cs_telephone", cs_telephone);
                        comm.ExecuteNonQuery();
                        break;
                    case "U":
                        strSQL += @" Update CompanyShop Set cs_name=@cs_name,cs_address=@cs_address,cs_telephone=@cs_telephone Where cs_code=@cs_code ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cs_name", cs_name);
                        comm.Parameters.AddWithValue("@cs_code", cs_code);
                        comm.Parameters.AddWithValue("@cs_address", cs_address);
                        comm.Parameters.AddWithValue("@cs_telephone", cs_telephone);
                        comm.ExecuteNonQuery();
                        break;
                    case "D":
                        strSQL += @" Update CompanyShop Set isUse='N' Where cs_code=@cs_code ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@cs_code", cs_code);
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
        #region==CompanyUsers==
        public DataTable GetCompanyUsers(string type,string code,string u_code)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [CompanyUsers] c with (nolock)   
                        left join Users u on u.u_code = c.u_code
                        Where 1=1 and c.code=@code and c.type=@type ";
            if (u_code != "")
                strSQL += " and u_code = @u_code";
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@code", code);
                if (u_code != "")
                    cmd.Parameters.AddWithValue("@u_code", u_code);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }

        public bool UpdateCompanyUsers(string type,string code, string u_code)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString());
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand comm = new SqlCommand("", conn);
            comm.Transaction = tran;
            try
            {

                string strSQL = @" 
                                   Declare @u_code_table table
                                   (
                                       u_code varchar(50)      
                                   )
                                   Insert into @u_code_table(u_code)
                                   Select word From dbo.udf_Split(@u_code,',')

                                   Insert into CompanyUsers(type,code,u_code)
                                   Select type=@type,code=@code,u_code From @u_code_table
                                   Where u_code not in (Select u_code From CompanyUsers Where [type]=@type and code=@code) 
    
                                   Delete CompanyUsers Where type=@type and code=@code and u_code not in (Select u_code From @u_code_table)
                                   ";
                
                comm.CommandText = strSQL;
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@type", type);
                comm.Parameters.AddWithValue("@code", code);
                comm.Parameters.AddWithValue("@u_code", u_code);
                comm.ExecuteNonQuery();
                 
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
        #region==UsersList==

        public DataTable GetLoginData(string id)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [vw_Users_isUse] with (nolock)                    
                    Where 1=1 and u_id=@u_id ";
            
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@u_id", id);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }


        public DataTable GetUsersDataNotSelect(string u_code_array)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [vw_Users_isUse] with (nolock)                    
                    Where 1=1 ";
            if (u_code_array != "")
            {
                strSQL += @" and u_code not in (Select word From dbo.udf_Split(@u_code_array,',')) ";
            }

            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                if (u_code_array != "")
                    cmd.Parameters.AddWithValue("@u_code_array", u_code_array);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }

        public DataTable GetUsersListData(string u_code, bool IsUse)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [Users] with (nolock)                    
                    Where 1=1";
            if (u_code != "")
                strSQL += " and u_code=@u_code ";
            if (IsUse)
                strSQL += " and isUse = 'Y'";
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                if (u_code != "")
                    cmd.Parameters.AddWithValue("@u_code", u_code);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }

        public bool UsersListCRUD(string CRUD, string u_code, string u_name,string u_id,string u_password)
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
                        strSQL += @" insert into Users (u_name,u_id,u_password) values (@u_name,@u_id,@u_password) ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@u_name", u_name);
                        comm.Parameters.AddWithValue("@u_id", u_id);
                        comm.Parameters.AddWithValue("@u_password", u_password);
                        comm.ExecuteNonQuery();
                        break;
                    case "U":
                        strSQL += @" Update Users Set u_name=@u_name,u_id=@u_id,u_password=@u_password Where u_code=@u_code ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@u_name", u_name);
                        comm.Parameters.AddWithValue("@u_id", u_id);
                        comm.Parameters.AddWithValue("@u_password", u_password);
                        comm.Parameters.AddWithValue("@u_code", u_code);
                        comm.ExecuteNonQuery();
                        break;
                    case "D":
                        strSQL += @" Update Users Set isUse='N' Where u_code=@u_code ";
                        comm.CommandText = strSQL;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@u_code", u_code);
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

        public bool InsertCompanyUseLog(string type,string u_code, string cs_code)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString());
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand comm = new SqlCommand("", conn);
            comm.Transaction = tran;
            try
            {

                string strSQL = @"";
                    
                strSQL += @" insert into [CompanyUsersUseLog] ([type],[code],[u_code]) values (@type,@code,@u_code) ";
                comm.CommandText = strSQL;
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@type", type);
                comm.Parameters.AddWithValue("@code", cs_code);
                comm.Parameters.AddWithValue("@u_code", u_code);
                comm.ExecuteNonQuery();
                 
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

        public DataTable GetUsersCompanyShopData(string u_code,string cs_code,bool IsNow)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [vw_Users_CompanyShop] with (nolock) 
                    Where 1=1 and [u_code]=@u_code ";
            if(cs_code!="")
                strSQL += " and code=@code ";
            if (IsNow)
            {
                strSQL += " and isNow = 'Y' ";
            }


            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@u_code", u_code);
                if(cs_code!="")
                    cmd.Parameters.AddWithValue("@code", cs_code);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }
        #endregion
        #region==CompanyToday==

        public DataTable GetCompanyShopToday(string cs_code,string InputDate)
        {
            DataTable Dt = new DataTable();

            string strSQL = @"";

            //strSQL = @" select * from AdvUsers with (nolock) where Code=@pno ";
            strSQL = @" select * from [InCome] with (nolock)                    
                    Where 1=1 and cs_code=@cs_code and DateDiff(dd,createdate,@InputDate) = 0 ";

            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strSQL);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cs_code", cs_code);
                cmd.Parameters.AddWithValue("@InputDate", InputDate);

                cmd.Connection = conn;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                conn.Close();
            }

            return Dt;
        }

        public bool InsertTodayInCome(string cs_code,int input_1000, int input_100, int input_50, int input_10, int input_5)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccountingConn"].ToString());
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand comm = new SqlCommand("", conn);
            comm.Transaction = tran;
            try
            {

                string strSQL = @"";
                
                    
                strSQL += @"
                                Declare @ic_sqno int
                                Set @ic_sqno = (Select Top(1)ic_sqno From [InCome] Where cs_code = @cs_code and convert(varchar, getdate(), 111) = convert(varchar, [createdate], 111))
                            
                                IF(@ic_sqno is not null)
                                Begin
                                   Update InCome 
                                   Set [ic_1000]=@ic_1000
                                      ,[ic_100]=@ic_100
                                      ,[ic_50]=@ic_50
                                      ,[ic_10]=@ic_10
                                      ,[ic_5]=@ic_5
                                      ,[ic_sum]=(@ic_1000+@ic_100+@ic_50+@ic_10+@ic_5)
                                      ,createusr=@UserNo
                                    Where ic_sqno=@ic_sqno
                                End
                                Else
                                Begin
                                    Insert into InCome(
                                       [cs_code]
                                      ,[ic_1000]
                                      ,[ic_100]
                                      ,[ic_50]
                                      ,[ic_10]
                                      ,[ic_5]
                                      ,[ic_sum],createusr)
                                      values
                                      (@cs_code,@ic_1000,@ic_100,@ic_50,@ic_10,@ic_5,(@ic_1000+@ic_100+@ic_50+@ic_10+@ic_5),@UserNo)
                                End
                            ";
                comm.CommandText = strSQL;
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@cs_code", cs_code);
                comm.Parameters.AddWithValue("@ic_1000", input_1000);
                comm.Parameters.AddWithValue("@ic_100", input_100);
                comm.Parameters.AddWithValue("@ic_50", input_50);
                comm.Parameters.AddWithValue("@ic_10", input_10);
                comm.Parameters.AddWithValue("@ic_5", input_5);
                comm.Parameters.AddWithValue("@UserNo", HttpContext.Current.Session["UserNo"].ToString());
                comm.ExecuteNonQuery();
                    
                
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