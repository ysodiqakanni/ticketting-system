using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class UserVerification
    {
        public int Userid { get; set; }
        public bool IdProofVerified { get; set; }
        public bool ResidenceProofVerified { get; set; }
        public string IdProofFile { get; set; }
        public string ResidenceProofFile { get; set; }
        public int VerificationCount { get; set; }
    }
}
