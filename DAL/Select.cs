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
    public class Select
    {
        #region lxc

        /// <summary>
        /// 根据Int型数据选择列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static System.Data.DataTable QueryOne<T>(T value, String TableName,String ValueName)
        {
            string sql = String.Format("SELECT * From {0} WHERE {1}=@{2}", TableName,ValueName,ValueName);
            SqlParameter[] parameters = {
                                            new SqlParameter("@"+ValueName, value)

                                        };
            Utility.SQLHelper db = new Utility.SQLHelper();
            return db.ExecuteQuery(sql, parameters, CommandType.Text);

        }


        /// <summary>
        /// 多条件分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="targets"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public static DataTable MatchSearchByStudent(String tableName, int pageSize, int currentPage, String orderField, String orderValue,String CollegeScau,String CollegeStudent,String Status)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("select top {0} * From(select row_number() over (order by {1} {2}) as RID, * from {3} where ([College]=@CollegeScau OR [College]=@CollegeStudent) AND [Status]=@Status ) A where RID>{4}",pageSize,orderField,orderValue,tableName,pageSize*(currentPage-1));
            SqlParameter[] parameters = {
                                          
                                            new SqlParameter("@CollegeScau", CollegeScau),
                                            new SqlParameter("@CollegeStudent", CollegeStudent),
                                            new SqlParameter("@Status",Status)
                                        };
            return db.ExecuteQuery(sql, parameters, CommandType.Text);
       
        }

       

        public static DataTable SearchByCollege(String TableName, int pageSize, int currentPage, String orderField, String orderValue, String College)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("select top {0} * From(select row_number() over (order by {1} {2}) as RID, * from {3} where ([College]=@College)) A where RID>{4}", pageSize, orderField, orderValue,TableName ,pageSize * (currentPage - 1));
            SqlParameter[] parameters = {
                                          
                                            new SqlParameter("@College", College)
                                        };
            return db.ExecuteQuery(sql, parameters, CommandType.Text);

        }

        public static Double CountByCollege(String College,String TableName)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("select count(*) from {0} where [College]=@College",TableName);
            SqlParameter[] parameters = {
                                          
                                            new SqlParameter("@College", College)
                                        };
            DataTable dt = db.ExecuteQuery(sql, parameters, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        public static DataTable MatchCountByStudent(String CollegeScau, String CollegeStudent, String Status)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("select * from Tb_Match where ([College]=@CollegeScau OR [College]=@CollegeStudent) AND [Status]=@Status");
            SqlParameter[] parameters = {
                                          
                                            new SqlParameter("@CollegeScau", CollegeScau),
                                            new SqlParameter("@CollegeStudent", CollegeStudent),
                                            new SqlParameter("@Status",Status)
                                        };
            return db.ExecuteQuery(sql, parameters, CommandType.Text);

        }

        public static DataTable FindCheckRecord(int ProjectID, int MatchModelID)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("select * from Tb_CheckRecord where [ProjectID]=@ProjectID AND [MatchModelID]=@MatchModelID");
            SqlParameter[] parameters = {
                                          
                                            new SqlParameter("@ProjectID", ProjectID),
                                            new SqlParameter("@MatchModelID", MatchModelID)
                                        };
            return db.ExecuteQuery(sql, parameters, CommandType.Text);
        }


        public static DataTable FindJudgeMatch(int JudgeID,DateTime Now)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("Select * From (Tb_JudgeInfoModel INNER JOIN Tb_MatchJuryRelation ON Tb_JudgeInfoModel.Id=Tb_MatchJuryRelation.JudgeID INNER JOIN Tb_Match ON Tb_MatchJuryRelation.MatchID=Tb_Match.ID) where (Tb_JudgeInfoModel.Id=@JudgeID And datediff(d,Tb_Match.DeadLine,@DateTime)<0)");
            SqlParameter[] parameters = {                                          
                                            new SqlParameter("@JudgeID", JudgeID),
                                            new SqlParameter("@DateTime",Now)
                                        };
            return db.ExecuteQuery(sql,parameters ,CommandType.Text);
        }

        public static DataTable FindProjectScore(int ProjectID, int MatchModelID)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("Select * From Tb_ProjectScore Where ProjectID=@ProjectID And MatchModelID=@MatchModelID");
            SqlParameter[] parameters = {                                          
                                            new SqlParameter("@ProjectID", ProjectID),
                                            new SqlParameter("@MatchModelID", MatchModelID)
                                        };
            return db.ExecuteQuery(sql, parameters, CommandType.Text);
        }

        public static DataTable FindProjectScore(int ProjectID, int MatchModelID,int JudgeID)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("Select * From Tb_ProjectScore Where ProjectID=@ProjectID And MatchModelID=@MatchModelID And JudgeID=@JudgeID");
            SqlParameter[] parameters = {                                          
                                            new SqlParameter("@ProjectID", ProjectID),
                                            new SqlParameter("@MatchModelID", MatchModelID),
                                            new SqlParameter("@JudgeID",JudgeID)
                                        };
            return db.ExecuteQuery(sql, parameters, CommandType.Text);
        }

        public static DataTable CountByEnableAndCollege(Boolean enable, String College)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = "select count(*) from Tb_User,Tb_StudentInfoModel where Tb_User.ID=Tb_StudentInfoModel.UserId and Tb_User.Enable=@Enable and Tb_StudentInfoModel.College=@College";
            SqlParameter[] parameters = {
                                          new SqlParameter("@Enable",enable),
                                            new SqlParameter("@College", College)
                                        };
            return db.ExecuteQuery(sql, parameters, CommandType.Text);
        }

        public static DataTable SelectByEnableAndCollege(int pageSize, int currentPage, String orderField, String orderValue, String College, Boolean Enable)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("select top {0} * from(select row_number() over (order by {1} {2}) as RID,Tb_StudentInfoModel.* from Tb_User,Tb_StudentInfoModel where (Tb_User.ID=Tb_StudentInfoModel.UserId and Tb_User.Enable=@Enable and Tb_StudentInfoModel.College=@College)) A where RID>{3}",pageSize,orderField,orderValue,pageSize*(currentPage-1));
            SqlParameter[] parameters = {
                                          new SqlParameter("@Enable",Enable),
                                            new SqlParameter("@College", College)
                                        };
            return db.ExecuteQuery(sql, parameters, CommandType.Text);
        }


        public static int ProjectScoreHasRecord(int ProjectID, int MatchModelID)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("select count(*) from Tb_ProjectScore where ProjectID=@ProjectID And MatchModelID=@MatchModelID ");
            SqlParameter[] parameters = {                                          
                                            new SqlParameter("@ProjectID", ProjectID),
                                            new SqlParameter("@MatchModelID", MatchModelID)
                                        };
            DataTable dt = db.ExecuteQuery(sql, parameters, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }


        public static int ProjectScoreHasRecord(int ProjectID, int MatchModelID,int JudgeID)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            String sql = String.Format("select count(*) from Tb_ProjectScore where ProjectID=@ProjectID And MatchModelID=@MatchModelID And JudgeID=@JudgeID");
            SqlParameter[] parameters = {                                          
                                            new SqlParameter("@ProjectID", ProjectID),
                                            new SqlParameter("@MatchModelID", MatchModelID),
                                            new SqlParameter("@JudgeID",JudgeID)
                                        };
            DataTable dt = db.ExecuteQuery(sql, parameters, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
        #endregion

        



        #region 根据ID查询单条数据
        /// <summary>
        /// 选择一条数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">ID字段</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable GetOne(string tableName, int id)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();

            string sql = string.Format("select * from {0} where ID=@ID", tableName);

            SqlParameter[] parameters = {
                                            new SqlParameter("ID", id)
                                        };

            return db.ExecuteQuery(sql, parameters, System.Data.CommandType.Text);
        }
        #endregion

        #region 单一条件查询多条数据
        /// <summary>
        /// 选择一系列数据
        /// </summary>
        /// <typeparam name="T">数据库模型</typeparam>
        /// <param name="model">模型实例对象</param> 
        /// <param name="target">数据查询条件</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable GetList<T>(T model, string target)
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

            string sql = string.Format("select * from {0} where {1}", Utility.Tool.ModelNameToTableName(type.Name), selectStr);

            SqlParameter[] parameters = paramslist.ToArray();

            return db.ExecuteQuery(sql, parameters, System.Data.CommandType.Text);
        }
        #endregion

        #region 多条件查询多条数据
        /// <summary>
        /// 选择一系列数据
        /// </summary>
        /// <typeparam name="T">数据库模型</typeparam>
        /// <param name="model">模型实例对象</param> 
        /// <param name="targets">数据查询条件</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable GetList<T>(T model, string[] targets)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();

            Type type = model.GetType();

            string selectStr = "";
            PropertyInfo[] propArray = type.GetProperties();
            List<SqlParameter> paramslist = new List<SqlParameter>();
            HashSet<string> HStargets = new HashSet<string>(targets);

            foreach (PropertyInfo pi in propArray)
            {
                if (!HStargets.Contains(pi.Name) || pi.GetValue(model, null) == null) continue;
                selectStr += "[" + pi.Name + "]=@" + pi.Name + " and ";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(model, null)));
            }
            selectStr = selectStr.Substring(0, selectStr.LastIndexOf(" and ", StringComparison.Ordinal));

            string sql = string.Format("select * from {0} where {1}", Utility.Tool.ModelNameToTableName(type.Name), selectStr);

            SqlParameter[] parameters = paramslist.ToArray();

            return db.ExecuteQuery(sql, parameters, System.Data.CommandType.Text);
        }
        #endregion

        #region 单一条件多表操作
        /// <summary>
        /// 选择一系列数据（联表）
        /// </summary>
        /// <typeparam name="T">第一型数据库模型</typeparam>
        /// <typeparam name="K">第二型数据库模型</typeparam>
        /// <param name="modelT">第一型数据库对象</param>
        /// <param name="modelK">第二型数据库对象</param>
        /// <param name="sameStr">相同字段</param>
        /// <param name="target">数据查询条件</param>
        /// <returns></returns>
        public static DataTable GetList<T, K>(T modelT, K modelK, string sameStr, string target)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();

            Type typeT = modelT.GetType();
            Type typeK = modelK.GetType();

            string selectStr = "";
            PropertyInfo[] propArrayT = typeT.GetProperties();
            PropertyInfo[] propArrayK = typeK.GetProperties();
            List<SqlParameter> paramslist = new List<SqlParameter>();

            foreach (PropertyInfo pi in propArrayT)
            {
                if (pi.Name != target || pi.Name == sameStr || pi.GetValue(modelT, null) == null) continue;
                selectStr += "[" + pi.Name + "]=@" + pi.Name + " and ";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(modelT, null)));
            }
            foreach (PropertyInfo pi in propArrayK)
            {
                if (pi.Name != target || pi.Name == sameStr || pi.GetValue(modelK, null) == null) continue;
                selectStr += "[" + pi.Name + "]=@" + pi.Name + " and ";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(modelK, null)));
            }
            selectStr += typeT.Name + "." + sameStr + "=" + typeK.Name + "." + sameStr;

            string sql = string.Format("select * from {0},{1} where {2}", Utility.Tool.ModelNameToTableName(typeT.Name), Utility.Tool.ModelNameToTableName(typeK.Name), selectStr);

            SqlParameter[] parameters = paramslist.ToArray();

            return db.ExecuteQuery(sql, parameters, System.Data.CommandType.Text);
        }
        #endregion

        #region 多条件多表操作
        /// <summary>
        /// 选择一系列数据（联表）
        /// </summary>
        /// <typeparam name="T">第一型数据库模型</typeparam>
        /// <typeparam name="K">第二型数据库模型</typeparam>
        /// <param name="modelT">第一型数据库对象</param>
        /// <param name="modelK">第二型数据库对象</param>
        /// <param name="sameStr">相同字段</param>
        /// <param name="targets">数据查询条件</param>
        /// <returns></returns>
        public static DataTable GetList<T, K>(T modelT, K modelK, string sameStr, string[] targets)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();

            Type typeT = modelT.GetType();
            Type typeK = modelK.GetType();

            string selectStr = "";
            PropertyInfo[] propArrayT = typeT.GetProperties();
            PropertyInfo[] propArrayK = typeK.GetProperties();
            List<SqlParameter> paramslist = new List<SqlParameter>();
            HashSet<string> HStargets = new HashSet<string>(targets);

            foreach (PropertyInfo pi in propArrayT)
            {
                if (!HStargets.Contains(pi.Name) || pi.Name == sameStr || pi.GetValue(modelT, null) == null) continue;
                selectStr += "[" + pi.Name + "]=@" + pi.Name + " and ";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(modelT, null)));
            }
            foreach (PropertyInfo pi in propArrayK)
            {
                if (!HStargets.Contains(pi.Name) || pi.Name == sameStr || pi.GetValue(modelK, null) == null) continue;
                selectStr += "[" + pi.Name + "]=@" + pi.Name + " and ";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(modelK, null)));
            }
            selectStr += typeT.Name + "." + sameStr + "=" + typeK.Name + "." + sameStr;

            string sql = string.Format("select * from {0},{1} where {2}", Utility.Tool.ModelNameToTableName(typeT.Name), Utility.Tool.ModelNameToTableName(typeK.Name), selectStr);

            SqlParameter[] parameters = paramslist.ToArray();

            return db.ExecuteQuery(sql, parameters, System.Data.CommandType.Text);
        }
        #endregion

        #region 查询指定表的分页数据
        /// <summary>
        ///  查询指定表的分页数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="currentPage">当前页号</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderValue">排序方式</param>
        /// <returns></returns>
        public static DataTable GetSome(string tableName, int pageSize, int currentPage, string orderField, string orderValue)
        {
            string sql = "select top {0} * from(select row_number() over (order by {1} {2}) as RID, * from {3}) A where RID>{4}";
            sql = String.Format(sql, pageSize, orderField, orderValue, tableName, pageSize * (currentPage - 1));
            Utility.SQLHelper db = new Utility.SQLHelper();
            return db.ExecuteQuery(sql, CommandType.Text);
        }
        #endregion

        #region 获得数据总行数
        /// <summary>
        /// 获得数据总行数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static double GetCount(string tableName)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();
            string sql = String.Format("select count(*) from {0}", tableName);
            DataTable dt = db.ExecuteQuery(sql, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
        #endregion

        #region 查询所有数据
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static DataTable GetAll(string tableName)
        {
            string sql = String.Format("select * from {0}", tableName);
            Utility.SQLHelper db = new Utility.SQLHelper();
            return db.ExecuteQuery(sql, CommandType.Text);
        }
        #endregion

        #region By云海

        #region 获得指定条件数据行数
        /// <summary>
        /// 获得指定条件数据总行数
        /// </summary>
        public static double GetCount<T>(T model, string target)
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
            string sql = String.Format("select count(*) from {0} where {1}", Utility.Tool.ModelNameToTableName(type.Name), selectStr);
            SqlParameter[] parameters = paramslist.ToArray();

            DataTable dt = db.ExecuteQuery(sql, parameters, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
        #endregion
        #region 获得指定条件数据行数(双表)
        /// <summary>
        /// 获得指定条件数据总行数(双表)
        /// </summary>
        public static double GetCount<T, K>(T modelT, K modelK, string linkStr, string target)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();

            Type typeT = modelT.GetType();
            Type typeK = modelK.GetType();

            string selectStr = "";
            PropertyInfo[] propArrayT = typeT.GetProperties();
            PropertyInfo[] propArrayK = typeK.GetProperties();
            List<SqlParameter> paramslist = new List<SqlParameter>();

            foreach (PropertyInfo pi in propArrayT)
            {
                if (pi.Name != target || pi.Name == linkStr || pi.GetValue(modelT, null) == null) continue;
                selectStr += "[" + pi.Name + "]=@" + pi.Name + " and ";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(modelT, null)));
            }
            foreach (PropertyInfo pi in propArrayK)
            {
                if (pi.Name != target || pi.Name == linkStr || pi.GetValue(modelK, null) == null) continue;
                selectStr += "[" + pi.Name + "]=@" + pi.Name + " and ";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(modelK, null)));
            }
            selectStr += linkStr;
            string TtableName = Utility.Tool.ModelNameToTableName(typeT.Name);
            string KtableName = Utility.Tool.ModelNameToTableName(typeK.Name);
            string sql = String.Format("select count(*) from {0} where {1}", TtableName + "," + KtableName, selectStr);
            SqlParameter[] parameters = paramslist.ToArray();

            DataTable dt = db.ExecuteQuery(sql, parameters, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }
        #endregion

        #region 获得指定条件分页数据
        /// <summary>
        /// 获得指定条件分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">表对应的模型对相</param>
        /// <param name="target">条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="currentPage">页码</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderValue">排序方式</param>
        /// <returns></returns>
        public static DataTable GetSome<T>(T model, string[] targets, int pageSize, int currentPage, string orderField, string orderValue)
        {
            Utility.SQLHelper db = new Utility.SQLHelper();

            Type type = model.GetType();

            string selectStr = "";
            PropertyInfo[] propArray = type.GetProperties();
            List<SqlParameter> paramslist = new List<SqlParameter>();
            HashSet<string> HStargets = new HashSet<string>(targets);
            foreach (PropertyInfo pi in propArray)
            {
                if (!HStargets.Contains(pi.Name) || pi.GetValue(model, null) == null) continue;
                selectStr += "[" + pi.Name + "]=@" + pi.Name + " and ";
                paramslist.Add(new SqlParameter(pi.Name, pi.GetValue(model, null)));
            }

            selectStr = selectStr.Substring(0, selectStr.LastIndexOf(" and ", StringComparison.Ordinal));
            SqlParameter[] parameters = paramslist.ToArray();
            string sql = "select top {0} * from {1} where {2} and id not in(select top {3} id from {4} where {5} order by {6} {7}) order by {8} {9} ";
            string tableName = Utility.Tool.ModelNameToTableName(type.Name);
            sql = String.Format(sql, pageSize, tableName, selectStr,
                pageSize * (currentPage - 1), tableName, selectStr, orderField, orderValue, orderField, orderValue);
            return db.ExecuteQuery(sql, parameters, CommandType.Text);

        }
        #endregion
        #endregion


    }
}
