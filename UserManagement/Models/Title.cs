using System;
using System.Collections.Generic;

namespace UserManagement.Models
{
    public partial class Title
    {
        public Title()
        {
            WebUsers = new HashSet<WebUser>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<WebUser> WebUsers { get; set; }
    }
}
