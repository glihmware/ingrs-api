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
    public partial class OpController : ControllerBase
    {
        [HttpGet]
        private async Task<ActionResult<List<OpId>>>
        List()
        {
            List<OpId> l = await Ef.CrudlAccess<Ent.Op>.List<OpId>();
            if (l == null)
            {
                return StatusCode(500);
            }

            return Ok(l);
        }

        [HttpGet("{id:int}")]
        private async Task<ActionResult<OpId>>
        Read(uint id)
        {
            OpId eid = await Ef.CrudlAccess<Ent.Op>.Read<OpId>(id);

            if (eid == null)
            {
                return NotFound();
            }

            return Ok(eid);
        }

        // POST: api/company
        [HttpPost]
        private async Task<ActionResult<OpId>>
        Create([FromBody] OpBase b)
        {
            Ent.Op e = b.ToEntity();
            Ef.ReasonCRUDL r = await Ef.CrudlAccess<Ent.Op>.Create(e);

            switch (r)
            {
              case Ef.ReasonCRUDL.DUPLICATE:
                return BadRequest();

              case Ef.ReasonCRUDL.CREATE:
                return Ok(new OpId(e));

              default:
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        private async Task<ActionResult>
        Update(uint id, [FromBody] OpId eid)
        {
            if (eid.Id != id)
            {
                return BadRequest();
            }

            Ef.ReasonCRUDL r = await Ef.CrudlAccess<Ent.Op>.Update<OpId>(eid);

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
            Ef.ReasonCRUDL r = await Ef.CrudlAccess<Ent.Op>.Delete(id);

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
