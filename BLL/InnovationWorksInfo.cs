using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class InnovationWorksInfo
    {
        public static List<Models.DB.InnovationWorksInfo> FindByInt(String Value, String ValueName)
        {
            #region 输入合法性检查
            int valueInt;
            try
            {
                valueInt = Convert.ToInt32(Value);
            }
            catch
            {
                return null;
            }
            #endregion

            List<Models.DB.InnovationWorksInfo> list = new List<Models.DB.InnovationWorksInfo>();

            DataTable dt = DAL.Select.QueryOne(valueInt, "Tb_InnovationWorksInfo", ValueName);

            foreach (DataRow row in dt.Rows)
            {
                Models.DB.InnovationWorksInfo model = new Models.DB.InnovationWorksInfo();
                model.BaseContent = row["BaseContent"].ToString();
                model.Features = row["Features"].ToString();
                model.Budget = row["Budget"].ToString();
                model.Id = Convert.ToInt32(row["Id"]);
                model.Category = row["Category"].ToString();
                model.Expection = row["Expection"].ToString();
                model.ProjectID = Convert.ToInt32(row["ProjectID"]);
                model.Purpose = row["Purpose"].ToString();
                model.KeyProblem = row["KeyProblem"].ToString();
                model.References = row["References"].ToString();
                model.PersonnelDivision = row["PersonnelDivision"].ToString();
                model.SecondCategories = row["SecondCategories"].ToString();
                model.PracticalsStep = row["PracticalsStep"].ToString(); ;
                model.ProjectBasic = row["ProjectBasic"].ToString();
                model.ProjectPlan = row["ProjectPlan"].ToString();
                model.SpecificPlan = row["SpecificPlan"].ToString();
                list.Add(model);
            }


            return list;
        }

        public static int CreateMore(String[,] data, String ProjectID)
        {
            #region 检查输入的合法性
            if (data == null)
            {
                return 0;
            }

            #endregion

            #region 把数据组装成对象
            List<Models.DB.InnovationWorksInfo> list = new List<Models.DB.InnovationWorksInfo>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Models.DB.InnovationWorksInfo model = new Models.DB.InnovationWorksInfo();
                model.Category = data[i, 0];
                model.SecondCategories = data[i, 1];
                model.Purpose = data[i, 2];
                model.References = data[i, 3];
                model.BaseContent = data[i, 4];
                model.KeyProblem = data[i, 5];
                model.ProjectBasic = data[i, 6];
                model.SpecificPlan = data[i, 7];
                model.PracticalsStep = data[i, 8];
                model.PersonnelDivision = data[i, 9];
                model.ProjectPlan = data[i,10];
                model.Features = data[i, 11];
                model.Expection = data[i, 12];
                model.Budget = data[i, 13];
                model.ProjectID = Convert.ToInt32(ProjectID);
                list.Add(model);
            }
            #endregion

            return DAL.Create.CreateList(list);
        }


        public static int UpDate(String ID, String[,] data, String ProjectID)
        {
            #region 检查输入的合法性
            int id;
            if (data == null)
            {
                return 0;
            }
            try
            {
                id = Convert.ToInt32(ID);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成对象
            Models.DB.InnovationWorksInfo model = new Models.DB.InnovationWorksInfo();
            model.Category = data[0, 0];
            model.SecondCategories = data[0, 1];
            model.Purpose = data[0, 2];
            model.References = data[0, 3];
            model.BaseContent = data[0, 4];
            model.KeyProblem = data[0, 5];
            model.ProjectBasic = data[0, 6];
            model.SpecificPlan = data[0, 7];
            model.PracticalsStep = data[0, 8];
            model.PersonnelDivision = data[0, 9];
            model.ProjectPlan = data[0, 10];
            model.Features = data[0, 11];
            model.Expection = data[0, 12];
            model.Budget = data[0, 13];
            model.ProjectID = Convert.ToInt32(ProjectID);
            model.Id = id;
            #endregion

            return DAL.Update.ChangeSome(model, "Id");
        }
    }
}
