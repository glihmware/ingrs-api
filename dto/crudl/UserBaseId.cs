using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ent = Ingrs.Access;

namespace Ingrs.Dto
{
    public partial class UserBase : IDto<Ent.User>
    {

        public string Argon22 { get; set; }
        public uint DomainId { get; set; }
        public uint? FailAttempts { get; set; }
        public string Iid { get; set; }
        public ulong? LastSuccessCon { get; set; }
        public ulong? RegisterTs { get; set; }
        public string Salt { get; set; }

        public UserBase()
        {

        }

        public UserBase(Ent.User e)
        {
            this.FromEntity(e);
        }

        public virtual Ent.User ToEntity()
        {
            return new Ent.User()
            {

                Argon22 = this.Argon22,
                DomainId = this.DomainId,
                FailAttempts = this.FailAttempts,
                Iid = this.Iid,
                LastSuccessCon = this.LastSuccessCon,
                RegisterTs = this.RegisterTs,
                Salt = this.Salt,
            };
        }

        public virtual void FromEntity(Ent.User e)
        {

            this.Argon22 = e.Argon22;
            this.DomainId = e.DomainId;
            this.FailAttempts = e.FailAttempts;
            this.Iid = e.Iid;
            this.LastSuccessCon = e.LastSuccessCon;
            this.RegisterTs = e.RegisterTs;
            this.Salt = e.Salt;
        }

        public static List<UserBase> BaseFromEntities(List<Ent.User> es)
        {
            var l = new List<UserBase>();
            foreach (Ent.User e in es)
            {
                var eb = new UserBase(e);
                l.Add(eb);
            }

            return l;
        }

    }



    public partial class UserId : UserBase, Ent.IIdentified
    {
        [Required]
        public uint Id { get; set; }

        public UserId()
        {

        }

        public UserId(Ent.User e)
        {
            this.FromEntity(e);
        }

        public override Ent.User ToEntity()
        {
            Ent.User e = base.ToEntity();
            e.Id = this.Id;
            return e;
        }

        public override void FromEntity(Ent.User e)
        {
            this.Id = e.Id;
            base.FromEntity(e);
        }

        public static List<UserId> IdFromEntities(List<Ent.User> es)
        {
            var l = new List<UserId>();
            foreach (Ent.User e in es)
            {
                var eid = new UserId(e);
                l.Add(eid);
            }

            return l;
        }

    }
}
