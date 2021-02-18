using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Cryptool;

using Ingrs.Dto;

namespace Ingrs.Access
{

  /// <summary>
  ///
  /// </summary>
  public static class OpAccess
  {
    /// <summary>
    ///   Returns the operation bound to the given TIID.
    /// </summary>
    /// <param name="opTIID"></param>
    /// <returns></returns>
    public static async Task<OpId>
    GetByTIID(string opTIID)
    {
      Op op = await EFCtx.Ingrs.Ops.FirstOrDefaultAsync(o => o.Tiid == opTIID);
      return new OpId(op);
    }
  }
}
