using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ClientPoC.Model;
using ClientPoC.Services;
using ClientPoC.ViewModels;
using ClientPoC.Models;


namespace ClientPoC.Controllers
{
    
    public class HomeController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _username = Session[Constants.Constants.AuthSessionUsername] as string;
            _authToken = Session[Constants.Constants.AuthSessionTokenKey] as string;
        }

        private static string _username;
        private static string _authToken;
        public async Task<ActionResult> Index()
        {
            ComponentData componentData = new ComponentData();
            using (var service = new ServiceWrapper(_username, _authToken))
            {
                //service.GetHubLayoutContainer();
                componentData = await service.GetComponentData();
            }

            var viewModel = ControlsContext.GetHomeIndexViewModel(componentData);
            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<PartialViewResult> Summary(HubDetails hubDetails)
        {
            HubLayoutViewModel viewModel = null;

            HubCollectionResults hubCollectionResults;

            List<HubDetails> tempHubDetails = new List<HubDetails>();
            tempHubDetails.Add(hubDetails);
            Dictionary<string, HubLayout> summaryView = null;
            HubCollectionItem hubCollectionItem;
            using (var service = new ServiceWrapper(_username, _authToken))
            {
                hubCollectionResults = await service.GetHubCollection(tempHubDetails);

                //summaryView = service.GetSummaryViewByReference(hubDetails.HubId);
                //hubCollectionItem = service.GetHubCollectionItem(hubDetails.HubId);
            }

            if (hubCollectionResults != null)
            {
                viewModel = ControlsContext.GetHomeSummaryView(hubCollectionResults, hubDetails.HubId);
            }

            //if (summaryView != null)
            //{
            //    viewModel = ControlsContext.GetHomeSummaryView(summaryView, hubDetails.HubId, hubCollectionItem);
            //}
            return PartialView("DisplayTemplates/HubLayoutViewModel", viewModel);
        }

        [HttpPost]
        public ActionResult Details(HubLayoutViewModel viewModel, string Reference, string Width, string Height, string DetailViewId)
        {
            using (var service = new ServiceWrapper(_username, _authToken))
            {
                Dictionary<string, HubDetailView> detailsView = null;

                detailsView = service.GetDetailsViewByReference(viewModel.DetailViewId);
                var hubdetailviewmodel = ControlsContext.GetDetailsViewModel(detailsView, viewModel.DataValues);

                ViewBag.Message = Reference + " " + Width + " " + Height + " " + DetailViewId;


                return PartialView(hubdetailviewmodel);
            }
        }

        [HttpGet]
        public async Task<ViewResult> Details(string detailViewReference, string width, string height)
        {
            using (var service = new ServiceWrapper(_username, _authToken))
            {
                Dictionary<string, HubDetailView> detailsView = null;
                List<DataValue> dataValues = null;
                HubCollectionItem hubCollectionItem = null;
                if (!string.IsNullOrEmpty(detailViewReference))
                {
                    detailViewReference = detailViewReference.Replace("P", "/");
                    int layoutWidth = 0, layoutHeight = 0;
                    if (!string.IsNullOrEmpty(width))
                    {
                        int.TryParse(width, out layoutWidth);
                    }
                    if (!string.IsNullOrEmpty(height))
                    {
                        int.TryParse(height, out layoutHeight);
                    }

                    var hubDetail = new HubDetails();
                    hubDetail.HubId = detailViewReference;
                    hubDetail.LayoutWidth = layoutWidth;
                    hubDetail.LayoutHeight = layoutHeight;

                    var hubDetailsCollection = new List<HubDetails>();
                    hubDetailsCollection.Add(hubDetail);

                    HubCollectionResults hubCollectionResults = await service.GetHubCollection(hubDetailsCollection);

                    if (hubCollectionResults != null)
                    {
                        hubCollectionItem = hubCollectionResults.Result.Items.FirstOrDefault(x => x.DetailViewId.Equals(detailViewReference));
                        if (hubCollectionItem != null && hubCollectionItem.Data != null && hubCollectionItem.Data.Values != null && hubCollectionItem.Data.Values.Count > 0)
                        {
                            dataValues = hubCollectionItem.Data.Values;
                        }
                    }

                    detailsView = service.GetDetailsViewByReference(detailViewReference);

                }

                var hubdetailviewmodel = ControlsContext.GetDetailsViewModel(detailsView, dataValues);
                return View(hubdetailviewmodel);
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(string Reference, string Width, string Height, string DetailViewId)
        {
            ViewBag.Message = Reference + " " + Width + " " + Height + " " + DetailViewId;
            return View();
        }
    }
}