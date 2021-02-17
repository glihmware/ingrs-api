using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ent = Ingrs.Access;

namespace Ingrs.Dto
{
    public partial class OpBase : IDto<Ent.Op>
    {

        public uint DomainId { get; set; }
        public ulong? GenerateTs { get; set; }
        public ulong RandNum { get; set; }
        public string Tiid { get; set; }
        public byte? Type { get; set; }

        public OpBase()
        {

        }

        public OpBase(Ent.Op e)
        {
            this.FromEntity(e);
        }

        public virtual Ent.Op ToEntity()
        {
            return new Ent.Op()
            {

                DomainId = this.DomainId,
                GenerateTs = this.GenerateTs,
                RandNum = this.RandNum,
                Tiid = this.Tiid,
                Type = this.Type,
            };
        }

        public virtual void FromEntity(Ent.Op e)
        {

            this.DomainId = e.DomainId;
            this.GenerateTs = e.GenerateTs;
            this.RandNum = e.RandNum;
            this.Tiid = e.Tiid;
            this.Type = e.Type;
        }

        public static List<OpBase> BaseFromEntities(List<Ent.Op> es)
        {
            var l = new List<OpBase>();
            foreach (Ent.Op e in es)
            {
                var eb = new OpBase(e);
                l.Add(eb);
            }

            return l;
        }

    }



    public partial class OpId : OpBase, Ent.IIdentified
    {
        [Required]
        public uint Id { get; set; }

        public OpId()
        {

        }

        public OpId(Ent.Op e)
        {
            this.FromEntity(e);
        }

        public override Ent.Op ToEntity()
        {
            Ent.Op e = base.ToEntity();
            e.Id = this.Id;
            return e;
        }

        public override void FromEntity(Ent.Op e)
        {
            this.Id = e.Id;
            base.FromEntity(e);
        }

        public static List<OpId> IdFromEntities(List<Ent.Op> es)
        {
            var l = new List<OpId>();
            foreach (Ent.Op e in es)
            {
                var eid = new OpId(e);
                l.Add(eid);
            }

            return l;
        }

    }
}
