using System;
using System.Threading.Tasks;

using Cryptool;

using Ingrs.Services;
using Ingrs.Access;
using Ingrs.Dto;

namespace Ingrs.Logic
{
  public static class DomainLogic
  {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="plain"></param>
    /// <returns></returns>
    public static byte[]
    EncryptData(DomainId domain, byte[] plain)
    {
      CryptoKeyPair kp = new CryptoKeyPair()
        {
          KeySize = 4096,
          Pub = Convert.FromBase64String(domain.RsaPub)
        };

      return Rsa.EncryptOAEP512(kp, plain);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="data"></param>
    /// <param name="signature"></param>
    /// <returns></returns>
    public static bool
    VerifySignature(Domain domain, byte[] data, byte[] signature)
    {
      CryptoKeyPair kp = new CryptoKeyPair()
        {
          KeySize = 521,
          Pub = Convert.FromBase64String(domain.EcdsaPub)
        };

      return Ecdsa.Verify(kp, signature, data);
    }
  }
}
