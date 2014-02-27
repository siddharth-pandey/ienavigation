using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using ClientPoC.Classes;
using ClientPoC.Model;
using ClientPoC.Models;
using ClientPoC.Models.Components.StandardContainers;
using ClientPoC.ViewModels.StandardControls;
using Glimpse.Core.ClientScript;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

namespace ClientPoC.ViewModels
{
    public static class ControlsContext
    {
        private static Dictionary<string, DefaultControlsViewModel> _dynamicViewModel;

        private static XDocument _controlsconfig;

        public static Dictionary<string, DefaultControlsViewModel> DynamicViewModel
        {
            get
            {
                return _dynamicViewModel;
            }
        }

        static ControlsContext()
        {
            Init();
        }

        private static void Init()
        {
            _dynamicViewModel = new Dictionary<string, DefaultControlsViewModel>();
            _controlsconfig = GetControlsConfiguration();
            //CreateDynamicViewModel(doc);
        }

        internal static XDocument GetControlsConfiguration()
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/ControlsConfig.xml");
            XDocument doc = XDocument.Load(path);
            return doc;
        }

        internal static DefaultControlsViewModel GetHomeIndexViewModel(ComponentData componentData)
        {
            var viewModel = new DefaultControlsViewModel();

            var controlViewModel = ProcessComponent(componentData,null);
            viewModel.Controls.Add(controlViewModel);

            return viewModel;
        }

        internal static HubLayoutViewModel GetHomeSummaryView(HubCollectionResults hubCollectionResults, string hubId)
        {
            HubCollectionItem hubCollectionItem = hubCollectionResults.Result.Items.FirstOrDefault(i => i.HubSchemaId.Equals(hubId));
            KeyValuePair<string, HubLayout> summaryView = new KeyValuePair<string, HubLayout>();
            HubLayoutViewModel hubLayoutViewModel = null;

            if (hubCollectionItem != null)
            {
                hubLayoutViewModel = new HubLayoutViewModel();
                var summaryViewId = hubCollectionItem.SummaryViewId;
                summaryView = hubCollectionResults.Result.SummaryViews.FirstOrDefault(sv => sv.Key.Equals(summaryViewId));

                hubLayoutViewModel.SummaryViewReference = summaryView.Key;
                hubLayoutViewModel.SummaryViewId = summaryView.Key;
                hubLayoutViewModel.DetailViewId = hubCollectionItem.DetailViewId;
                hubLayoutViewModel.EditViewId = hubCollectionItem.EditViewId;
                hubLayoutViewModel.HubSchemaId = hubCollectionItem.HubSchemaId;
                hubLayoutViewModel.BackgroundColour = summaryView.Value.BackgroundColour;
                hubLayoutViewModel.BorderColour = summaryView.Value.BorderColour;
                hubLayoutViewModel.BorderThickness = summaryView.Value.BorderThickness;
                hubLayoutViewModel.ViewHeight = summaryView.Value.Height;
                hubLayoutViewModel.Orientation = summaryView.Value.Orientation;
                hubLayoutViewModel.RotationLayouts = summaryView.Value.RotationLayouts;
                hubLayoutViewModel.ViewContexts = summaryView.Value.ViewContexts;
                hubLayoutViewModel.ViewDeviceTypes = summaryView.Value.ViewDeviceTypes;
                hubLayoutViewModel.ViewRoles = summaryView.Value.ViewRoles;
                hubLayoutViewModel.ViewWidth = summaryView.Value.Width;
                hubLayoutViewModel.Width = summaryView.Value.Width;
                hubLayoutViewModel.Height = summaryView.Value.Height;


                if (hubCollectionItem.Data != null && hubCollectionItem.Data.Values != null && hubCollectionItem.Data.Values.Count > 0)
                {
                    hubLayoutViewModel.DataValues = hubCollectionItem.Data.Values;
                }

                hubLayoutViewModel.Components = ProcessComponent(summaryView.Value.Components, hubLayoutViewModel.DataValues).ToList();
            }

            return hubLayoutViewModel;
        }

