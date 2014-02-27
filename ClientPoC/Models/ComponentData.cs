using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientPoC.Models;

namespace ClientPoC.Model
{
    public class ComponentData : BaseEntity
    {
        public Guid InstanceId { get; set; }
        public Guid ControlId { get; set; }
        public Guid ParentId { get; set; }
        public Boolean IsContainer { get; set; }
        public String ComponentName { get; set; }
        public String LinkedData { get; set; }
        public Double Width { get; set; }
        public Double Height { get; set; }
        public Double Left { get; set; }
        public Double Top { get; set; }
        public Int32 ZIndex { get; set; }
        public Boolean IsEnabled { get; set; }
        public Boolean IsVisible { get; set; }
        public Boolean IsMandatory { get; set; }
        public String PropertyString { get; set; }
        public List<ComponentData> Components { get; set; }
        public String LocalProperty { get; set; }
        public String Path { get; set; }

        public virtual void FeedData(IEnumerable<DataValue> dataValues)
        {

        }

        public virtual void FeedDefaultData(ComponentData componentData)
        {
            InstanceId = componentData.InstanceId;
            ControlId = componentData.ControlId;
            ParentId = componentData.ParentId;
            IsContainer = componentData.IsContainer;
            ComponentName = componentData.ComponentName;
            LinkedData = componentData.LinkedData;
            Width = componentData.Width;
            Height = componentData.Height;
            Left = componentData.Left;
            Top = componentData.Top;
            ZIndex = componentData.ZIndex;
            IsEnabled = componentData.IsEnabled;
            IsMandatory = componentData.IsMandatory;
            IsVisible = componentData.IsVisible;
            PropertyString = componentData.PropertyString;
            LocalProperty = componentData.LocalProperty;
            Path = componentData.Path;
            Components = componentData.Components;
        }
    }
}
