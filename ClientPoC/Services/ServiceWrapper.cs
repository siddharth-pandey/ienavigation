using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ClientPoC.Classes;
using ClientPoC.Model;
using ClientPoC.Models;
using ClientPoC.Models.Components.StandardContainers;
using ClientPoC.Services.HubData;
using ClientPoC.ViewModels;

namespace ClientPoC.Services
{
    public class ServiceWrapper : IDisposable
    {
        private string _authToken;
        private string _username;
        public ServiceWrapper(string username, string authToken)
        {
            _username = username;
            _authToken = authToken;
        }

        public void Dispose()
        {
        }

        internal HubLayoutContainer GetHubLayoutContainer()
        {
            using (var service = new HubDataGetHubLayoutContainerService(_username, _authToken))
            {
                var hubLayoutContainer = service.GetHubLayoutContainer();

                Task.Factory.StartNew(() =>
                {
                    GetHubCollection(hubLayoutContainer.HubDetails);
                });
                return hubLayoutContainer;
            }
        }

        internal async Task<ComponentData> GetComponentData()
        {
            using (var service = new HubDataGetHubLayoutContainerService(_username, _authToken))
            {
                var cd = await service.GetComponentData();
                return cd;
            }
        }

        internal async Task<HubCollectionResults> GetHubCollection(List<HubDetails> hubDetails)
        {
            foreach (var hd in hubDetails)
            {
                //Check the Cache
                var ExistingKeys = CacheManager.GetAllExistingKeys(hd.HubId);
            }

            HubCollectionResults hubCollectionResults = null;

            using (var service = new HubDataGetHubCollection(_username, _authToken))
            {
                hubCollectionResults = await service.GetHubCollection(hubDetails);

                if (hubCollectionResults.Result != null)
                {
                    if (hubCollectionResults.Result.SummaryViews != null && hubCollectionResults.Result.SummaryViews.Count > 0)
                    {
                        foreach (var sv in hubCollectionResults.Result.SummaryViews)
                        {
                            CacheManager.Add(sv.Value, sv.Key, CacheManager.CacheType.Summary);
                        }
                    }

                    if (hubCollectionResults.Result.DetailViews != null && hubCollectionResults.Result.DetailViews.Count > 0)
                    {
                        foreach (var sv in hubCollectionResults.Result.DetailViews)
                        {
                            CacheManager.Add(sv.Value, sv.Key, CacheManager.CacheType.Details);
                        }
                    }
                }
            }
            return hubCollectionResults;
        }

        internal Dictionary<string, HubLayout> GetSummaryViewByReference(string viewReference)
        {
            //Check the cache
            var summaryView = CacheManager.Get<HubLayout>(viewReference, CacheManager.CacheType.Summary);
            Dictionary<string, HubLayout> objecToReturn = null;
            if (summaryView == null)
            {

            }
            else
            {
                objecToReturn = summaryView;
            }
            return objecToReturn;
        }

        internal Dictionary<string, HubDetailView> GetDetailsViewByReference(string viewReference)
        {
            //Check the cache
            var detailsView = CacheManager.Get<HubDetailView>(viewReference, CacheManager.CacheType.Details);
            Dictionary<string, HubDetailView> objecToReturn = null;
            if (detailsView == null)
            {

            }
            else
            {
                objecToReturn = detailsView;
            }
            return objecToReturn;
        }

        internal HubCollectionItem GetHubCollectionItem(string hubSchemaId)
        {
            HubCollectionItem hubCollectionItem = new HubCollectionItem();
            hubCollectionItem.HubSchemaId = hubSchemaId;
            hubCollectionItem.DetailViewId = CacheManager.GetViewReference(hubSchemaId, CacheManager.CacheType.Details);
            hubCollectionItem.SummaryViewId = CacheManager.GetViewReference(hubSchemaId, CacheManager.CacheType.Summary);
            return hubCollectionItem;
        }
    }
}