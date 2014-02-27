using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;
using System.Web;
using ClientPoC.Model;
using ClientPoC.Models;
using ClientPoC.Models.Components.StandardContainers;
using ClientPoC.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientPoC.Services.HubData
{
    public class HubDataGetHubLayoutContainerService : BaseHubDataService
    {
        private HubDataRepository _hubDataRepo = new HubDataRepository();
        public HubDataGetHubLayoutContainerService(string useranme, string authToken)
            : base(Constants.Constants.ServerTenantDev + Constants.Constants.HubLayoutContainerClinicalUrl, useranme, authToken)
        {

        }

        public HubLayoutContainer GetHubLayoutContainer()
        {
            var componentData = GetAsync<ComponentData>(_serviceRequestUrl).Result;

            HubLayoutContainer hubLayoutContainer = null;
            if (componentData != null && !string.IsNullOrEmpty(componentData.PropertyString))
            {
                hubLayoutContainer = JsonConvert.DeserializeObject<HubLayoutContainer>(componentData.PropertyString);
                if (hubLayoutContainer != null && !string.IsNullOrEmpty(hubLayoutContainer.HubDetailsStr))
                {
                    var hubDetails = JsonConvert.DeserializeObject<List<HubDetails>>(hubLayoutContainer.HubDetailsStr);
                    if (hubDetails != null && hubDetails.Count > 0)
                    {
                        hubLayoutContainer.HubDetails = new List<HubDetails>();
                        hubLayoutContainer.HubDetails = hubDetails;
                    }
                }
            }
            return hubLayoutContainer;
        }

        public async Task<ComponentData> GetComponentData()
        {
            return await GetAsync<ComponentData>(_serviceRequestUrl);
        }
    }
}