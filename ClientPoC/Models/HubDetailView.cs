using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientPoC.Model
{
    public class HubDetailView
    {
        public HubDetailView()
        {
            Sample = "sample";
        }
        
        public List<ComponentData> Components { get; set; }
        public String BackgroundColour { get; set; }
        public List<KeyDescription> ViewRoles { get; set; }
        public List<KeyDescription> ViewDeviceTypes { get; set; }
        public List<KeyDescription> ViewContexts { get; set; }

        public string Sample { get; set; }
    }
}