using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientPoC.Model;
using ClientPoC.Models;

namespace ClientPoC.ViewModels
{
    public class DefaultControlsViewModel
    {
        public List<ComponentData> Controls { get; set; }

        public DefaultControlsViewModel()
        {
            Controls = new List<ComponentData>();
        }

        public bool IsAsyncEnabled
        {
            get
            {
                bool result = false;
                var configValue = ConfigurationManager.AppSettings["AsyncEnabled"];

                return configValue.ToLower().Equals("true", StringComparison.CurrentCulture);
            }
        }
    }
}