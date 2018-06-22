using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Models
{
    public class FoodProduct
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string productPicture { get; set; }
        public int salesVolume { get; set; }
        public string storeProductId { get; set; }
        public string productDescription { get; set; }
        public int productPrice { get; set; }
        public string storeId { get; set; }
        public int productState { get; set; }
        public int categoryID { get; set; }
    }
}