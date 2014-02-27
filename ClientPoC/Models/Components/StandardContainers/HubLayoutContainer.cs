using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientPoC.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ClientPoC.Models.Components.StandardContainers
{
    public class HubLayoutContainer : ComponentData
    {
        public string UseFullWidth { get; set; }
        public string AllowFlow { get; set; }
        public int ContainerWidth { get; set; }
        public int ContainerHeight { get; set; }

        [JsonProperty("HubDetailsCollection")]        
        public List<HubDetails> HubDetails { get; set; }

        [JsonProperty("HubDetails")]
        public string HubDetailsStr { get; set; }

    }
}