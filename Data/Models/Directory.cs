using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Directory
    {
        public Directory()
        {
            Contacts = new HashSet<Contact>();
        }

        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
