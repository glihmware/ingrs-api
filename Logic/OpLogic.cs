using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Cryptool;

using Ingrs.Services;
using Ingrs.Access;
using Ingrs.Dto;

namespace Ingrs.Logic
{

  /// <summary>
  ///   
  /// </summary>
  public enum OpType
    {
      Register,
      Verify,
      Update,
      Delete,

    }

  /// <summary>
  ///   
  /// </summary>
  public static class OpLogic
  {
    /// <summary>
    ///   Define a time that the operation still valid.
    /// </summary>
    private static readonly double __opLiveSpanSeconds = 150;

    /// <summary>
    ///   Runs defined actions when the op is
    ///   considered as finished.
    /// </summary>
    /// <param name="op"></param>
    /// <returns></returns>
    public static async Task
    Terminate(OpId op)
    {
      ReasonCRUDL r = await CrudlAccess<Op>.Delete(op.Id);
      if (r != ReasonCRUDL.DELETE)
      {
        Console.WriteLine("fail delete op?");
        // throw? should not happen?
      }
    }



    /// <summary>
    ///   Verifies if the Op can be executed.
    /// </summary>
    /// <param name="op"></param>
    /// <param name="randPlusOne"></param>
    /// <param name="usecase"></param>
    /// <returns></returns>
    public static async Task<bool>
    Verify(OpId op, ulong randPlusOne, OpType type, uint domainId)
    {
      // Expiration.
      ulong liveMinutes =
        ((ulong)DateTimeOffset.Now.ToUnixTimeSeconds() - op.GenerateTs);

      if (liveMinutes > __opLiveSpanSeconds)
      {
        Console.WriteLine("Op has expired");
        goto terminate_op;
      }

      // Usecase.
      if ((OpType)op.Type != type)
      {
        Console.WriteLine("Op bad usecase");
        goto terminate_op;
      }

      // Random number.
      if ((op.RandNum + 1) != randPlusOne)
      {
        Console.WriteLine("Random number is bad");
        goto terminate_op;
      }

      // Domain.
      if (op.DomainId != domainId)
      {
        Console.WriteLine("Bad domain IId");
        goto terminate_op;
      }

      return true;

    terminate_op:
      await OpLogic.Terminate(op);
      return false;
    }


    /// <summary>
    ///   Gets an op from it's TIID for the
    ///   given domain.
    /// </summary>
    /// <param name="TIID"></param>
    /// <param name="domainId"></param>
    /// <returns></returns>
    public static async Task<OpId>
    Retrieve(string TIID, uint domainId)
    {
      OpId op = await OpAccess.GetByTIID(TIID);
      if (op == null)
      {
        Console.WriteLine($"No op for thid TIID {TIID}");
        return null;
      }

      if (op.DomainId != domainId)
      {
        Debug.WriteLine("Domain mismatch");
        return null;
      }

      return op;
    }





    /// <summary>
    ///   Creates a new op.
    /// </summary>
    /// <param name="domainId"></param>
    /// <returns></returns>
    public static async Task<bool>
    New(uint domainId, OpType type)
    {
      // 1. Get new TIID and check it doesn't exit.
    TIIDGEN:
      string opTIID = IID.NewTIID();

      if (await EFCtx.Ingrs.Ops.AnyAsync(d => d.Tiid == opTIID))
      {
        goto TIIDGEN;
      }

      // 3. Get RNG from crypto (u64).
      ulong rn = Rng.U64();

      // 4. Generation timestamp.
      ulong now = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds();

      // 4. Create op.
      var op = new Op()
        {
          DomainId = domainId,

          Tiid = opTIID,
          RandNum = rn,
          Type = (byte)type,
          GenerateTs = now,

        };

      ReasonCRUDL r = await CrudlAccess<Op>.Create(op);
      if (r != ReasonCRUDL.CREATE)
      {
        Console.WriteLine("EFCtx fails creating op");
        return false;
      }

      return true;
    }




  }
}
