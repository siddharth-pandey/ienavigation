using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientPoC.Model;

namespace ClientPoC.ViewModels
{
    public class DetailsViewModel : HubDetailView
    {
        public DetailsViewModel()
        {
        }
        public string Type
        {
            get { return "HubDetailsView"; }
        }
    }
}