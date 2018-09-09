using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LoginAuth
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    

    public class LoginServiceSql : ILoginService
    {
        public bool AuthenticateUser(string userName, string password)
        {
            bool retvalue = false;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAppConn"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("CheckLoginDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@username", userName));
                cmd.Parameters.Add(new SqlParameter("@password", password));
                DataTable dt = new DataTable();
                da.Fill(dt);
                retvalue = (dt != null && dt.Rows.Count > 0) ? true : false;
            }
            return retvalue;
        }
    }
}
