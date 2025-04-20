using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;

namespace MagicVilla_VillaApi.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/VillaApi")]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogger<VillaApiController> _logger;
        public VillaApiController(ILogger<VillaApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.DBVillaStore);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVillas(int id)
        {
            if (id == 0)
            {
                _logger.LogInformation("Id 0");
                return BadRequest();
            }

            var obj = VillaStore.DBVillaStore.FirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                _logger.LogInformation("Villa not found");
                return NotFound();
            }
            _logger.LogInformation("Ok");

            return Ok(obj);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villa)
        {
            if (villa == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villa.Id = VillaStore.DBVillaStore.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.DBVillaStore.Add(villa);

            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var obj = VillaStore.DBVillaStore.FirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            VillaStore.DBVillaStore.Remove(obj);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PutVilla(int id, [FromBody] VillaDTO villa)
        {
            if (id != villa.Id || villa == null)
            {
                return BadRequest();
            }
            var obj = VillaStore.DBVillaStore.FirstOrDefault(x => x.Id == id);
            obj.Name = villa.Name;

            return NoContent();
        }
        [HttpPatch("{id:int}", Name ="UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDto)
        {
            if(id ==0 || id == null)
            {
                return BadRequest();
            }

            var villa = VillaStore.DBVillaStore.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();

        }
    }
}
