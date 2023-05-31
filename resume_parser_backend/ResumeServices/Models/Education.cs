using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeServices.Models
{
    public class Education
    {
        [BsonElement("institute_name")]
        public string InstituteName { get; set; } = String.Empty;
        [BsonElement("degree")]
        public string Degree { get; set; } = String.Empty;
        [BsonElement("specialisation")]
        public string Specialisation { get; set; } = String.Empty;
        [BsonElement("start_date")]
        public DateOnly StartDate { get; set; }
        [BsonElement("end_date")]
        public DateOnly EndDate { get; set;}
    }
}
