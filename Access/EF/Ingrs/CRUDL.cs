
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

using Ingrs.Access;

namespace Ingrs.Access
{

  public enum ReasonCRUDL
    {
      CREATE,
      UPDATE,
      DELETE,
      DUPLICATE,
      FK_CONSTRAINT,
      NOT_FOUND,
      ERROR,
    }

    public static class CRUDL
    {
        public static async Task<List<T>>
        List<T>(DbSet<T> t)
        where T : class
        {
            return await t.ToListAsync();
        }

        public static async Task<T>
        Read<T>(DbSet<T> t, uint id)
        where T : class
        {
            return await t.FindAsync(id);
        }

        public static async Task<ReasonCRUDL>
        Create<T>(DbSet<T> t, T obj)
        where T : class
        {
            t.Add(obj);

            int save = 0;

            try
            {
                save = await EFCtx.Ingrs.SaveChangesAsync();
            }
            catch (Exception ex)
            {
              t.Remove(obj);

              // Console.WriteLine(ex);
              if (ex.InnerException != null
                  && ex.InnerException.Message.Contains("Duplicate entry"))
              {
                return ReasonCRUDL.DUPLICATE;
              }
              return ReasonCRUDL.ERROR;
            }

          if (save != 1)
          {
            return ReasonCRUDL.ERROR;
          }

          return ReasonCRUDL.CREATE;
        }

        public static async Task<ReasonCRUDL>
        Update<T>(DbSet<T> t, T obj, string[] fieldsToExclude = null)
        where T : class
        {
            T entityTracked = await Read(t, ((IIdentified)obj).Id);

            if (entityTracked == null)
            {
                return ReasonCRUDL.NOT_FOUND;
            }

            fieldsToExclude ??= new string[] { };

            foreach (var p in typeof(T).GetProperties())
            {
                if (Array.Exists(fieldsToExclude, el => el == p.Name))
                {
                    continue;
                }

                if (p.GetMethod.IsVirtual)
                {
                    continue;
                }

                p.SetValue(entityTracked, p.GetValue(obj));
            }

          int save;
          try
          {
            save = await EFCtx.Ingrs.SaveChangesAsync();
          }
          catch (Exception)
          {
            // Console.WriteLine( eeee {ex}");
            return ReasonCRUDL.ERROR;
          }

          if (save != 1)
          {
            return ReasonCRUDL.ERROR;
          }

          return ReasonCRUDL.UPDATE;
        }

        public static async Task<ReasonCRUDL>
        Delete<T>(DbSet<T> t, uint id) where T : class
        {
            T entity = await t.FindAsync(id);

            if (entity == null)
            {
                return ReasonCRUDL.NOT_FOUND;
            }

            try
            {
                t.Remove(entity);
                await EFCtx.Ingrs.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Console.WriteLine(ex);
                return ReasonCRUDL.FK_CONSTRAINT;
            }

            return ReasonCRUDL.DELETE;
        }
    }

}

