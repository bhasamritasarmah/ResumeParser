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
        [BsonRepresentation(BsonType.ObjectId)]
        public string resume_id { get; set; } = String.Empty;
        public string person_name { get; set; } = String.Empty;
        public string phone_number { get; set; } = String.Empty;
        public string email { get; set; } = String.Empty;
        /*public Address address { get; set; } = new Address { };
        public Education education { get; set; } = new Education { };
        public Experience experience { get; set; } = new Experience { };
        public Project project { get; set; } = new Project { };
        public Skills skills { get; set; } = new Skills { };*/
    }
}
