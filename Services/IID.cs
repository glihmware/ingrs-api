using System;
using System.Diagnostics;
using System.Text;

using Cryptool;


namespace Ingrs.Services
{
  /// <summary>
  ///   Ingrs ID service.
  /// </summary>
  public static class IID
  {
    private static readonly Argon2Parameter __argon2Prms;
    private static readonly Argon2Parameter __argon2PrmsTmp;

    /// <summary>
    ///   Constructs statically.
    /// </summary>
    static IID()
    {
      __argon2Prms = new Argon2Parameter()
        {
          Parallelism = 1,
          MemorySize1Kb = 1024 * 512,
          Iterations = 2,
          ByteCount = 128,
          Salt = null,
          Data = null,
        };

      __argon2PrmsTmp = new Argon2Parameter()
        {
          Parallelism = 1,
          MemorySize1Kb = 1024 * 128,
          Iterations = 1,
          ByteCount = 256,
          Salt = null,
          Data = null,
        };
    }

    /// <summary>
    ///   Temporal IID.
    /// </summary>
    /// <returns></returns>
    public static string NewTIID()
    {
      string guid1 = Guid.NewGuid().ToString();
      string guid2 = Guid.NewGuid().ToString();

      byte[] psswd = Encoding.UTF8.GetBytes($"{guid1}|{guid2}");
      byte[] hash = Argon2.ComputeHash2id(psswd, __argon2PrmsTmp);

      return Convert.ToBase64String(hash);
    }

    /// <summary>
    ///   Ingrs identifier.
    /// </summary>
    /// <returns></returns>
    public static string NewIID()
    {
      string guid1 = Guid.NewGuid().ToString();
      string guid2 = Guid.NewGuid().ToString();

      byte[] psswd = Encoding.UTF8.GetBytes($"{guid1}|{guid2}");
      byte[] hash = Argon2.ComputeHash2id(psswd, __argon2Prms);

      return Convert.ToBase64String(hash);
    }

  }
}
