using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using MagicVilla_VillaApi.Repository.Interfaces;
using System.Net;
using MagicVilla_VillaApi.Service.Interfaces;

namespace MagicVilla_VillaApi.Controllers
{
    [ApiController]
    [Route("api/VillaApi")]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogger<VillaApiController> _logger;
        readonly IMapper _mapper;
        private readonly IVillaRepository _repository;
        private readonly IVillaServices _villaServices;
        protected ApiResponse _response;

        public VillaApiController(ILogger<VillaApiController> logger, ApplicationDBContext context, IMapper mapper, IVillaRepository repository, IVillaServices villaServices)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _villaServices = villaServices;
            _response = new ApiResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villas = await _repository.GetAll();

                _response.Result = _mapper.Map<IEnumerable<VillaDTO>>(villas);
                _response.HttpStatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.ErrorMessege = new List<string> { ex.Message };
                _response.isSuccess = false;
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);

            }
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetVillas(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogInformation("Id 0");
                    return BadRequest();
                }
                var obj = await _repository.Get(x => x.Id == id);
                if (obj == null)
                {
                    _response.Result = HttpStatusCode.NotFound;
                    _response.Result = false;                
                    _logger.LogInformation("Villa not found");
                    return NotFound(_response);
                }

                _logger.LogInformation("Ok");
                _response.Result = obj;
                _response.HttpStatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.ErrorMessege = new List<string> { ex.Message };
                _response.isSuccess = false;
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateVilla([FromBody] VillaDTOCreate dto_create)
        {
            if (dto_create == null)
            {
                _response.ErrorMessege = new List<string> { "dto_create == null" };
                _response.Result = false;
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                Villa new_villa = _mapper.Map<Villa>(dto_create);
                await _repository.Create(new_villa);

                _response.Result = new_villa;
                _response.HttpStatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { new_villa.Id }, _response);

            }
            catch(Exception ex)
            {
                _response.ErrorMessege = new List<string> { ex.Message };
                _response.isSuccess = false;
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> DeleteVilla(int id)
        {
           
            try
            {
                if (id == 0)
                {
                    _response.ErrorMessege = new List<string> { "id == 0" };
                    _response.Result = false;
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var obj = await _repository.Get(x => x.Id == id);
                if (obj == null)
                {
                    _response.ErrorMessege = new List<string> { "id == 0" };
                    _response.Result = false;
                    _response.HttpStatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _repository.Delete(obj);
                _response.HttpStatusCode = HttpStatusCode.OK;
                

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessege = new List<string> { ex.Message };
                _response.isSuccess = false;
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> PutVilla(int id, [FromBody] VillaDTOUpdate villa)
        {
            try
            {
                if (id != villa.Id || villa == null)
                {
                    _response.Result = false;
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                await _villaServices.Update(villa);
                _response.HttpStatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessege = new List<string> { ex.Message };
                _response.isSuccess = false;
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }
        
        [HttpPatch("{id:int}", Name ="UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDto)
        {
            try
            {
                if (id == 0)
                {
                    _response.Result = false;
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _repository.Get(x => x.Id == id);
                if (villa == null)
                {
                    _response.Result = false;
                    _response.HttpStatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                VillaDTO modelDTO = _mapper.Map<VillaDTO>(villa);
                patchDto.ApplyTo(modelDTO, ModelState);

                _mapper.Map(modelDTO, villa);
                await _repository.Save();
                _response.HttpStatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessege = new List<string> { ex.Message };
                _response.isSuccess = false;
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }
    }
}
