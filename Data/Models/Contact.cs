using System;
using System.Collections.Generic;

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

        public virtual Directory Person { get; set; }
    }
}
