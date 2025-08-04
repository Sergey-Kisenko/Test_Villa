using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using MagicVilla_VillaApi.CustomLogs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MagicVilla_VillaApi.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/VillaApi")]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogger<VillaApiController> _logger;
        readonly ApplicationDBContext _context;
        readonly IMapper _mapper;
        public VillaApiController(ILogger<VillaApiController> logger, ApplicationDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villas = await _context.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villas));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVillas(int id)
        {
            if (id == 0)
            {
                _logger.LogInformation("Id 0");
                return BadRequest();
            }

            var obj = await _context.Villas.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
            {
                _logger.LogInformation("Villa not found");
                return NotFound();
            }
            _logger.LogInformation("Ok");

            return Ok(_mapper.Map<VillaDTO>(obj));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaDTOCreate dto_create)
        {
            if (dto_create == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            Villa model = _mapper.Map<Villa>(dto_create);
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            // вызвавать маршрут - показать виллу и данные созданной виллы
            return CreatedAtRoute("GetVilla", new { model.Id }, model);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var obj = await _context.Villas.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Villas.Remove(obj);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutVilla(int id, [FromBody] VillaDTOUpdate villa)
        {
            if (id != villa.Id || villa == null)
            {
                return BadRequest();
            }
            var obj = await _context.Villas.FirstOrDefaultAsync(x => x.Id == id);
            obj = _mapper.Map<Villa>(villa);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpPatch("{id:int}", Name ="UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int? id, JsonPatchDocument<VillaDTO> patchDto)
        {
            if(id ==0 || id == null)
            {
                return BadRequest();
            }

            // или использовать отвязку трекинга, менять сущость и делать апдейт
            //var villa = _context.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);

            var villa = _context.Villas.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return BadRequest();
            }
            VillaDTO modelDTO = _mapper.Map<VillaDTO>(villa);

            patchDto.ApplyTo(modelDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            villa = _mapper.Map<Villa>(modelDTO);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
