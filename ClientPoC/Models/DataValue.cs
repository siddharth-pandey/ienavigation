using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPoC.Models
{
    public class DataValue
    {
        //Path that the value should be inserted to within the document hubschema.
        //  - eg: Referrals[5].Appointments[1].Date - this is the third row of Refferals collection, first row of Appointments collection and the component marked Date
        // - eg: Names[0].LastName
        // - eg: Title
        public String Path { get; set; }
        //Json encoded value generated from the component  
        public String Value { get; set; }
        //
        //This is the DataDictionaryId that this piece of data represents.
        //If null then its not linked to a DataDictionaryId, the value is local to this document only    
        public String DataDictionaryId { get; set; }
        public DateTimeOffset LastUpdatedDate { get; set; }
        public String LastUpdatedBy { get; set; }
    }
}
