using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ResumeServices.Models
{
    [BsonIgnoreExtraElements]
    public class Resume
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        public string resume_id { get; set; } = String.Empty;
        public string person_name { get; set; } = String.Empty;
        public string phone_number { get; set; } = String.Empty;
        public string email { get; set; } = String.Empty;
        public List<Education> education { get; set; } = new List<Education> { };
        public List<Experience> experience { get; set; } = new List<Experience> { };
        public List<Project> project { get; set; } = new List<Project> { };
        public Skills skills { get; set; } = new Skills { };
        public string raw_resume { get; set; } = String.Empty;
        public string date_time { get; set; } = String.Empty;
    }
}
