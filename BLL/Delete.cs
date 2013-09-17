using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Delete
    {

        public static int Word(string tableName, string ID)
        {
            #region 检查输入的合法性
            int id = 0;
            try
            {
                id = Convert.ToInt32(ID);
            }
            catch
            {
                return 0;
            }
            #endregion

            return DAL.Delete.Work(tableName, id);
        }


        public static int WordList(String ProjectID,String TableName)
        {
            #region 检查输入的合法性
            if (String.IsNullOrEmpty(TableName))
            {
                return 0;
            }
            int prjectid = 0;
            try
            {
                prjectid = Convert.ToInt32(ProjectID);
            }
            catch
            {
                return 0;
            }
            #endregion


            List<String> TableList = new List<String>();
            if (TableName == "Tb_CupProjectModel")
            {
                TableList.Add("Tb_CupDeclarantInfo");
                TableList.Add("Tb_CupTeamMemberInfo");
                TableList.Add("Tb_CupWorksInfo");
                TableList.Add("Tb_CupWorksInvention");
                TableList.Add("Tb_CupWorksSurvey");
                TableList.Add("Tb_RecommendedInfo");
            }
            else if (TableName == "Tb_InnovationProjectModel")
            {
                TableList.Add("Tb_TutorInfo");
                TableList.Add("Tb_InnovationDeclarantInfo");
                TableList.Add("Tb_InnovationTeamMember");
                TableList.Add("Tb_InnovationWorksInfo");
            }
            return DAL.Delete.WorkList(TableList, prjectid);
        }

    }
}
