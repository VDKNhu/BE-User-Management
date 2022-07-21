using System;
using System.Collections.Generic;

namespace UserManagement.Models
{
    public partial class WebUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public short? Gender { get; set; }
        public string? Company { get; set; }
        public int? Title { get; set; }
        public string Email { get; set; } = null!;
        public string? Avatar { get; set; }

        public virtual Title? TitleNavigation { get; set; }
    }
}
