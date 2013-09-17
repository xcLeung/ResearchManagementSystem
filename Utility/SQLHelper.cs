using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Utility
{
    public class SQLHelper
    {
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private SqlDataReader sdr = null;
        public SQLHelper()
        {
            string strconn = ConfigurationManager.ConnectionStrings["sqlserver_connection_string"].ConnectionString;
            conn = new SqlConnection(strconn);
        }

        private SqlConnection GetConn()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        //关闭数据库连接  
        private void OutConn()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        ///  执行不带参数的增删改SQL语句或存储过程  
        public int ExecuteNonQuery(string cmdText, CommandType ct)
        {
            int res;
            try
            {
                cmd = new SqlCommand(cmdText, GetConn());
                cmd.CommandType = ct;
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OutConn();
            }
            return res;
        }
        ///  执行带参数的增删改SQL语句或存储过程  
        public int ExecuteNonQuery(string cmdText, SqlParameter[] paras, CommandType ct)
        {
            int res;
            try
            {
                cmd = new SqlCommand(cmdText, GetConn());
                cmd.CommandType = ct;
                cmd.Parameters.AddRange(paras);
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OutConn();
            }
            return res;
        }

        ///  执行不带参数的查询SQL语句或存储过程  
        public DataTable ExecuteQuery(string cmdText, CommandType ct)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand(cmdText, GetConn());
            cmd.CommandType = ct;
            using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                dt.Load(sdr);
            }
            return dt;
        }
        ///  执行带参数的查询SQL语句或存储过程  
        public DataTable ExecuteQuery(string cmdText, SqlParameter[] paras, CommandType ct)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand(cmdText, GetConn());
            cmd.CommandType = ct;
            cmd.Parameters.AddRange(paras);
            using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                dt.Load(sdr);
            }
            return dt;
        }

        #region By云海
        /// <summary>
        /// 通过事务执行多条语句
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="ParameterList"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(List<string> SqlList, List<SqlParameter[]> ParameterList)
        {
            int n = 0;
            if (SqlList.Count != ParameterList.Count)
            {
                return 0;
            }
            try
            {
                using (SqlTransaction trans = GetConn().BeginTransaction())
                {
                    cmd = new SqlCommand { CommandTimeout = 0 };
                    try
                    {

                        for (int i = 0; i < ParameterList.Count; i++)
                        {
                            SqlParameter[] cmdParms = ParameterList[i];
                            cmd.Connection = GetConn();
                            cmd.Transaction = trans;
                            cmd.Parameters.AddRange(cmdParms);
                            cmd.CommandText = SqlList[i];
                            n += cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();

                    }
                    catch { }
                }

            }
            catch
            {
                return 0;
            }
            finally
            {
                OutConn();
            }
            return n;
        }
        #endregion
    }
}
