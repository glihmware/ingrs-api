
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

using Ingrs.Dto;
using Ingrs.Access;
using Ef = Ingrs.Access;
using Ent = Ingrs.Access;

namespace Ingrs.Access
{


  public static partial class CrudlAccess<T>
    where T : Ent.Entity, Ent.IIdentified
    {

            public static Dictionary<string, object> _sets { get; set; }
        = new Dictionary<string, object>()
        {

            { "Ingrs.Access.Domain", Ef.EFCtx.Ingrs.Domains },
            { "Ingrs.Access.Op", Ef.EFCtx.Ingrs.Ops },
            { "Ingrs.Access.User", Ef.EFCtx.Ingrs.Users },
        };


      public static async Task<List<I>>
        List<I>()
        where I : Ent.IIdentified, IDto<T>
      {
        List<T> l = await Ef.CRUDL.List((DbSet<T>)_sets[typeof(T).ToString()]);
        return (List<I>)typeof(I)
               .GetMethod("IdFromEntities")
               .Invoke(null, new object[] {l});
      }

        public static async Task<I>
        Read<I>(uint id)
          where I : Ent.IIdentified, IDto<T>, new()
        {
            T e = await Ef.CRUDL.Read((DbSet<T>)_sets[typeof(T).ToString()], id);

            if (e == null)
            {
                return default(I);
            }

            I i = new I();
            i.FromEntity(e);
            return i;
        }

        public static async Task<Ef.ReasonCRUDL>
        Create(T e)
        {
            return await Ef.CRUDL.Create((DbSet<T>)_sets[typeof(T).ToString()], e);
        }

        public static async Task<Ef.ReasonCRUDL>
        Update<I>(I eid, string[] fieldsToExclude = null)
          where I : Ent.IIdentified, IDto<T>
        {
            T e = eid.ToEntity();
            return await Ef.CRUDL.Update((DbSet<T>)_sets[typeof(T).ToString()], e, fieldsToExclude);
        }

        public static async Task<Ef.ReasonCRUDL>
        Delete(uint id)
        {
            return await Ef.CRUDL.Delete((DbSet<T>)_sets[typeof(T).ToString()], id);
        }
    }
}


