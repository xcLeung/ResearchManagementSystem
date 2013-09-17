using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CupTeamMemberInfo
    {
        public static int CreateMore(String[,] data,String ProjectID)
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
            List<Models.DB.CupTeamMemberInfo> list = new List<Models.DB.CupTeamMemberInfo>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Models.DB.CupTeamMemberInfo model = new Models.DB.CupTeamMemberInfo();
                model.Name = data[i, 0];
                model.Sex = data[i, 1];
                model.Age = Convert.ToInt32(data[i, 2]);
                model.BackGround = data[i, 3];
                model.WorkUnit = data[i, 4];
                model.ProjectID = Convert.ToInt32(ProjectID);
                list.Add(model);
            }
            #endregion

            return DAL.Create.CreateList(list);

        }

        /// <summary>
        /// 根据ProjectID获取数据
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public static List<Models.DB.CupTeamMemberInfo> SelectByProjectId(string ProjectId)
        {
            List<Models.DB.CupTeamMemberInfo> TeamMemberList = new List<Models.DB.CupTeamMemberInfo>();
            int projectId = 0;
            if (string.IsNullOrEmpty(ProjectId))
            {
                return null;
            }
            try
            {
                projectId = Convert.ToInt32(ProjectId);
            }
            catch
            {
                return null;
            }
            Models.DB.CupTeamMemberInfo model = new Models.DB.CupTeamMemberInfo();
            model.ProjectID = projectId;
            System.Data.DataTable dt = DAL.Select.GetList(model, "ProjectID");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new Models.DB.CupTeamMemberInfo();
                    model.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    model.Name = dt.Rows[i]["Name"].ToString();
                    model.Sex = dt.Rows[i]["Sex"].ToString();
                    model.Age = Convert.ToInt32(dt.Rows[i]["Age"]);
                    model.BackGround = dt.Rows[i]["BackGround"].ToString();
                    model.WorkUnit = dt.Rows[i]["WorkUnit"].ToString();                  
                    model.ProjectID = Convert.ToInt32(dt.Rows[i]["ProjectID"]);
                    TeamMemberList.Add(model);
                }
            }
            return TeamMemberList;
        }

        public static int Create(String[] data,String ProjectID)
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
            Models.DB.CupTeamMemberInfo model = new Models.DB.CupTeamMemberInfo();
            model.Name = data[0];
            model.Sex = data[1];
            model.Age = Convert.ToInt32(data[2]);
            model.BackGround = data[3];
            model.WorkUnit = data[4];
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
                int num = Convert.ToInt32(data[2]);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数组组装成对象
            Models.DB.CupTeamMemberInfo model = new Models.DB.CupTeamMemberInfo();
            model.Name = data[0];
            model.Sex = data[1];
            model.Age = Convert.ToInt32(data[2]);
            model.BackGround = data[3];
            model.WorkUnit = data[4];
            model.ID = Convert.ToInt32(data[5]);
            model.ProjectID = Convert.ToInt32(ProjectID);
            #endregion

            return DAL.Update.ChangeSome(model, "ID");
        }



        #region YunHai
        /// <summary>
        /// 根据ProjectID统计记录
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public static double CountByProjectID(string ProjectID)
        {
            int projectID = 0;
            if (string.IsNullOrEmpty(ProjectID))
            {
                return 0;
            }
            try
            {
                projectID = Convert.ToInt32(ProjectID);
            }
            catch
            {
                return 0;
            }
            Models.DB.CupTeamMemberInfo TeamMember = new Models.DB.CupTeamMemberInfo();
            TeamMember.ProjectID = projectID;
            return DAL.Select.GetCount(TeamMember, "ProjectID");
        }
        #endregion
    }
}
