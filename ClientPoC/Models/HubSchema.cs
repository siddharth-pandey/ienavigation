using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientPoC.Model
{
    public class HubSchema : BaseEntity
    {
        public String Author { get; set; }
        public String Context { get; set; }
        public String Name { get; set; }
        public Boolean IsPublic { get; set; }
        public Boolean IsObsolete { get; set; }
        public Int32 RefreshInterval { get; set; }
        public Boolean AutoRefresh { get; set; }
        public String DataContext { get; set; }
        public String ExternalId { get; set; }
        public Boolean Draft { get; set; }
        public List<HubLayout> HubLayouts { get; set; }
        public List<HubDetailView> DetailViews { get; set; }
        public HubEditView EditView { get; set; }
        public List<HubRule> Rules { get; set; }
    }
}