using System;
using System.Collections.Generic;

namespace ClientPoC.Model
{
    public interface IComponentData
    {
        Guid InstanceId { get; set; }
        Guid ControlId { get; set; }
        Guid ParentId { get; set; }
        Boolean IsContainer { get; set; }
        String ComponentName { get; set; }
        String LinkedData { get; set; }
        Double Width { get; set; }
        Double Height { get; set; }
        Double Left { get; set; }
        Double Top { get; set; }
        Int32 ZIndex { get; set; }
        Boolean IsEnabled { get; set; }
        Boolean IsVisible { get; set; }
        Boolean IsMandatory { get; set; }
        String PropertyString { get; set; }
        List<IComponentData> Components { get; set; }
        String LocalProperty { get; set; }
        String Path { get; set; }
        String Id { get; set; }
        Guid Etag { get; set; }
        Int32 RevisionNumber { get; set; }
        String LastUpdatedBy { get; set; }
        DateTimeOffset LastUpdated { get; set; }
        Boolean IsNewObject { get; set; }
        Boolean Deleted { get; set; }
        void Initialise(string propertyString);
    }
}