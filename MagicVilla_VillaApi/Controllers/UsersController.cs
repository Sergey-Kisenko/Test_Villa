using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers
{
    [ApiController]
    [Route("api/UsersAuth")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected ApiResponse _response;

        public UsersController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
            this._response = new();
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginRequestDTO model)
        {
            var loginRespose = await _userRepository.Login(model);
            if (loginRespose == null || string.IsNullOrEmpty(loginRespose.Token))
            {
                _response.isSuccess = false;
                _response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorMessege.Add("User name or password is incorrect");
                return BadRequest(_response);
            }

            _response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _response.isSuccess = true;
            _response.Result = loginRespose;

            return Ok(_response);
        }
       
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterationRequestDTO model)
        {
            bool ifUserNameUnuque = _userRepository.IsUniqueUser(model.UserName);
            if(ifUserNameUnuque == false)
            {
                _response.isSuccess = false;
                _response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorMessege.Add("Username already exists");
                return BadRequest(_response);
            }

            var user = await _userRepository.Register(model);
            if(user == null)
            {
                _response.isSuccess = false;
                _response.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorMessege.Add("Error while registration");
                return BadRequest(_response);

            }
            _response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            _response.isSuccess = true;

            return Ok(_response);
        }
    }
}
