using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Role
    {
        public static DataTable QueryOne(String RoleName)
        {
            string sql = "select * from Tb_Role where Name=@Name";
            SqlParameter[] parameters = {
                                            new SqlParameter("Name", SqlDbType.NVarChar,255)
                                        };
            parameters[0].Value = RoleName;
            Utility.SQLHelper db = new Utility.SQLHelper();
            return db.ExecuteQuery(sql, parameters, CommandType.Text);
        }

    }
}
