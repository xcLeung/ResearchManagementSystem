using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Select
    {
        public static double Count(String TableName)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            string sql = string.Format("select count(*) from {0}",TableName);
            DataTable dt = db.ExecuteQuery(sql, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }


     
    }
}
