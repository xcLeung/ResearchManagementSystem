using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MatchModel
    {
        /// <summary>
        /// 获取全部记录
        /// </summary>
        /// <returns></returns>
        public static List<Models.DB.MatchModel> SelectMatchModel()
        {
            List<Models.DB.MatchModel> MatchModels = new List<Models.DB.MatchModel>();
            Models.DB.MatchModel MatchModel;
            System.Data.DataTable dt = DAL.Select.GetAll("Tb_MatchModel");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MatchModel = new Models.DB.MatchModel();
                    MatchModel.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    MatchModel.Name = dt.Rows[i]["Name"].ToString();
                    MatchModels.Add(MatchModel);
                }
            }
            return MatchModels;
        }
        /// <summary>
        /// 选择一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static Models.DB.MatchModel SelectOne(int Id)
        {
            Models.DB.MatchModel MatchModel = new Models.DB.MatchModel();
            System.Data.DataTable dt = DAL.Select.GetOne("Tb_MatchModel",Id);
            if (dt.Rows.Count > 0)
            {
                MatchModel.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                MatchModel.Name = dt.Rows[0]["Name"].ToString();
            }
            return MatchModel;
        }
    }
}
