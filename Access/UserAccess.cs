using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Cryptool;

using Ingrs.Dto;

namespace Ingrs.Access
{
  public static class UserAccess
  {

    /// <summary>
    ///   Gets the Salt for the given user.
    /// </summary>
    /// <param name="Argon2Ud"></param>
    /// <returns></returns>
    public static async Task<string>
    GetSalt(string argon2UD)
    {
      User u = await UserAccess.GetByUD(argon2UD);

      if (u == null)
      {
        return null;
      }

      return u.Salt;
    }


    /// <summary>
    ///   Gets a user by it's Argon2UD.
    /// </summary>
    /// <param name="Ud"></param>
    /// <returns></returns>
    public static async Task<User>
    GetByUD(string argon2UD)
    {
      return await EFCtx.Ingrs.Users.FirstOrDefaultAsync(o => o.Argon2Ud == argon2UD);
    }


    /// <summary>
    ///   Gets a user by it's IID.
    /// </summary>
    /// <param name="Iid"></param>
    /// <returns></returns>
    public static async Task<User>
    GetByIID(string Iid)
    {
      return await EFCtx.Ingrs.Users.FirstOrDefaultAsync(o => o.Iid == Iid);
    }


    /// <summary>
    ///   Gets a user by it's credencials.
    /// </summary>
    /// <param name="domainId"></param>
    /// <param name="argon2UD"></param>
    /// <param name="argon22"></param>
    /// <returns></returns>
    public static async Task<User>
    GetByCred(uint domainId, string argon2UD, string argon22)
    {
      return await EFCtx.Ingrs.Users
        .FirstOrDefaultAsync(o => o.Argon2Ud == argon2UD && o.Argon22 == argon22 && o.DomainId == domainId);
    }


    /// <summary>
    ///   Updates user's last successfull connection date.
    /// </summary>
    /// <param name="u"></param>
    /// <returns></returns>
    public static async Task<bool>
    UpdateSuccessCon(UserId u)
    {
      u.LastSuccessCon = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds();

      ReasonCRUDL r = await CrudlAccess<User>.Update<UserId>(u);
      if (r != ReasonCRUDL.UPDATE)
      {
        return false;
      }

      return true;
    }
  }
}
