using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientPoC.Model;

namespace ClientPoC.Models
{
    public class HubData : BaseEntity
    {
        //The Id of the HubSchema item that was used when generating this document  
        public String HubId { get; set; }
        //The Revision Number of the HubSchema that was used when saving this data. 
        public Int32 Version { get; set; }
        //The Id of the item in context (eg Patient)    
        public String ContextId { get; set; }
        //Id (Guid) of the context type, eg Patient    
        public String ContextTypeId { get; set; }
        //Id from an external system if this data document is imported in from a 3rd party system  
        public String ExternalId { get; set; }
        //A string representation of minimum context data set (eg "Surname:Bloggs, Forename: John")    
        public String ContextSummary { get; set; }
        //This means that currently this document is read only - this could be different from the value specified against the schema. Only relevant if the schema Readonly is set to false. 
        public Boolean ReadOnly { get; set; }
        //This flag indicates that this document is now completed and can no longer be edited. This flag should be set at the same time as the DateCompleted property. 
        public Boolean IsCompleted { get; set; }
        //This is set when the document has been completed and can no longer be edited. 
        public DateTimeOffset DateCompleted { get; set; }
        //This is set when the document has been created - should be set by the client when they first save locally.    
        public DateTimeOffset DateCreated { get; set; }
        //The user who created this document - should be set by the server on first save  
        public String CreatedBy { get; set; }
        //This is the collection of values stored against the HubSchema   
        public List<DataValue> Values { get; set; }
    }
}
