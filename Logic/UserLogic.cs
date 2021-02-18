// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Threading.Tasks;

// using Cryptool;

// using Ingrs.Access;
// using Ingrs.Services;
// using Ingrs.Dto;


// namespace Ingrs.Logic
// {

//   /// <summary>
//   /// 
//   /// </summary>
//   public class UserCred
//   {
//     public string IID { get; set; }
//     public string Salt { get; set; }
//     public string Argon2UD { get; set; }
//     public string Argon2PS { get; set; }
//   }


//   public static class UserLogic
//   {

//     /// <summary>
//     ///   Registers a user for the given domain.
//     /// </summary>
//     /// <param name="domainId"></param>
//     /// <param name="cred"></param>
//     /// <returns></returns>
//     public static async Task<IngrsReason>
//       Register(uint domainId, UserCred cred)
//     {
//       cred.IID = null;

//       // 1. Check if user exists.
//       User u = await UserAccess.GetByUD(cred.Argon2UD);
//       if (u != null)
//       {
//         Debug.WriteLine("User exists");
//         return IngrsReason.UserNameAlreadyExists;
//       }

//     regenIID:
//       Debug.WriteLine("Generate IID");
//       // 2. Generate new IID.
//       string Iid = IID.NewIID();
//       u = await UserAccess.GetByIID(Iid);
//       if (u != null)
//       {
//         goto regenIID;
//       }

//       // 3. Register.
//       u = new User()
//         {
//           Iid = Iid,
//           Salt = cred.Salt,
//           ArgonUd = cred.Argon2UD,

//           ArgonPs = UserSrv.ComputeArgonPSDb(cred.Argon2PS, cred.Salt),

//           DomainId = domainId,

//         };

//       Debug.WriteLine("Registering user");
//       bool registered = await UserAccess.Create(u);
//       if (!registered)
//       {
//         return IngrsReason.EFCtxError;
//       }

//       cred.IID = Iid;

//       return IngrsReason.Ok;
//     }



//     /// <summary>
//     ///   Verifies the given user credentials.
//     /// </summary>
//     /// <param name="domainId"></param>
//     /// <param name="cred"></param>
//     /// <returns></returns>
//     public static async Task<IngrsReason>
//       Verify(UserCred cred)
//     {
//       cred.IID = null;

//       byte[] dummySalt = new byte[64];
//       Rng.GetBytes(dummySalt);
//       string ds = Convert.ToBase64String(dummySalt);

//       // Get candidate user.
//       User user = await UserAccess.GetByUD(cred.Argon2UD);
//       if (user == null)
//       {
//         UserSrv.ComputeArgonPSDb(cred.Argon2PS, ds);
//         return IngrsReason.UserUnknown;
//       }

//       // Compute Argon2(ArgonPS, salt).
//       string argon2PSDb = UserSrv.ComputeArgonPSDb(cred.Argon2PS, user.Salt);

//       // Check if user's credential exists.
//       User u = await UserAccess.GetByCred(user.DomainId, cred.Argon2UD, argon2PSDb);
//       if (u == null)
//       {
//         Debug.WriteLine("Bad cred");
//         return IngrsReason.UserBadCredencials;
//       }

//       // Update user.
//       Debug.WriteLine("updating user");
//       //DateTime LastConn = (DateTime)u.LastSuccessCon;
//       bool updated = await UserAccess.UpdateSuccessCon(u);
//       if (!updated)
//       {
//         return IngrsReason.EFCtxError;
//       }

//       cred.IID = u.Iid;
//       return IngrsReason.Ok;
//     }




//   }
// }
