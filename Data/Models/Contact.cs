using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace Data.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public Guid PersonId { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Directory Person { get; set; }
    }
}
