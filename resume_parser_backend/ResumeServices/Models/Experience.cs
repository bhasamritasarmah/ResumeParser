using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeServices.Models
{
    public class Experience
    {
        [BsonElement("organisation_name")]
        public string OrganisationName { get; set; } = String.Empty;
        [BsonElement("designation")]
        public string Designation { get; set; } = String.Empty;
        [BsonElement("work_start_date")]
        public DateOnly StartDate { get; set; }
        [BsonElement("work_end_date")]
        public DateOnly EndDate { get; set;}
        [BsonElement("work_responsibilities")]
        public string RolesAndResponsibilities { get; set; } = String.Empty;
    }
}
