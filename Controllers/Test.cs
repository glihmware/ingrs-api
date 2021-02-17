using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Ingrs.Dto;
using Ingrs.Access;


namespace Ingrs.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
    public partial class Test : ControllerBase
    {
      [HttpGet]
      public async Task<ActionResult>
        F1()
      {
        Console.WriteLine("start testing!");
        //
        string tiid = Services.IID.NewTIID();
        string iid = Services.IID.NewIID();
        Console.WriteLine($"TIID: {tiid.Length} {tiid}");
        Console.WriteLine($"IID: {iid.Length} {iid}");

        IngrsReason ir = await DomainAccess.VerifyAdd("1234", "MIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEAtk9L33pVIH5rKw2lLBBs4C3VI+Ou1LH+GIbx4ATfW7+4E/ly0KUIf1FvzBISg6V9AdACRhX2N8b5S6LUpJLiuSQbQASur44qtS49q+FOhKUSvNOlJmFDtzdt1z8hmbcSb2V1rimN7k5zRBYLf1pJQc4uwpH312oIAHxbjqkTkmfMXTsmz5RYjFjJCmKvqoffNT1vumqUc8sTz0vomo00DgBMq/XGrTKnRtXrjqNaw6NoE95Wjn8wZTjlC0XB086KN3YjyZUv/QnKFjIg9b3mOU+FFza2gHDmLnUrod0OnnB/UJzkVPnwjNY4GRi1HYUepOIiEKYbFT6YWSxImNUY3kdKYLef+e/9fySDZ5FL088S3DQP/bjTuQ1d1CySVNRNphyhLFnIYB8+wf8tPJFwo8RqDquNZ3pkttc0o9jSCpnzV6JBmlfb+lZisPTT6YHUomFDNrzAd/oQ/oUI7O2Tb0Jy3xZWURHGuQ4KJFx3gQmORxdo5fwPd7LSb6xiCW3VfJRqoxKvuoxQMoUFWXY2mAJfFt4KiyPJMihJhjK1Dx18b+jVnhjOHsGc+MaEUt2tsUZOS1QL8YA23X2G4TpMKkbMSTKm7JpmcPYt+ot6oi3cOPcwSm3KW0heQCQ7TlLItgV5dLg47LaHmS9WwZJMwIEWPz9trAMRcvti6CiOBOMCAwEAAQ==", "MIGbMBAGByqGSM49AgEGBSuBBAAjA4GGAAQAW6z5gb/W7q9wELG0d86a5wnFlD+FhkQ5uCdvBJWqz2pqEDwHqHZjO844Hd/UvE9eAvOUBy4Vgl2BJ7/BU1BfqPQAcHrOE9op+GA+sZ5awS5IyIFmOfiqGRTubBKDLDOKVJZzXp6reO1nONI5DHPr07qK+r1DTDKZOHVn31Z3WXOqLLc=");
        Console.WriteLine($"test --> {ir}");



        //
        // bool created = await OpLogic.New(123, OpUsecase.Register);
        // Debug.WriteLine($"CREATED: {created}");

        // //
        // UserCred uc = new UserCred()
        //   {
        //     Salt = Convert.ToBase64String(new byte[] { 10, 20, 30 }),
        //     Argon2UD = Convert.ToBase64String(new byte[] { 1, 2, 3}),
        //     Argon2PS = Convert.ToBase64String(new byte[] { 0xa, 0xb, 0xc }),
        //   };

        // IngrsReason r;

        // try
        // {
        //   r = await UserLogic.Register(1, uc);
        //   Debug.WriteLine($"{r} ==> {uc.IID}");
        // }
        // catch (Exception e)
        // {
        //   Debug.WriteLine($"1 {e}");
        // }

        // try
        // {
        //   r = await UserLogic.Verify(uc);
        //   Debug.WriteLine($"{r} ==> {uc.IID}");
        // }
        // catch (Exception e)
        // {
        //   Debug.WriteLine($"2 {e}");
        // }


        // Debug.WriteLine($"SALT {await UserAccess.GetSalt(uc.Argon2UD)}");




        return Ok();
      }

    }
}
