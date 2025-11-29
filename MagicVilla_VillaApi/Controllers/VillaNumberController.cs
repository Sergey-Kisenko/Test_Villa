using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Repository;
using MagicVilla_VillaApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaApi.Controllers
{
    [ApiController]
    [Route("api/VillaNumber")]
    public class VillaNumberController : Controller
    {
        readonly IMapper _mapper;
        private readonly IVillaNumberRepository _context;
        private ApiResponse _response;

        public VillaNumberController(IMapper _mapper, IVillaNumberRepository _context)
        {
            this._mapper = _mapper;
            this._context = _context;
            _response = new ApiResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetAll()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumbers = await _context.GetAll(includeProperties:nameof(Villa));

                _response.Result = _mapper.Map<IEnumerable<VillaNumberDTO>>(villaNumbers);
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
        [HttpGet("{VillaNo:int}", Name = "GetNumberVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>>Get(int VillaNo)
        {
            try
            {
                if (VillaNo == 0)
                {
                    _response.ErrorMessege = new List<string> { "VillaNo is 0" };
                    return BadRequest(_response);
                }
                VillaNumber villaNumbers = await _context.Get(x=>x.VillaNo == VillaNo);
                if (villaNumbers == null)
                {
                    _response.ErrorMessege = new List<string> { "Villa number is null" };
                    return BadRequest(_response);
                }
                _response.Result = _mapper.Map<VillaNumber>(villaNumbers);
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

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateVillaNumber([FromBody]VillaNumberDTOCreate numberDTOCreate)
        {
            try
            {
                if (numberDTOCreate == null)
                {
                    _response.isSuccess = false;
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                VillaNumber villaNum = _mapper.Map<VillaNumber>(numberDTOCreate);
                await _context.Create(villaNum);
                _response.Result = numberDTOCreate;
                _response.HttpStatusCode = HttpStatusCode.OK;

                return CreatedAtRoute("GetNumberVilla", new { numberDTOCreate.VillaNo }, _response);

            }
            catch (Exception ex)
            {
                _response.ErrorMessege = new List<string> { ex.Message };
                _response.isSuccess = false;
                _response.HttpStatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberDTOUpdate dTOUpdate )
        {
            try
            {
                if (dTOUpdate == null)
                {
                    _response.isSuccess = false;
                    return BadRequest(_response);
                }

                await _context.Update(_mapper.Map<VillaNumber>(dTOUpdate));

                _response.Result = dTOUpdate;
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
                var obj = await _context.Get(x => x.VillaNo == id);
                if (obj == null)
                {
                    _response.ErrorMessege = new List<string> { "id == 0" };
                    _response.Result = false;
                    _response.HttpStatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _context.Delete(obj);
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
