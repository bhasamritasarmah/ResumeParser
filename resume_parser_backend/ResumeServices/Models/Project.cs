using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeServices.Models
{
    public class Project
    {
        [BsonElement("project_name")]
        public string ProjectName { get; set; } = String.Empty;
        [BsonElement("project_start_date")]
        public DateOnly StartDate { get; set; }
        [BsonElement("project_end_date")]
        public DateOnly EndDate { get; set;}
        [BsonElement("project_roles")]
        public string RolesAndResponsibilities { get; set; } = String.Empty;
    }
}
