using AutoMapper;
using MagicVillaWeb.Model;
using MagicVillaWeb.Model.DTO;
using MagicVillaWeb.Models;
using MagicVillaWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVillaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IVillaService _villaService;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, IVillaService villaService)
        {
            _logger = logger;
            _mapper = mapper;
            _villaService = villaService;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDTO> villas = new ();
            var resonse = await _villaService.GetAllAsync<ApiResponse>();
            if (resonse!= null && resonse.isSuccess)
            {
                villas = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(resonse.Result));
            }

            return View(villas);
        }
        public async Task<IActionResult> Book()
        {
            return View();
        }
      

    }
}
