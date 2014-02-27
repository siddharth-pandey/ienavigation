using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientPoC.Model
{
    public class BaseEntity : BaseQueryable
    {
        public String Id { get; set; }
        public Guid Etag { get; set; }
        public Int32 RevisionNumber { get; set; }
        public String LastUpdatedBy { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public Boolean IsNewObject { get; set; }
    }
}