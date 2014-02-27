using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientPoC.Model;

namespace ClientPoC.Models
{
    public class HubCollection : BaseQueryable
    {
        public HubCollection()
        {
        }

        //The summary view layouts (tiles). Keyed on the schema Id.    
        public Dictionary<String, HubLayout> SummaryViews { get; set; }
        //The detail view layouts (full page summaries). Keyed on the schema Id.   
        public Dictionary<String, HubDetailView> DetailViews { get; set; }
        //The edit view layouts. Keyed on the schema Id.  
        public Dictionary<String, HubEditView> EditViews { get; set; }
        //The data for the request. Each item will have a pointer to the relevant schema in the Summary view (and detail/edit views if they exist for it)  
        public List<HubCollectionItem> Items { get; set; }

    }
}
