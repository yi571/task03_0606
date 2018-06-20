using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace task03_0606.Models.Cart
{
    
    public static class Operation
    {
        [WebMethod(EnableSession = true)]
        public static task03_0606.Models.Cart.Cart GetCurrentCart() {
            if (HttpContext.Current != null)
            {

                if (HttpContext.Current.Session["Cart"] == null)
                {
                    var order = new Cart();
                    HttpContext.Current.Session["Cart"] = order;
                }
                return (Cart)HttpContext.Current.Session["Cart"];

            }
            else {
                throw new InvalidOperationException("HttpContext.Current.Session為空");
            }
        }

    }
}