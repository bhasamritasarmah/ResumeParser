using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeServices.Models
{
    public class Skills
    {
        [BsonElement("technical_skills")]
        public List<string> TechnicalSkills { get; set; } = new List<string> { };
        [BsonElement("soft_skills")]
        public List<string> SoftSkills { get; set; } = new List<string> { };
        [BsonElement("management_skills")]
        public List<string> ManagementSkills { get; set; } = new List<string> { };
        [BsonElement("hr_skills")]
        public List<string> HRSkills { get; set; } = new List<string> { };
        [BsonElement("other_skills")]
        public List<string> OtherSkills { get; set; } = new List<string> { };
    }
}
