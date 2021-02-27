using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.DTOs
{
    public class DirectoryDto
    {
        public DirectoryDto()
        {
            Contacts = new HashSet<ContactDto>();
        }

        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ContactDto> Contacts { get; set; }
    }
}
