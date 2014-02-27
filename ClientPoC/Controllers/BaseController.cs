using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ClientPoC.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewBag.FetchAllAtOnce = ConfigurationManager.AppSettings.Get("FetchAllAtOnce").Equals("true");
        }
    }
}
