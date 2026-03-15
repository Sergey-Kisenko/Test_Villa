using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Repository;
using MagicVilla_VillaApi.Repository.Interfaces;
using MagicVilla_VillaApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;

namespace MagicVilla_VillaApi.Controllers
{
    [ApiController]
    [Route("api/VillaNumber")]
    public class VillaNumberController : Controller
    {
        readonly IMapper _mapper;
        private readonly IVillaNumberRepository _repository;
        private readonly IVillaNumberService _villaNmberServices;

        private ApiResponse _response;

        public VillaNumberController(IMapper _mapper, IVillaNumberRepository _context, IVillaNumberService _villaNmberServices)
        {
            this._mapper = _mapper;
            this._repository = _context;
            this._villaNmberServices = _villaNmberServices;

            _response = new ApiResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetAll()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumbers = await _repository.GetAll(includeProperties:nameof(Villa));

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
                VillaNumber villaNumbers = await _repository.Get(x=>x.VillaNo == VillaNo, false,"Villa");
                if (villaNumbers == null)
                {
                    _response.ErrorMessege = new List<string> { "Villa number is null" };
                    return BadRequest(_response);
                }
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumbers);
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

                if (await _repository.Get(u => u.VillaNo == numberDTOCreate.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessege", "Villa number already Exsist");
                    _response.isSuccess = false;
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(ModelState);
                }
                if (await _repository.Get(u => u.VillaId == numberDTOCreate.VillaId) != null)
                {

                    ModelState.AddModelError("ErrorMessege", "Villa id is not invalid");
                    _response.isSuccess = false;
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(ModelState);
                }
                
                VillaNumber villaNum = _mapper.Map<VillaNumber>(numberDTOCreate);
                await _repository.Create(villaNum);
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

        [HttpPut("{VillaNo:int}")]
        public async Task<ActionResult<ApiResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberDTOUpdate dTOUpdate )
        {
            try
            {
                if (dTOUpdate == null && id != dTOUpdate.VillaNo)
                {
                    _response.isSuccess = false;
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                await _villaNmberServices.Update(dTOUpdate);

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

        [HttpDelete("{VillaNo:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> DeleteVilla(int VillaNo) // не пашет 
        {

            try
            {
                if (VillaNo == 0)
                {
                    _response.ErrorMessege = new List<string> { "VillaNo == 0" };
                    _response.Result = false;
                    _response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var obj = await _repository.Get(x => x.VillaNo == VillaNo, true);
                if (obj == null)
                {
                    _response.ErrorMessege = new List<string> { "VillaNo == 0" };
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
    }
}
