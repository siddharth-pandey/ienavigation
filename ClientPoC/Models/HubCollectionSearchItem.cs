using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientPoC.Model
{
    public class HubCollectionSearchItem
    {
        //The hub schema ID    
        public String HubId { get; set; }
        //The summary tile width requested    
        public Int32 Width { get; set; }
        //The summary tile height requested    
        public Int32 Height { get; set; }

    }
}