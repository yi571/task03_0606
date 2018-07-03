using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task03_0606.Controllers
{
    public class Feedback
    {
        public int feedbackId { set; get; }
        public int productID { set; get; }
        public string phoneNum { set; get; }
        public string feedbackTxt { set; get; }

    }
}