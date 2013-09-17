using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class InnovationTeamMember
    {

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
            Models.DB.InnovationTeamMember TeamMember = new Models.DB.InnovationTeamMember();
            TeamMember.ProjectID = projectID;
            return DAL.Select.GetCount(TeamMember, "ProjectID");
        }


        public static List<Models.DB.InnovationTeamMember> SelectByProjectId(string ProjectId)
        {
            List<Models.DB.InnovationTeamMember> TeamMemberList = new List<Models.DB.InnovationTeamMember>();
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
            Models.DB.InnovationTeamMember model = new Models.DB.InnovationTeamMember();
            model.ProjectID = projectId;
            System.Data.DataTable dt = DAL.Select.GetList(model, "ProjectID");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new Models.DB.InnovationTeamMember();
                    model.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    model.Name = dt.Rows[i]["Name"].ToString();
                    model.College = dt.Rows[i]["College"].ToString();
                    model.InTimeYear = Convert.ToInt32(dt.Rows[i]["InTimeYear"]);
                    model.Mail = dt.Rows[i]["Mail"].ToString();
                    model.Major = dt.Rows[i]["Major"].ToString();
                    model.Phone = dt.Rows[i]["Phone"].ToString();
                    model.Experience = dt.Rows[i]["Experience"].ToString();
                    model.StudentID = dt.Rows[i]["StudentID"].ToString();
                    model.ProjectID = Convert.ToInt32(dt.Rows[i]["ProjectID"]);
                    TeamMemberList.Add(model);
                }
            }
            return TeamMemberList;
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
                    int num = Convert.ToInt32(data[i, 4]);
                }
            }
            catch
            {
                return 0;
            }

            #endregion

            #region 把数据组装成对象
            List<Models.DB.InnovationTeamMember> list = new List<Models.DB.InnovationTeamMember>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Models.DB.InnovationTeamMember model = new Models.DB.InnovationTeamMember();
                model.Name = data[i, 0];
                model.StudentID = data[i, 1];
                model.College = data[i, 2];
                model.Major = data[i, 3];
                model.InTimeYear = Convert.ToInt32(data[i, 4]);
                model.Phone = data[i, 5];
                model.Mail = data[i, 6];
                model.Experience = data[i, 7];
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
                int num = Convert.ToInt32(data[4]);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成对象
            Models.DB.InnovationTeamMember model = new Models.DB.InnovationTeamMember();
            model.Name = data[0];
            model.StudentID = data[1];
            model.College = data[2];
            model.Major = data[3];
            model.InTimeYear = Convert.ToInt32(data[4]);
            model.Phone = data[5];
            model.Mail = data[6];
            model.Experience = data[7];
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
                int num = Convert.ToInt32(data[4]);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数组组装成对象
            Models.DB.InnovationTeamMember model = new Models.DB.InnovationTeamMember();
            model.Name = data[0];
            model.StudentID = data[1];
            model.College = data[2];
            model.Major = data[3];
            model.InTimeYear = Convert.ToInt32(data[4]);
            model.Phone = data[5];
            model.Mail = data[6];
            model.Experience = data[7];
            model.Id = Convert.ToInt32(data[8]);
            model.ProjectID = Convert.ToInt32(ProjectID);
            #endregion

            return DAL.Update.ChangeSome(model, "Id");
        }


        
    }
}
