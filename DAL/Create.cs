using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Create
    {

        public static int CreateOne<T>(T models)
        {
            Type type = models.GetType();

            string insertStr1 = "";
            string insertStr2 = "";

            PropertyInfo[] propArray = type.GetProperties();
            List<SqlParameter> paramslist = new List<SqlParameter>();

            foreach (PropertyInfo pi in propArray)
            {
                if (String.Compare(pi.Name,"ID",true)==0 || pi.GetValue(models, null) == null) continue;
                insertStr1 += "["+pi.Name + "],";
                insertStr2 += "@" + pi.Name + ",";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(models, null)));
            }
            insertStr1 = insertStr1.TrimEnd(',');
            insertStr2 = insertStr2.TrimEnd(',');

            String tableName = "Tb_" + type.Name;

            string sql = string.Format("insert into {0}({1}) values({2})", tableName, insertStr1, insertStr2);

            SqlParameter[] parameters = paramslist.ToArray();
            Utility.SQLHelper db = new Utility.SQLHelper();
            return db.ExecuteNonQuery(sql, parameters, System.Data.CommandType.Text);
        }
        #region By云海
        /// <summary>
        /// 插入一列数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Models"></param>
        /// <returns></returns>
        public static int CreateList<T>(List<T> models)
        {
            if (models.Count <= 0)
            {
                return 0;
            }
            List<string> SqlList = new List<string>();
            List<SqlParameter[]> ParameterList = new List<SqlParameter[]>();
            for (int i = 0; i < models.Count; i++)
            {

                Type type = models[0].GetType();

                string insertStr1 = "";
                string insertStr2 = "";

                PropertyInfo[] propArray = type.GetProperties();
                List<SqlParameter> paramslist = new List<SqlParameter>();

                foreach (PropertyInfo pi in propArray)
                {
                    if (String.Compare(pi.Name, "ID", true) == 0 || pi.GetValue(models[i], null) == null) continue;
                    insertStr1 += "[" + pi.Name + "],";
                    insertStr2 += "@" + pi.Name + ",";
                    paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(models[i], null)));
                }
                insertStr1 = insertStr1.TrimEnd(',');
                insertStr2 = insertStr2.TrimEnd(',');

                String tableName = "Tb_" + type.Name;

                string sql = string.Format("insert into {0}({1}) values({2})", tableName, insertStr1, insertStr2);


                SqlParameter[] parameters = paramslist.ToArray();
                SqlList.Add(sql);
                ParameterList.Add(parameters);
            }
            Utility.SQLHelper db = new Utility.SQLHelper();
            return db.ExecuteNonQuery(SqlList, ParameterList);
        }
        #endregion


        #region lxc
        public static int CreateOneReturnID<T>(T models)
        {
            Type type = models.GetType();

            string insertStr1 = "";
            string insertStr2 = "";

            PropertyInfo[] propArray = type.GetProperties();
            List<SqlParameter> paramslist = new List<SqlParameter>();

            foreach (PropertyInfo pi in propArray)
            {
                if (String.Compare(pi.Name, "ID", true) == 0 || pi.GetValue(models, null) == null) continue;
                insertStr1 += "[" + pi.Name + "],";
                insertStr2 += "@" + pi.Name + ",";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(models, null)));
            }

            paramslist.Add(new SqlParameter("id",SqlDbType.Int));

            insertStr1 = insertStr1.TrimEnd(',');
            insertStr2 = insertStr2.TrimEnd(',');

            String tableName = "Tb_" + type.Name;

            string sql = string.Format("insert into {0}({1}) values({2});select @id=SCOPE_IDENTITY()", tableName, insertStr1, insertStr2);

            SqlParameter[] parameters = paramslist.ToArray();
            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
            Utility.SQLHelper db = new Utility.SQLHelper();
            db.ExecuteNonQuery(sql, parameters, System.Data.CommandType.Text);
            int id = Convert.ToInt32(parameters[parameters.Length-1].Value);
            return id;
        }
        #endregion
    }
}
