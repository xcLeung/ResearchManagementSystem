using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RecommendInfo
    {
        public static int Create(String[] data, String ProjectID)
        {
            #region 检查输入的合法性
            if (data == null)
            {
                return 0;
            }
            try
            {              
             int num = Convert.ToInt32(data[2]);              
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成对象
            Models.DB.RecommendedInfo model = new Models.DB.RecommendedInfo();
            model.Name = data[0];
            model.Sex = data[1];
            model.Age = Convert.ToInt32(data[2]);
            model.Title = data[3];
            model.WorkUnits = data[4];
            model.Address = data[5];
            model.PostalCode = data[6];
            model.Phone = data[7];
            model.HomePhone = data[8];
            model.Elaborate = data[9];
            model.Evaluate = data[10];
            model.Remark = data[11];
            model.ProjectID = Convert.ToInt32(ProjectID);
            #endregion

            return DAL.Create.CreateOne(model);
        }

        public static int Updata(String[] data,String ProjectID)
        {
            #region 检查输入的合法性
            if (data == null)
            {
                return 0;
            }
            try
            {
                int num = Convert.ToInt32(data[2]);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成对象
            Models.DB.RecommendedInfo model = new Models.DB.RecommendedInfo();
            model.Name = data[0];
            model.Sex = data[1];
            model.Age = Convert.ToInt32(data[2]);
            model.Title = data[3];
            model.WorkUnits = data[4];
            model.Address = data[5];
            model.PostalCode = data[6];
            model.Phone = data[7];
            model.HomePhone = data[8];
            model.Elaborate = data[9];
            model.Evaluate = data[10];
            model.Remark = data[11];
            model.ProjectID = Convert.ToInt32(ProjectID);
            model.ID = Convert.ToInt32(data[12]);
            #endregion

            return DAL.Update.ChangeSome(model, "ID");
        }



        public static int CreateMore(String[,] data, String ProjectID)
        {
            #region 检查输入的合法性
            if (data == null)
            {
                return 0;
            }

            try
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    int num = Convert.ToInt32(data[i, 2]);
                }
            }
            catch
            {
                return 0;
            }

            #endregion

            #region 把数据组装成对象
            List<Models.DB.RecommendedInfo> list = new List<Models.DB.RecommendedInfo>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Models.DB.RecommendedInfo model = new Models.DB.RecommendedInfo();
                model.Name = data[i, 0];
                model.Sex = data[i, 1];
                model.Age = Convert.ToInt32(data[i, 2]);
                model.Title = data[i, 3];
                model.WorkUnits = data[i, 4];
                model.Address = data[i, 5];
                model.PostalCode = data[i, 6];
                model.Phone = data[i, 7];
                model.HomePhone = data[i, 8];
                model.Elaborate = data[i, 9];
                model.Evaluate = data[i, 10];
                model.Remark = data[i, 11];
                model.ProjectID = Convert.ToInt32(ProjectID);
                list.Add(model);
            }
            #endregion

            return DAL.Create.CreateList(list);
        }


        #region By云海
        /// <summary>
        /// 根据ProjectID获取数据
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public static List<Models.DB.RecommendedInfo> SelectByProjectId(string ProjectId)
        {
            List<Models.DB.RecommendedInfo> Recommenders = new List<Models.DB.RecommendedInfo>();
            int projectId = 0;
            if (string.IsNullOrEmpty(ProjectId))
            {
                return Recommenders;
            }
            try
            {
                projectId = Convert.ToInt32(ProjectId);
            }
            catch
            {
                return Recommenders;
            }
            Models.DB.RecommendedInfo Recommender = new Models.DB.RecommendedInfo();
            Recommender.ProjectID = projectId;
            System.Data.DataTable dt = DAL.Select.GetList(Recommender, "ProjectID");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Recommender = new Models.DB.RecommendedInfo();
                    Recommender.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    Recommender.Name = dt.Rows[i]["Name"].ToString();
                    Recommender.Sex = dt.Rows[i]["Sex"].ToString();
                    Recommender.Age = Convert.ToInt32(dt.Rows[i]["Age"]);
                    Recommender.Title = dt.Rows[i]["Title"].ToString();
                    Recommender.WorkUnits = dt.Rows[i]["WorkUnits"].ToString();
                    Recommender.Address = dt.Rows[i]["Address"].ToString();
                    Recommender.PostalCode = dt.Rows[i]["PostalCode"].ToString();
                    Recommender.Phone = dt.Rows[i]["Phone"].ToString();
                    Recommender.Elaborate = dt.Rows[i]["Elaborate"].ToString();
                    Recommender.Evaluate = dt.Rows[i]["Evaluate"].ToString();
                    Recommender.Remark = dt.Rows[i]["Remark"].ToString();
                    Recommender.HomePhone = dt.Rows[i]["HomePhone"].ToString();
                    Recommender.ProjectID = Convert.ToInt32(dt.Rows[i]["ProjectID"]);
                    Recommenders.Add(Recommender);
                }
            }
            return Recommenders;
        }
        #endregion
    }
}
