using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPoC.Models
{
    public class HubCollectionItem
    {
        public String HubSchemaId { get; set; }
        //The Name of the schema 
        public String Name { get; set; }
        //The Id for the summary (tile) layout. Use this as the key to retrieve the relevant layout markup from the layout dictionary.   
        public String SummaryViewId { get; set; }
        //The Id for the full page detail layout. Use this as the key to retrieve the relevant layout info from the layout dictionary.   
        public String DetailViewId { get; set; }
        //The Id for edit view. Use this as the key to retrieve the relevant layout info from the edit view dictionary.   
        public String EditViewId { get; set; }
        //The data used to feed the views (Can be null) 
        public HubData Data { get; set; }
    }
}
