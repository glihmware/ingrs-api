using System;
using System.Collections.Generic;

#nullable disable

namespace Ingrs.Access
{
    public partial class Domain
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Iid { get; set; }
        public string RsaPub { get; set; }
        public string EcdsaPub { get; set; }
    }
}
