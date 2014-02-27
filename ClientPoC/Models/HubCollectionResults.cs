using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientPoC.Model;

namespace ClientPoC.Models
{
    public class HubCollectionResults
    {
        public HubCollectionSearchCriteria SearchCriteria { get; set; }
        public HubCollection Result { get; set; }
    }
}