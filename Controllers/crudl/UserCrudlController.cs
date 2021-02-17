using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Ingrs.Dto;
using Ingrs.Access;
using Ef = Ingrs.Access;
using Ent = Ingrs.Access;

namespace Ingrs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UserController : ControllerBase
    {
        [HttpGet]
        private async Task<ActionResult<List<UserId>>>
        List()
        {
            List<UserId> l = await Ef.CrudlAccess<Ent.User>.List<UserId>();
            if (l == null)
            {
                return StatusCode(500);
            }

            return Ok(l);
        }

        [HttpGet("{id:int}")]
        private async Task<ActionResult<UserId>>
        Read(uint id)
        {
            UserId eid = await Ef.CrudlAccess<Ent.User>.Read<UserId>(id);

            if (eid == null)
            {
                return NotFound();
            }

            return Ok(eid);
        }

        // POST: api/company
        [HttpPost]
        private async Task<ActionResult<UserId>>
        Create([FromBody] UserBase b)
        {
            Ent.User e = b.ToEntity();
            Ef.ReasonCRUDL r = await Ef.CrudlAccess<Ent.User>.Create(e);

            switch (r)
            {
              case Ef.ReasonCRUDL.DUPLICATE:
                return BadRequest();

              case Ef.ReasonCRUDL.CREATE:
                return Ok(new UserId(e));

              default:
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        private async Task<ActionResult>
        Update(uint id, [FromBody] UserId eid)
        {
            if (eid.Id != id)
            {
                return BadRequest();
            }

            Ef.ReasonCRUDL r = await Ef.CrudlAccess<Ent.User>.Update<UserId>(eid);

            switch (r)
            {
              case Ef.ReasonCRUDL.NOT_FOUND:
                return NotFound();

              case Ef.ReasonCRUDL.UPDATE:
                 return NoContent();

              default:
                 return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        private async Task<ActionResult>
        Delete(uint id)
        {
            Ef.ReasonCRUDL r = await Ef.CrudlAccess<Ent.User>.Delete(id);

            switch (r)
            {
              case Ef.ReasonCRUDL.NOT_FOUND:
                return NotFound();

              case Ef.ReasonCRUDL.FK_CONSTRAINT:
                return BadRequest();

              case Ef.ReasonCRUDL.DELETE:
                 return NoContent();

              default:
                 return StatusCode(500);
            }
        }
    }
}