        internal static HubLayoutViewModel GetHomeSummaryView(Dictionary<string, HubLayout> summaryViews, string hubId, HubCollectionItem hubCollectionItem)
        {
            //HubCollectionItem hubCollectionItem = hubCollectionResults.Result.Items.FirstOrDefault(i => i.HubSchemaId.Equals(hubId));
            //KeyValuePair<string, HubLayout> summaryView = new KeyValuePair<string, HubLayout>();
            HubLayoutViewModel hubLayoutViewModel = null;

            //if (hubCollectionItem != null)
            //{

            //var summaryViewId = hubCollectionItem.SummaryViewId;
            //summaryView = hubCollectionResults.Result.SummaryViews.FirstOrDefault(sv => sv.Key.Equals(summaryViewId));
            foreach (var summaryView in summaryViews)
            {
                hubLayoutViewModel = new HubLayoutViewModel();
                hubLayoutViewModel.SummaryViewReference = summaryView.Key;
                hubLayoutViewModel.SummaryViewId = summaryView.Key;
                hubLayoutViewModel.DetailViewId = hubCollectionItem.DetailViewId;
                hubLayoutViewModel.EditViewId = hubCollectionItem.EditViewId;
                hubLayoutViewModel.HubSchemaId = hubCollectionItem.HubSchemaId;
                hubLayoutViewModel.BackgroundColour = summaryView.Value.BackgroundColour;
                hubLayoutViewModel.BorderColour = summaryView.Value.BorderColour;
                hubLayoutViewModel.BorderThickness = summaryView.Value.BorderThickness;
                hubLayoutViewModel.ViewHeight = summaryView.Value.Height;
                hubLayoutViewModel.Orientation = summaryView.Value.Orientation;
                hubLayoutViewModel.RotationLayouts = summaryView.Value.RotationLayouts;
                hubLayoutViewModel.ViewContexts = summaryView.Value.ViewContexts;
                hubLayoutViewModel.ViewDeviceTypes = summaryView.Value.ViewDeviceTypes;
                hubLayoutViewModel.ViewRoles = summaryView.Value.ViewRoles;
                hubLayoutViewModel.ViewWidth = summaryView.Value.Width;

                if (summaryView.Value.Components != null && summaryView.Value.Components.Count > 0)
                {
                    hubLayoutViewModel.Components = new List<ComponentData>();
                    foreach (var comp in summaryView.Value.Components)
                    {
                        var cType = GetControlType(comp.ControlId);
                        if (string.IsNullOrEmpty(cType))
                        {
                            continue;
                        }
                        var compData = ConstructComponent(comp, cType);
                        hubLayoutViewModel.Components.Add(compData);
                    }
                }
            }

            //}

            //summary view model
            return hubLayoutViewModel;
        }

        internal static ComponentData FillComponentData(ComponentData controlViewModel, ComponentData component)
        {
            controlViewModel.InstanceId = component.InstanceId;
            controlViewModel.ControlId = component.ControlId;
            controlViewModel.ParentId = component.ParentId;
            controlViewModel.IsContainer = component.IsContainer;
            controlViewModel.ComponentName = component.ComponentName;
            controlViewModel.LinkedData = component.LinkedData;
            controlViewModel.Width = component.Width;
            controlViewModel.Height = component.Height;
            controlViewModel.Left = component.Left;
            controlViewModel.Top = component.Top;
            controlViewModel.ZIndex = component.ZIndex;
            controlViewModel.IsEnabled = component.IsEnabled;
            controlViewModel.IsMandatory = component.IsMandatory;
            controlViewModel.IsVisible = component.IsVisible;
            controlViewModel.PropertyString = component.PropertyString;
            controlViewModel.LocalProperty = component.LocalProperty;
            controlViewModel.Path = component.Path;

            return controlViewModel;
        }



        internal static DefaultControlsViewModel GetDetailsViewModel(Dictionary<string, HubDetailView> detailsview, IEnumerable<DataValue> dataValues)
        {

            HubDetailView hubDetailsView = null;
            DefaultControlsViewModel defaultViewModel = new DefaultControlsViewModel();
            DetailsViewModel detailsViewModel = null;

            if (detailsview != null && detailsview.Count > 0)
            {
                foreach (var hdv in detailsview)
                {
                    var components = ProcessComponent(hdv.Value.Components, dataValues).ToList();
                    defaultViewModel.Controls = components;
                }
            }
            return defaultViewModel;
        }

        private static dynamic GetControlType(Guid controlId)
        {
            if (ControlsContext._controlsconfig.Root != null)
            {
                var firstOrDefault = (ControlsContext._controlsconfig.Root.Elements("type")
                    .Where(e => (string)e.Attribute("value") == controlId.ToString().ToUpper())).FirstOrDefault();
                if (firstOrDefault != null)
                {
                    var result = firstOrDefault.Attribute("name").Value;
                    return result;
                }
            }
            return null;

        }

        private static ComponentData BuildControl(string type, ComponentData comp)
        {
            Object[] args = { comp };
            Type tempType = Type.GetType("ClientPoC.ViewModels.StandardControls." + type + "ViewModel");
            var obj = Activator.CreateInstance(tempType);

            return obj as ComponentData;
        }

        private static ComponentData ProcessComponent(ComponentData componentData, IEnumerable<DataValue> dataValues)
        {
            ComponentData newControlViewModel = null;

            newControlViewModel = ConstructComponent(componentData, dataValues);

            if (componentData.Components != null && componentData.Components.Count > 0)
            {
                var components = ProcessComponent(componentData.Components, dataValues).ToList();
                newControlViewModel.Components = components;
            }

            return newControlViewModel;
        }

        private static IEnumerable<ComponentData> ProcessComponent(IEnumerable<ComponentData> components, IEnumerable<DataValue> dataValues )
        {
            List<ComponentData> componentData = new List<ComponentData>();
            foreach (var comp in components)
            {
                try
                {
                    ComponentData compData = ProcessComponent(comp, dataValues);
                    componentData.Add(compData);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return componentData;
        }

        private static ComponentData ConstructComponent(ComponentData componentData, IEnumerable<DataValue> dataValue)
        {
            try
            {
                var controlType = GetControlType(componentData.ControlId);
                if (string.IsNullOrEmpty(controlType))
                {
                    throw new Exception("Null Control");
                }

                ComponentData newControlViewModel = BuildControl(controlType, componentData);
                newControlViewModel.FeedDefaultData(componentData);
                newControlViewModel.FeedData(dataValue);

                return newControlViewModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception();
            }
        }
    }
}