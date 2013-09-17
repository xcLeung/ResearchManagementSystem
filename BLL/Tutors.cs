using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Tutors
    {
        public static List<Models.DB.TutorInfo> SelectByProjectId(string ProjectId)
        {
            List<Models.DB.TutorInfo> Recommenders = new List<Models.DB.TutorInfo>();
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
            Models.DB.TutorInfo Recommender = new Models.DB.TutorInfo();
            Recommender.ProjectID = projectId;
            System.Data.DataTable dt = DAL.Select.GetList(Recommender, "ProjectID");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Recommender = new Models.DB.TutorInfo();
                    Recommender.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    Recommender.Name = dt.Rows[i]["Name"].ToString();
                    Recommender.Age = Convert.ToInt32(dt.Rows[i]["Age"]);
                    Recommender.Work = dt.Rows[i]["Work"].ToString();
                    Recommender.Reasearch = dt.Rows[i]["Reasearch"].ToString();
                    Recommender.Achieves = dt.Rows[i]["Achieves"].ToString();
                    Recommender.Recommendation = dt.Rows[i]["Recommendation"].ToString();
                    Recommender.ProjectID = Convert.ToInt32(dt.Rows[i]["ProjectID"]);
                    Recommenders.Add(Recommender);
                }
            }
            return Recommenders;
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
                    int num = Convert.ToInt32(data[i, 1]);
                }
            }
            catch
            {
                return 0;
            }

            #endregion

            #region 把数据组装成对象
            List<Models.DB.TutorInfo> list = new List<Models.DB.TutorInfo>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Models.DB.TutorInfo model = new Models.DB.TutorInfo();
                model.Name = data[i, 0];
                model.Age = Convert.ToInt32(data[i, 1]);
                model.Work = data[i, 2];
                model.Reasearch = data[i, 3];
                model.Achieves = data[i, 4];
                model.Recommendation = data[i, 5];
                model.ProjectID = Convert.ToInt32(ProjectID);
                list.Add(model);
            }
            #endregion

            return DAL.Create.CreateList(list);
        }

        public static int Create(String[] data, String ProjectID)
        {
            #region 检查输入的合法性
            if (data == null)
            {
                return 0;
            }
            try
            {
                int num = Convert.ToInt32(data[1]);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成对象
            Models.DB.TutorInfo model = new Models.DB.TutorInfo();
            model.Name = data[0];
            model.Age = Convert.ToInt32(data[1]);
            model.Work = data[2];
            model.Reasearch = data[3];
            model.Achieves = data[4];
            model.Recommendation = data[5];
            model.ProjectID = Convert.ToInt32(ProjectID);
            #endregion

            return DAL.Create.CreateOne(model);
        }


        public static int Updata(String[] data, String ProjectID)
        {
            #region 检查输入的合法性
            if (data == null)
            {
                return 0;
            }
            try
            {
                int num = Convert.ToInt32(data[1]);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成对象
            Models.DB.TutorInfo model = new Models.DB.TutorInfo();
            model.Name = data[0];
            model.Age = Convert.ToInt32(data[1]);
            model.Work = data[2];
            model.Reasearch = data[3];
            model.Achieves = data[4];
            model.Recommendation = data[5];
            model.ProjectID = Convert.ToInt32(ProjectID);
            model.Id = Convert.ToInt32(data[6]);
            #endregion

            return DAL.Update.ChangeSome(model, "Id");
        }
    }
}
