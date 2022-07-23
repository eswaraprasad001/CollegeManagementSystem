using System;
using System.Collections.Generic;

namespace CollegeManagement.Models
{
    public partial class User
    {
        public User()
        {
            Staffdetails = new HashSet<Staffdetail>();
            Studentdetails = new HashSet<Studentdetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public sbyte Isadmin { get; set; }
        public string Password { get; set; } = null!;
        public sbyte Status { get; set; }

        public virtual ICollection<Staffdetail> Staffdetails { get; set; }
        public virtual ICollection<Studentdetail> Studentdetails { get; set; }
    }
}
