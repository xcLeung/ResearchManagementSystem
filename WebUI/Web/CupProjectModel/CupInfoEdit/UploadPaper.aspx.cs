using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResearchManagementSystem.Web.CupProjectModel.CupInfoEdit
{
    public partial class UploadPaper : System.Web.UI.Page
    {
        protected List<Models.DB.CupProjectModel> CupModellist;
        protected String ProjectID;


        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Request["ProjectID"];
            CupModellist = BLL.CupProjectModel.FindByInt(ProjectID, "ID");

        }
    }
}