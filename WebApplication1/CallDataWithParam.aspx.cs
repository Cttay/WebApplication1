using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class CallDataWithParam : System.Web.UI.Page
    {
        WebData wd = new WebData();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string parameterValue = txtParameter.Text;
            DataSet ds = wd.GetAccount(parameterValue);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
}