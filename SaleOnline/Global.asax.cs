using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using SaleOnline.App_Start;

using SaleOnline.Models;

namespace SaleOnline
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //-----
            BundleCollection bundles = BundleTable.Bundles;
            BundleConfig.RegisterBundle(bundles);

        }
        protected void Sesstion_Start(Object sender,EventArgs e)
        {
            Session["TtDangNhap"] = null;
            //-----Cấp cho người truy cập 1 giỏ hàng chưa chứa gì cả
            Session["GioHang"] = new CartShop();
        }
     

    }
}
