using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CupWorksSurvey
    {
        public static int CreateMore(String[,] data,String ProjectID)
        {
            #region 检查输入的合法性
            if (data == null)
            {
                return 0;
            }

            #endregion

            #region 把数据组装成对象
            List<Models.DB.CupWorksSurvey> list = new List<Models.DB.CupWorksSurvey>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Models.DB.CupWorksSurvey model = new Models.DB.CupWorksSurvey();
                model.Categories = data[i, 0];
                model.Purpose = data[i, 1];
                model.Features = data[i, 2];
                model.ApplyValue = data[i, 3];
                model.PaperDigest = data[i, 4];
                model.ReceivedAwards = data[i, 5];
                model.References = data[i, 6];
                model.SurveyWay = data[i, 7];
                model.SurveyUnits = data[i, 8];
                model.SameResearchLevel = data[i, 9];
                model.ProjectID = Convert.ToInt32(ProjectID);
                list.Add(model);
            }
            #endregion

            return DAL.Create.CreateList(list);
        }

        public static List<Models.DB.CupWorksSurvey> FindByInt(String Value, String ValueName)
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

            List<Models.DB.CupWorksSurvey> list = new List<Models.DB.CupWorksSurvey>();

            DataTable dt = DAL.Select.QueryOne(valueInt, "Tb_CupWorksSurvey", ValueName);

            foreach (DataRow row in dt.Rows)
            {
                Models.DB.CupWorksSurvey model = new Models.DB.CupWorksSurvey();
                model.ApplyValue = row["ApplyValue"].ToString();
                model.Features = row["Features"].ToString();
     
                model.Id = Convert.ToInt32(row["ID"]);
                model.Categories = row["Categories"].ToString();
                model.PaperDigest = row["PaperDigest"].ToString();
                model.ProjectID = Convert.ToInt32(row["ProjectID"]);
                model.Purpose = row["Purpose"].ToString();
                model.ReceivedAwards = row["ReceivedAwards"].ToString();
                model.References = row["References"].ToString();
                model.SameResearchLevel = row["SameResearchLevel"].ToString();
                model.SurveyUnits = row["SurveyUnits"].ToString();
                model.SurveyWay = row["SurveyWay"].ToString();

                list.Add(model);
            }


            return list;
        }

        public static int UpData(String ID, String[,] data, String ProjectID)
        {
            #region 检查输入的合法性
            if (data == null)
            {
                return 0;
            }
            #endregion

            #region 把数据组装成对象
            Models.DB.CupWorksSurvey model = new Models.DB.CupWorksSurvey();
            model.Categories = data[0, 0];
            model.Purpose = data[0, 1];
            model.Features = data[0, 2];
            model.ApplyValue = data[0, 3];
            model.PaperDigest = data[0, 4];
            model.ReceivedAwards = data[0, 5];
            model.References = data[0, 6];
            model.SurveyWay = data[0, 7];
            model.SurveyUnits = data[0, 8];
            model.SameResearchLevel = data[0, 9];
            model.ProjectID = Convert.ToInt32(ProjectID);
            model.Id = Convert.ToInt32(ID);
            #endregion

            return DAL.Update.ChangeSome(model, "Id");
        }

    }
}
