using AutoMapper;
using MagicVillaWeb.Model;
using MagicVillaWeb.Model.DTO;
using MagicVillaWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MagicVillaWeb.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _serviceVillaNumber;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService serviceVillaNumber, IMapper mapper)
        {
            _serviceVillaNumber = serviceVillaNumber;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> list = new();
            var resonse = await _serviceVillaNumber.GetAllAsync<ApiResponse>();
            if (resonse.isSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(resonse.Result));
            }
            return View(list);
        }
    }
}
