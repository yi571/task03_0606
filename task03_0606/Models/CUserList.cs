using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using task03_0606.Models;

namespace task03_0606.Models {
    public class CUserList {
        public int Id { get; set; }
        public string LastName {get;set;}
        public string FirstName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string Pwd { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Road { get; set; }
        public string Lane { get; set; }
        public string Alley { get; set; }
        public string AddressNum { get; set; }
        public string AddressF { get; set; }
    }
}


