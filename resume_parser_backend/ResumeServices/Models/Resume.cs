using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeServices.Models
{
    [BsonIgnoreExtraElements]
    public class Resume
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        public PersonName Name { get; set; } = new PersonName { };
        [BsonElement("resume_name")]
        public string ResumeName { get; set; } = String.Empty;
        [BsonElement("dob")]
        public DateOnly DateOfBirth { get; set; }
        [BsonElement("phone_numbers")]
        public List<string> PhoneNumbers { get; set; } = new List<string>();
        [BsonElement("email_ids")]
        public List<string> EmailIds { get; set; } = new List<string>();
        public List<Education> Qualifications { get; set; } = new List<Education> { };
        public List<Experience> Experiences { get; set; } = new List<Experience> { };
        public List<Project> Projects { get; set; } = new List<Project> { };
        public Skills Skills { get; set; } = new Skills();
    }
}
