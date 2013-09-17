using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Delete
    {
        public static int Work(string tableName, int id)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();

            string sql = string.Format("delete from {0} where ID=@ID", tableName);

            SqlParameter[] parameters = {
                                            new SqlParameter("ID", id)
                                        };
            return db.ExecuteNonQuery(sql, parameters, System.Data.CommandType.Text);
        }

        public static int WorkList(List<String> TableList, int ProjectID)
        {
            if (TableList.Count <= 0)
            {
                return 0;
            }
            Utility.SQLHelper db = new Utility.SQLHelper();
            List<SqlParameter[]> ParameterList = new List<SqlParameter[]>();
             List<String> SqlList = new List<String>();

            for (int i = 0; i < TableList.Count; i++)
            {
                SqlParameter[] parameters = {
                                            new SqlParameter("ProjectID", ProjectID)
                                        };
                String sql = String.Format("delete from {0} where ProjectID=@ProjectID", TableList[i]);
                ParameterList.Add(parameters);
                SqlList.Add(sql);
            }
            return db.ExecuteNonQuery(SqlList, ParameterList);
        }



        #region By云海
        /// <summary>
        /// 删除指点字段的记录
        /// </summary>
        public static int Work<T>(T model, string target)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();

            Type type = model.GetType();

            string selectStr = "";
            PropertyInfo[] propArray = type.GetProperties();
            List<SqlParameter> paramslist = new List<SqlParameter>();

            foreach (PropertyInfo pi in propArray)
            {
                if (pi.Name != target || pi.GetValue(model, null) == null) continue;
                selectStr += "[" + pi.Name + "]=@" + pi.Name + " and ";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(model, null)));
            }
            selectStr = selectStr.Substring(0, selectStr.LastIndexOf(" and ", StringComparison.Ordinal));

            string sql = string.Format("delete from {0} where {1}", Utility.Tool.ModelNameToTableName(type.Name), selectStr);

            SqlParameter[] parameters = paramslist.ToArray();

            return db.ExecuteNonQuery(sql, parameters, System.Data.CommandType.Text);
        }
        #endregion
    }
}
