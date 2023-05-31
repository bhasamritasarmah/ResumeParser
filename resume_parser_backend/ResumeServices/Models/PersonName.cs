using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeServices.Models
{
    public class PersonName
    {
        [BsonElement("first_name")]
        public string FirstName { get; set; } = String.Empty;
        [BsonElement("last_name")]
        public string LastName { get; set; } = String.Empty;
    }
}
