using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.DTOs
{
    public class ContactDto
    {
        public int Id { get; set; }
        public Guid PersonId { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }

        public virtual DirectoryDto Person { get; set; }
    }
}
