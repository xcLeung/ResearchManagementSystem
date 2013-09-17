using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CupDeclarantInfo
    {
        #region lxc
        /// <summary>
       /// 插入挑战杯杯申报人信息
       /// </summary>
       /// <param name="BirthDay"></param>
       /// <param name="SchoolSystme"></param>
       /// <param name="PaperTitle"></param>
       /// <param name="TopAddress"></param>
       /// <param name="TopPostalCode"></param>
       /// <param name="TopPhone"></param>
       /// <param name="Address"></param>
       /// <param name="PostalCode"></param>
       /// <param name="Phone"></param>
       /// <param name="ProjectModelID"></param>
       /// <returns></returns>
       public static int Create(String BirthDay, String SchoolSystme, String PaperTitle, String TopAddress,String BackGround,
                      String TopPostalCode, String TopPhone, String Address, String PostalCode, String Phone, String ProjectID)
        {
            #region 检查输入的合法性

            int projectid;
            DateTime birthday;
            try
            {
                birthday = Convert.ToDateTime(BirthDay);
                projectid = Convert.ToInt32(ProjectID);
            }
            catch
            {
                return 0;
            }
            #endregion

            #region 把数据组装成一个对象
            Models.DB.CupDeclarantInfo model = new Models.DB.CupDeclarantInfo();
            model.Address = Address;
            model.BackGround = BackGround;
            model.Birthday = birthday;
            model.PaperTitle = PaperTitle;
            model.Phone = Phone;
            model.PostalCode = PostalCode;
            model.ProjectID = projectid;
            model.SchoolSystme = SchoolSystme;
            model.TopAddress = TopAddress;
            model.TopPhone = TopPhone;
            model.TopPostalCode = TopPostalCode;
            #endregion

            return DAL.Create.CreateOne(model);

        }

       public static Models.DB.CupDeclarantInfo SelectOneByProjectID(string ProjectID)
       {
           int projectId = 0;
           Models.DB.CupDeclarantInfo DecLarant = new Models.DB.CupDeclarantInfo();
           if (string.IsNullOrEmpty(ProjectID))
           {
               return DecLarant;
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
               DecLarant.Birthday = Convert.ToDateTime(dt.Rows[0]["Birthday"]);
               DecLarant.SchoolSystme = dt.Rows[0]["SchoolSystme"].ToString();
               DecLarant.PaperTitle = dt.Rows[0]["PaperTitle"].ToString();
               DecLarant.Address = dt.Rows[0]["Address"].ToString();
               DecLarant.PostalCode = dt.Rows[0]["PostalCode"].ToString();
               DecLarant.Phone = dt.Rows[0]["Phone"].ToString();
               DecLarant.ProjectID = Convert.ToInt32(dt.Rows[0]["ProjectID"]);
               DecLarant.BackGround = dt.Rows[0]["BackGround"].ToString();
               DecLarant.TopAddress = dt.Rows[0]["TopAddress"].ToString();
               DecLarant.TopPostalCode = dt.Rows[0]["TopPostalCode"].ToString();
               DecLarant.TopPhone = dt.Rows[0]["TopPhone"].ToString();
               DecLarant.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
               return DecLarant;
           }
           return null;

       }


       public static int Updata(String ID,String BirthDay, String SchoolSystme, String PaperTitle, String TopAddress, String BackGround,
                     String TopPostalCode, String TopPhone, String Address, String PostalCode, String Phone, String ProjectID)
       {
           #region 检查输入的合法性
           int projectid;
           DateTime birthday;
           int id;
           try
           {
               id=Convert.ToInt32(ID);
               birthday = Convert.ToDateTime(BirthDay);
               projectid = Convert.ToInt32(ProjectID);
           }
           catch
           {
               return 0;
           }
           #endregion

           


           #region 把数据组装成一个对象
           Models.DB.CupDeclarantInfo model = new Models.DB.CupDeclarantInfo();
           model.Address = Address;
           model.BackGround = BackGround;
           model.Birthday = birthday;
           model.PaperTitle = PaperTitle;
           model.Phone = Phone;
           model.PostalCode = PostalCode;
           model.ProjectID = projectid;
           model.SchoolSystme = SchoolSystme;
           model.TopAddress = TopAddress;
           model.TopPhone = TopPhone;
           model.TopPostalCode = TopPostalCode;
           model.Id = id;
           #endregion

           
           return DAL.Update.ChangeSome(model, "Id");
       }



        #endregion

       #region By云海
       public static Models.DB.CupDeclarantInfo SelectOne(string ProjectID)
       {
           int projectId = 0;
           Models.DB.CupDeclarantInfo DecLarant = new Models.DB.CupDeclarantInfo();
           if (string.IsNullOrEmpty(ProjectID))
           {
               return DecLarant;
           }
           try
           {
               projectId = Convert.ToInt32(ProjectID);
           }
           catch { return DecLarant; }
           DecLarant.ProjectID = projectId;
           System.Data.DataTable dt = DAL.Select.GetList(DecLarant, "ProjectID");
           if (dt.Rows.Count > 0)
           {
               DecLarant.Birthday = Convert.ToDateTime(dt.Rows[0]["Birthday"]);
               DecLarant.SchoolSystme = dt.Rows[0]["SchoolSystme"].ToString();
               DecLarant.PaperTitle = dt.Rows[0]["PaperTitle"].ToString();
               DecLarant.Address = dt.Rows[0]["Address"].ToString();
               DecLarant.PostalCode = dt.Rows[0]["PostalCode"].ToString();
               DecLarant.Phone = dt.Rows[0]["Phone"].ToString();
               DecLarant.ProjectID = Convert.ToInt32(dt.Rows[0]["ProjectID"]);
               DecLarant.BackGround = dt.Rows[0]["BackGround"].ToString();
               DecLarant.TopAddress = dt.Rows[0]["TopAddress"].ToString();
               DecLarant.TopPostalCode = dt.Rows[0]["TopPostalCode"].ToString();
               DecLarant.TopPhone = dt.Rows[0]["TopPhone"].ToString();
               DecLarant.Id = Convert.ToInt32(dt.Rows[0]["Id"]);

           }
           return DecLarant;

       }
       #endregion
    }
}
