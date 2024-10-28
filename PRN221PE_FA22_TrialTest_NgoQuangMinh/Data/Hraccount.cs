using System;
using System.Collections.Generic;

namespace PRN221PE_FA22_TrialTest_NgoQuangMinh.Data
{
    public partial class Hraccount
    {
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public int? MemberRole { get; set; }
    }
}
