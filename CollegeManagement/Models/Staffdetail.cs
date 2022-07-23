using System;
using System.Collections.Generic;

namespace CollegeManagement.Models
{
    public partial class Staffdetail
    {
        public int Id { get; set; }
        public string Staffname { get; set; } = null!;
        public string Stream { get; set; } = null!;
        public int Doj { get; set; }
        public string Address { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public int? Staffid { get; set; }

        public virtual User? Staff { get; set; }
    }
}
