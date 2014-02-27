using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using ClientPoC.Model;
using ClientPoC.Models;
using ClientPoC.Models.Components.StandardContainers;
using Newtonsoft.Json;

namespace ClientPoC.ViewModels.StandardControls
{
    public class HubLayoutContainerViewModel : HubLayoutContainer
    {
        private int _containerWidth;
        private int _containerHeight;

        public HubLayoutContainerViewModel()
        { }

        //public HubLayoutContainerViewModel(ComponentData component)
        //{
        //    var hubLayoutContainer = JsonConvert.DeserializeObject<HubLayoutContainer>(component.PropertyString);
        //    UseFullWidth = hubLayoutContainer.UseFullWidth;
        //    AllowFlow = hubLayoutContainer.AllowFlow;
        //    ContainerWidth = hubLayoutContainer.ContainerWidth;
        //    ContainerHeight = hubLayoutContainer.ContainerHeight;
        //    Debug.WriteLine(this.GetType());
            
        //    if (hubLayoutContainer != null && !string.IsNullOrEmpty(hubLayoutContainer.HubDetailsStr))
        //    {
        //        var hubDetails = JsonConvert.DeserializeObject<List<HubDetails>>(hubLayoutContainer.HubDetailsStr);
        //        if (hubDetails != null && hubDetails.Count > 0)
        //        {
        //            HubDetails = new List<HubDetails>();
        //            HubDetails = hubDetails;
        //        }
        //    }
        //}

        public override void FeedDefaultData(ComponentData componentData)
        {
            base.FeedDefaultData(componentData);

            var hubLayoutContainer = JsonConvert.DeserializeObject<HubLayoutContainer>(componentData.PropertyString);
            UseFullWidth = hubLayoutContainer.UseFullWidth;
            AllowFlow = hubLayoutContainer.AllowFlow;
            ContainerWidth = hubLayoutContainer.ContainerWidth;
            ContainerHeight = hubLayoutContainer.ContainerHeight;
            Debug.WriteLine(this.GetType());

            if (hubLayoutContainer != null && !string.IsNullOrEmpty(hubLayoutContainer.HubDetailsStr))
            {
                var hubDetails = JsonConvert.DeserializeObject<List<HubDetails>>(hubLayoutContainer.HubDetailsStr);
                if (hubDetails != null && hubDetails.Count > 0)
                {
                    HubDetails = new List<HubDetails>();
                    HubDetails = hubDetails;
                }
            }
        }

        public new string Type
        {
            get { return "HubLayoutContainer"; }
        }
        public new int ContainerWidth
        {
            get { return _containerWidth; }
            set { _containerWidth = CalculateWidth(value); }
        }

        public new int ContainerHeight
        {
            get { return _containerHeight; }
            set { _containerHeight = CalculateHeight(value); }
        }

        private int CalculateWidth(int dimension)
        {
            return CalculateLayout(dimension);
        }

        private int CalculateHeight(int dimension)
        {
            return CalculateLayout(dimension);
        }

        private int CalculateLayout(int dimension)
        {
            return (dimension * 100) + ((dimension - 1) * 5);
        }

        public bool IsAsyncEnabled
        {
            get
            {
                bool result = false;
                var configValue = ConfigurationManager.AppSettings["AsyncEnabled"];

                return configValue.ToLower().Equals("true", StringComparison.CurrentCulture);
            }
        }
    }
}