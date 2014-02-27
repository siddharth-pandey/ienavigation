using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientPoC.Model
{
    public class HubCollectionSearchCriteria : PagingContext
    {
        //The current context ID, EG the patient ID    
        public String ContextId { get; set; }
        //The hub schema requests, IE the ID and summary tile size    
        public List<HubCollectionSearchItem> Hubs { get; set; }
        public String SearchDescription { get; set; }
    }
}