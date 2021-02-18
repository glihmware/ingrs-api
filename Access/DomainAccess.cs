using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Cryptool;

using Ingrs.Services;


namespace Ingrs.Access
{

  /// <summary>
  ///   Gets domain by IID.
  /// </summary>
  public static class DomainAccess
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="domainIID"></param>
    /// <returns></returns>
    public static async Task<Domain>
      GetByIID(string domainIID)
    {
      return await EFCtx.Ingrs.Domains.FirstOrDefaultAsync(d => d.Iid == domainIID);
    }

    /// <summary>
    ///   Verifies if the given domain IID already exists.
    /// </summary>
    /// <param name="domainName"></param>
    /// <returns></returns>

    public static async Task<bool>
      DomainIIDExists(string domainIID)
    {
      return await EFCtx.Ingrs.Domains.AnyAsync(d => d.Iid == domainIID);
    }

    /// <summary>
    ///   Verifies if the given domain name already exists.
    /// </summary>
    /// <param name="domainName"></param>
    /// <returns></returns>

    public static async Task<bool>
      DomainNameExists(string domainName)
    {
      return await EFCtx.Ingrs.Domains.AnyAsync(d => d.Name == domainName);
    }


    /// <summary>
    ///   Verifies if a domain exists from it's id.
    /// </summary>
    /// <param name="domainId"></param>
    /// <returns></returns>
    public static async Task<bool>
      DomainIdExists(uint domainId)
    {
      return await EFCtx.Ingrs.Domains.AnyAsync(d => d.Id == domainId);
    }

    /// <summary>
    ///   Verifies the domain's info.
    ///
    ///   If they are ok, a new IID is generated
    ///   and the domain is added.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="rsaPub"></param>
    /// <param name="ecdsaPub"></param>
    /// <returns></returns>
    public static async Task<IngrsReason>
    VerifyAdd(string name, string rsaPub, string ecdsaPub)
    {
      byte[] rsa = BufArray.BufferFromB64(rsaPub);
      byte[] ecdsa = BufArray.BufferFromB64(ecdsaPub);

      if (rsa == null || !Rsa.VerifyPubKeyImport(rsa))
      {
        return IngrsReason.RsaPubInvalid;
      }

      if (ecdsa == null || !Ecdsa.VerifyPubKeyImport(ecdsa))
      {
        return IngrsReason.EcdsaPubInvalid;
      }

      // Should also check the keys unicity...?
      if (await EFCtx.Ingrs.Domains.AnyAsync(d => d.Name == name))
      {
        return IngrsReason.DomainNameAlreadyExists;
      }

    IIDGEN:
      string domainIID = IID.NewIID();

      if (await EFCtx.Ingrs.Domains.AnyAsync(d => d.Iid == domainIID))
      {
        goto IIDGEN;
      }

      Domain d = new Domain()
        {
          Name = name,
          Iid = domainIID,
          RsaPub = rsaPub,
          EcdsaPub = ecdsaPub,
        };

      ReasonCRUDL r = await CrudlAccess<Domain>.Create(d);
      switch (r)
      {
        case ReasonCRUDL.DUPLICATE:
          // should go back to generate? should not happen if keys are not unique.
          throw new Exception("TO CHECK!");

        case ReasonCRUDL.CREATE:
          return IngrsReason.Ok;

        default:
          return IngrsReason.EFCtxError;
      }
    }
  }
}
