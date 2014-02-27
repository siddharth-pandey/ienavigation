using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientPoC.Model;
using ClientPoC.Models;
using ClientPoC.ViewModels;
using Newtonsoft.Json;

namespace ClientPoC.ViewModels.StandardControls
{
    public class FixedContainerViewModel : ComponentData
    {
        public FixedContainerViewModel()
        { }

        //public FixedContainerViewModel(ComponentData component)
        //{
        //    var fixedContainer = JsonConvert.DeserializeObject<FixedContainerViewModel>(component.PropertyString);
        //    MinimumHeight = 10;
        //    Height = 200;
        //    MinimumWidth = 10;
        //    Width = 200;
        //    Title = fixedContainer.Title;
        //    ContainerName = fixedContainer.ContainerName;
        //    BorderColour = fixedContainer.BorderColour;
        //    BorderWeight = fixedContainer.BorderWeight;
        //    BackgroundColour = fixedContainer.BackgroundColour;
        //    IncludeTitleBar = fixedContainer.IncludeTitleBar;
        //    MinimumHeight = fixedContainer.MinimumHeight;
        //    Height = component.Height;
        //    MinimumWidth = fixedContainer.MinimumWidth;
        //    Width = component.Width;
        //    if (component.Components != null && component.Components.Count > 0)
        //    {
        //        Components = new List<ComponentData>();
        //        foreach (var comp in component.Components)
        //        {
        //            var controlType = ControlsContext.GetControlType(comp.ControlId);
        //            if (string.IsNullOrEmpty(controlType))
        //            {
        //                continue;
        //            }
        //            var newControl = ControlsContext.BuildControl(controlType, comp);
        //            var newControlViewModel = ControlsContext.FillComponentData(newControl, comp);
        //            Components.Add(newControlViewModel);
        //        }
        //    }
        //}

        public override void FeedDefaultData(ComponentData componentData)
        {
            base.FeedDefaultData(componentData);

            var fixedContainer = JsonConvert.DeserializeObject<FixedContainerViewModel>(componentData.PropertyString);
            MinimumHeight = 10;
            Height = 200;
            MinimumWidth = 10;
            Width = 200;
            Title = fixedContainer.Title;
            ContainerName = fixedContainer.ContainerName;
            BorderColour = fixedContainer.BorderColour;
            BorderWeight = fixedContainer.BorderWeight;
            BackgroundColour = fixedContainer.BackgroundColour;
            IncludeTitleBar = fixedContainer.IncludeTitleBar;
            MinimumHeight = fixedContainer.MinimumHeight;
            Height = componentData.Height;
            MinimumWidth = fixedContainer.MinimumWidth;
            Width = componentData.Width;
        }

        public new string Type
        {
            get { return "FixedContainer"; }
        }

        public double MinimumHeight { get; set; }
        public new double Height { get; set; }
        public new double Width { get; set; }
        public double MinimumWidth { get; set; }
        public string ContainerName { get; set; }
        public string BorderColour { get; set; }
        public int BorderWeight { get; set; }
        public string BackgroundColour { get; set; }
        public string IncludeTitleBar { get; set; }
        public string Title { get; set; }

    }
}