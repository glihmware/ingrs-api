using System;
using System.Collections.Generic;

#nullable disable

namespace Ingrs.Access
{
    public partial class Op
    {
        public uint Id { get; set; }
        public string Tiid { get; set; }
        public ulong RandNum { get; set; }
        public ulong GenerateTs { get; set; }
        public byte? Type { get; set; }
        public uint DomainId { get; set; }
    }
}
