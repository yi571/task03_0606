using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models {
    public class CLayoutChoose {
        //public string LayoutUrl { get; set; }

        public string LayoutUrl(string layoutUrl, string identity) {
            string LayoutUrl = layoutUrl;
            
            if (identity == "superUser") {
                const string V = "~/Views/_Member_SuperUser_LayoutPage1.cshtml";
                LayoutUrl = V;
            }
            if (identity == "storeUser") {
                const string V1 = "~/Views/_Member_Home_LayoutPage1.cshtml";
                LayoutUrl = V1;
            }
            if (identity == "store") {
                const string V2 = "~/Views/_Store_Home_LayoutPage1.cshtml";
                LayoutUrl = V2;
            }
           
            return LayoutUrl;

        }

    }
}