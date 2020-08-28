using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Accounting.App_Code
{
    public class ClsCompany
    {        
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
        #endregion
    }


}