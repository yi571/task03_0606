﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task03_0606.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Member() {
            return View();
        }

        public ActionResult Login() {
            return View();
        }

        public ActionResult Register() {
            return View();
        }

        public ActionResult ForgotPassword() {
            return View();
        }
    }
}