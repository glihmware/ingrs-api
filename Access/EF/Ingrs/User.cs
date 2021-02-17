using System;
using System.Collections.Generic;

#nullable disable

namespace Ingrs.Access
{
    public partial class User
    {
        public uint Id { get; set; }
        public string Iid { get; set; }
        public string Salt { get; set; }
        public string Argon22 { get; set; }
        public ulong? RegisterTs { get; set; }
        public uint? FailAttempts { get; set; }
        public ulong? LastSuccessCon { get; set; }
        public uint DomainId { get; set; }
    }
}
