using System;
using System.Text;
using System.Threading.Tasks;

using Cryptool;


namespace Ingrs.Services
{
  public static class UserSrv
  {

    /// <summary>
    ///   Computes the Argon2 hash of Argon2 from front-end..
    /// </summary>
    //
    /// <param name="argon2Front"> string base64 of front-end computed Argon2PS </param>
    /// <param name="salt"> string base64 of the salt </param>
    /// <returns></returns>
    public static string
    ComputeArgonFromArgonFront(string argon2Hash)
    {
      var p = new Argon2Parameter()
        {
          Parallelism = 2,
          MemorySize1Kb = 1024 * 128,
          Iterations = 2,
          ByteCount = 128,
          Salt = null,
          Data = null,
        };

      byte[] a2front = Convert.FromBase64String(argon2Hash);

      byte[] hash = Argon2.ComputeHash2id(a2front, p);
      return Convert.ToBase64String(hash);
    }


  }
}
