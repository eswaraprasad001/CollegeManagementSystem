using System;
using System.Collections.Generic;

namespace CollegeManagement.Models
{
    public partial class Studentdetail
    {
        public int Id { get; set; }
        public string Studentname { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phoneno { get; set; } = null!;
        public int Batch { get; set; }
        public int? Studentid { get; set; }

        public virtual User? Student { get; set; }
    }
}
