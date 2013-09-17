using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class InnovationDeclarantInfo
    {
        /// <summary>
        /// 根据项目ID选择一条记录
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public static Models.DB.InnovationDeclarantInfo SelectOne(String ProjectID)
        {
            int projectId = 0;
            Models.DB.InnovationDeclarantInfo DecLarant = new Models.DB.InnovationDeclarantInfo();
            if (string.IsNullOrEmpty(ProjectID))
            {
                return null;
            }
            try
            {
                projectId = Convert.ToInt32(ProjectID);
            }
            catch { return null; }
            DecLarant.ProjectID = projectId;
            System.Data.DataTable dt = DAL.Select.GetList(DecLarant, "ProjectID");
            if (dt.Rows.Count > 0)
            {
                DecLarant.Phone = dt.Rows[0]["Phone"].ToString();
                DecLarant.ProjectID = Convert.ToInt32(dt.Rows[0]["ProjectID"]);
                DecLarant.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                DecLarant.Experience = dt.Rows[0]["Experience"].ToString();
                return DecLarant;
            }
            return null;

        }

        public static int Create(String ProjectID, String Phone, String Experience)
        {
            #region 检查输入的合法性
            int projectid;
            try
            {
                projectid = Convert.ToInt32(ProjectID);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成一个类的对象
            Models.DB.InnovationDeclarantInfo model = new Models.DB.InnovationDeclarantInfo();
            model.Experience = Experience;
            model.Phone = Phone;
            model.ProjectID = projectid;
            #endregion

            return DAL.Create.CreateOne(model);
        }


        public static int Update(String ID,String ProjectID, String Phone, String Experience)
        {
            #region 检查输入的合法性
            int projectid,id;
            try
            {
                id = Convert.ToInt32(ID);
                projectid = Convert.ToInt32(ProjectID);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成一个类的对象
            Models.DB.InnovationDeclarantInfo model = new Models.DB.InnovationDeclarantInfo();
            model.Experience = Experience;
            model.Phone = Phone;
            model.ProjectID = projectid;
            model.Id = id;
            #endregion

            return DAL.Update.ChangeSome(model,"Id");
        }

    }
}
