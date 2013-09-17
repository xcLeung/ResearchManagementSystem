using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User
    {
        public static int Insert(Models.DB.User user)
        {
            
            string sql = string.Format("insert into Tb_User(Name,Password,RoleID,Enable) values(@Name,@Password,@RoleID,@Enable);select @id=SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
                                            new SqlParameter("Name", SqlDbType.NVarChar,255),
                                            new SqlParameter("Password", user.Password),
                                            new SqlParameter("RoleID", SqlDbType.Int),
                                            new SqlParameter("Enable",SqlDbType.Bit),
                                            new SqlParameter("id", SqlDbType.Int)
                                        };

            parameters[0].Value = user.Name;
            parameters[1].Value = user.Password;
            parameters[2].Value = user.RoleId;
            parameters[3].Value = user.Enable;
            parameters[4].Direction = ParameterDirection.Output;

         //   int temp = Convert.ToInt32(parameters[4].Value);

            Utility.SQLHelper db = new Utility.SQLHelper();
            db.ExecuteNonQuery(sql, parameters, CommandType.Text);
            int id = Convert.ToInt32(parameters[4].Value);
            return id;
        
        }

         public static DataTable QueryOne(String UserName, String Password)
         {
             string sql = "select * from Tb_User where Name=@Name and Password=@Password";
             SqlParameter[] parameters = {
                                            new SqlParameter("@Name", UserName),
                                            new SqlParameter("@Password", Password)
                                        };
         
             Utility.SQLHelper db = new Utility.SQLHelper();
             return db.ExecuteQuery(sql, parameters, CommandType.Text);
         }


         public static DataTable QueryOne(String UserName)
         {
             string sql = "select * from Tb_User where Name=@Name";
             SqlParameter[] parameters = {
                                            new SqlParameter("@Name", UserName)
                                         };

             Utility.SQLHelper db = new Utility.SQLHelper();
             return db.ExecuteQuery(sql, parameters, CommandType.Text);
         }
    }
}
