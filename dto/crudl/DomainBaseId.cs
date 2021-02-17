using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ent = Ingrs.Access;

namespace Ingrs.Dto
{
    public partial class DomainBase : IDto<Ent.Domain>
    {

        public string EcdsaPub { get; set; }
        public string Iid { get; set; }
        public string Name { get; set; }
        public string RsaPub { get; set; }

        public DomainBase()
        {

        }

        public DomainBase(Ent.Domain e)
        {
            this.FromEntity(e);
        }

        public virtual Ent.Domain ToEntity()
        {
            return new Ent.Domain()
            {

                EcdsaPub = this.EcdsaPub,
                Iid = this.Iid,
                Name = this.Name,
                RsaPub = this.RsaPub,
            };
        }

        public virtual void FromEntity(Ent.Domain e)
        {

            this.EcdsaPub = e.EcdsaPub;
            this.Iid = e.Iid;
            this.Name = e.Name;
            this.RsaPub = e.RsaPub;
        }

        public static List<DomainBase> BaseFromEntities(List<Ent.Domain> es)
        {
            var l = new List<DomainBase>();
            foreach (Ent.Domain e in es)
            {
                var eb = new DomainBase(e);
                l.Add(eb);
            }

            return l;
        }

    }



    public partial class DomainId : DomainBase, Ent.IIdentified
    {
        [Required]
        public uint Id { get; set; }

        public DomainId()
        {

        }

        public DomainId(Ent.Domain e)
        {
            this.FromEntity(e);
        }

        public override Ent.Domain ToEntity()
        {
            Ent.Domain e = base.ToEntity();
            e.Id = this.Id;
            return e;
        }

        public override void FromEntity(Ent.Domain e)
        {
            this.Id = e.Id;
            base.FromEntity(e);
        }

        public static List<DomainId> IdFromEntities(List<Ent.Domain> es)
        {
            var l = new List<DomainId>();
            foreach (Ent.Domain e in es)
            {
                var eid = new DomainId(e);
                l.Add(eid);
            }

            return l;
        }

    }
}
