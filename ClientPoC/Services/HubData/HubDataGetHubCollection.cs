using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using ClientPoC.Model;
using ClientPoC.Models;

namespace ClientPoC.Services.HubData
{
    public class HubDataGetHubCollection : BaseHubDataService
    {
        public HubDataGetHubCollection(string useranme, string authToken) : base(Constants.Constants.ServerTenantDev + Constants.Constants.PostHubDataCollectionUrl, useranme, authToken)
        {
        }

        public async Task<HubCollectionResults> GetHubCollection(List<HubDetails> hubDetails)
        {
            var hubCollectionSearchCriteria = new HubCollectionSearchCriteria();
            hubCollectionSearchCriteria.ContextId = "patients/33";
            hubCollectionSearchCriteria.Hubs = new List<HubCollectionSearchItem>();

            foreach (HubCollectionSearchItem searchItem in hubDetails.Select(hub => new HubCollectionSearchItem
            {
                HubId = hub.HubId,
                Width = hub.LayoutWidth,
                Height = hub.LayoutHeight
                
            }))
            {
                hubCollectionSearchCriteria.Hubs.Add(searchItem);
            }

            var response = await PostAsync<HubCollectionSearchCriteria, HubCollectionResults>(_serviceBaseAddress,
                _serviceRequestUrl, hubCollectionSearchCriteria);

            return response;
        }


    }
}