using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace task03_0606
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start() {
            //Session["memberType"] = "Business";
            //// "Customer" , "Admin" ,"Guest"
            //Session["Member"] = 1;

            Session["identity"] = "normal"; //storeUser  normalUser

            Session["userPhone"] = "0972345678"; //0972345678
            Session["seat"] = 2;
            Session["memberType"] = ""; //Guest
            Session["storeId"] = 21354423;
        }

    }
}
