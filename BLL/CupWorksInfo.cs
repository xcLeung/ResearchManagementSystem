using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CupWorksInfo
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
            List<Models.DB.CupWorksInfo> list = new List<Models.DB.CupWorksInfo>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Models.DB.CupWorksInfo model = new Models.DB.CupWorksInfo();
                model.SecondCategories = data[i, 0];
                model.ThirdCategories = data[i, 1];
                model.FourthCategories = data[i, 2];
                model.Purpose = data[i, 3];
                model.Features = data[i, 4];
                model.ApplyValue = data[i, 5];
                model.PaperDigest = data[i, 6];
                model.ReceivedAwards = data[i, 7];
                model.TestResult = data[i, 8];
                model.References = data[i, 9];
                model.MaterialsList = data[i, 10];
                model.SameResearchLevel = data[i, 11];
                model.ProjectID = Convert.ToInt32(ProjectID);
                list.Add(model);
            }
            #endregion

            return DAL.Create.CreateList(list);
        }

        public static List<Models.DB.CupWorksInfo> FindByInt(String Value,String ValueName){
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

            List<Models.DB.CupWorksInfo> list = new List<Models.DB.CupWorksInfo>();

            DataTable dt = DAL.Select.QueryOne(valueInt, "Tb_CupWorksInfo", ValueName);

            foreach(DataRow row in dt.Rows)
            {
                Models.DB.CupWorksInfo model = new Models.DB.CupWorksInfo();
                model.ApplyValue = row["ApplyValue"].ToString();
                model.Features = row["Features"].ToString();
                model.FourthCategories = row["FourthCategories"].ToString();
                model.ID = Convert.ToInt32(row["ID"]);
                model.MaterialsList = row["MaterialsList"].ToString();
                model.PaperDigest = row["PaperDigest"].ToString();
                model.ProjectID = Convert.ToInt32(row["ProjectID"]);
                model.Purpose = row["Purpose"].ToString();
                model.ReceivedAwards = row["ReceivedAwards"].ToString();
                model.References = row["References"].ToString();
                model.SameResearchLevel = row["SameResearchLevel"].ToString();
                model.SecondCategories = row["SecondCategories"].ToString();
                model.TestResult = row["TestResult"].ToString(); ;
                model.ThirdCategories = row["ThirdCategories"].ToString();
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
            Models.DB.CupWorksInfo model = new Models.DB.CupWorksInfo();
            model.SecondCategories = data[0, 0];
            model.ThirdCategories = data[0, 1];
            model.FourthCategories = data[0, 2];
            model.Purpose = data[0, 3];
            model.Features = data[0, 4];
            model.ApplyValue = data[0, 5];
            model.PaperDigest = data[0, 6];
            model.ReceivedAwards = data[0, 7];
            model.TestResult = data[0, 8];
            model.References = data[0, 9];
            model.MaterialsList = data[0, 10];
            model.SameResearchLevel = data[0, 11];
            model.ID = Convert.ToInt32(ID);
            model.ProjectID = Convert.ToInt32(ProjectID);                     
            #endregion

            return DAL.Update.ChangeSome(model, "ID");
        }
    }
}
