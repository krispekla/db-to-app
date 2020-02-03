using PPPKProjekt.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPPKProjekt
{
    public partial class PutniNalog : System.Web.UI.Page
    {
        private IRepo repo = new Repo(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);
        private static List<TravelOrder> orders;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllOrders();
            }
        }

        private void GetAllOrders()
        {
            orders = repo.GetAllTravelOrder();
            gvTransportOrder.DataSource = orders;
            gvTransportOrder.DataBind();
        }

        protected void ddlFilterOrders_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}