using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Update
    {
        /// <summary>
        /// 更新一系列数据
        /// </summary>
        /// <typeparam name="T">数据库模型</typeparam>
        /// <param name="model">模型实例对象</param>
        /// <param name="target">数据查询条件</param>
        /// <returns>影响行数</returns>
        public static int ChangeSome<T>(T model, string target)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();

            Type type = model.GetType();

            string updateStr1 = "";
            string updateStr2 = "";

            PropertyInfo[] propArray = type.GetProperties();
            PropertyInfo lastPi = type.GetProperty(target);
            List<SqlParameter> paramslist = new List<SqlParameter>();

            foreach (PropertyInfo pi in propArray)
            {
                if ( (String.Compare(pi.Name,target)==0)  || (String.Compare(pi.Name, "ID", true) == 0) || pi.GetValue(model, null) == null) continue;
                updateStr1 += "[" + pi.Name + "]=@" + pi.Name + ",";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(model, null)));
            }
            updateStr1 = updateStr1.TrimEnd(',');
            updateStr2 = "[" + target + "]=@" + target;
            paramslist.Add(new SqlParameter(target, lastPi.GetValue(model, null)));

            string sql = string.Format("update {0} set {1} where {2}", Utility.Tool.ModelNameToTableName(type.Name), updateStr1, updateStr2);

            SqlParameter[] parameters = paramslist.ToArray();

            return db.ExecuteNonQuery(sql, parameters, System.Data.CommandType.Text);
        }

        /// <summary>
        /// 更新一系列数据
        /// </summary>
        /// <typeparam name="T">数据库模型</typeparam>
        /// <param name="model">模型实例对象</param>
        /// <param name="targets">数据查询条件</param>
        /// <returns>影响行数</returns>
        public static int ChangeSome<T>(T model, string[] targets)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();

            Type type = model.GetType();

            string updateStr1 = "";
            string updateStr2 = "";

            PropertyInfo[] propArray = type.GetProperties();
            List<PropertyInfo> lastPis = new List<PropertyInfo>();
            List<SqlParameter> paramslist = new List<SqlParameter>();
            foreach (string target in targets)
            {
                lastPis.Add(type.GetProperty(target));
            }

            foreach (PropertyInfo pi in propArray)
            {
                if (pi.Name == "ID" || pi.GetValue(model, null) == null) continue;
                if (lastPis.Contains(pi)) continue;
                updateStr1 += "[" + pi.Name + "]=@" + pi.Name + ",";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(model, null)));
            }
            foreach (PropertyInfo lastPi in lastPis.ToArray())
            {
                updateStr2 = "[" + lastPi.Name + "]=@" + lastPi.Name + " and ";
                paramslist.Add(new SqlParameter(lastPi.Name, lastPi.GetValue(model, null)));
            }
            updateStr1 = updateStr1.TrimEnd(',');
            updateStr2 = updateStr2.Substring(0, updateStr2.LastIndexOf(" and ", StringComparison.Ordinal));

            string sql = string.Format("update {0} set {1} where {2}", Utility.Tool.ModelNameToTableName(type.Name), updateStr1, updateStr2);

            SqlParameter[] parameters = paramslist.ToArray();

            return db.ExecuteNonQuery(sql, parameters, System.Data.CommandType.Text);
        }



        #region lxc
        public static int UpdatePaperUrl(int ID,String Url)
        {
            string sql = String.Format("update Tb_CupProjectModel set [PaperDoc] = @PaperDoc where [ID]=@ID");
            SqlParameter[] parameters = {
                                          
                                            new SqlParameter("@PaperDoc", Url),
                                            new SqlParameter("@ID", ID)                                          
                                        };
            Utility.SQLHelper db = new Utility.SQLHelper();
            return db.ExecuteNonQuery(sql, parameters, System.Data.CommandType.Text);
        }

        public static int UpdateRemark(int ID, String Remark,String TableName)
        {
            string sql = String.Format("update {0} set [Remark] = @Remark where [ID]=@ID",TableName);
            SqlParameter[] parameters = {
                                          
                                            new SqlParameter("@Remark", Remark),
                                            new SqlParameter("@ID", ID)                                          
                                        };
            Utility.SQLHelper db = new Utility.SQLHelper();
            return db.ExecuteNonQuery(sql, parameters, System.Data.CommandType.Text);
        }

        public static int UpdateProjectStatus(String Status, String TableName,int ID)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("Update {0} set [Statu]=@Statu,[DeclarationDate]=@DeclarationDate where [ID]=@ID", TableName);
            SqlParameter[] parameters = {
                                          
                                            new SqlParameter("@Statu", Status),
                                            new SqlParameter("@DeclarationDate",System.DateTime.Now),
                                            new SqlParameter("@ID",ID)
                                        };
            return db.ExecuteNonQuery(sql, parameters, System.Data.CommandType.Text);
        }

        #endregion
    }
}
