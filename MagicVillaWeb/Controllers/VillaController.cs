using AutoMapper;
using MagicVillaUtility;
using MagicVillaWeb.Model;
using MagicVillaWeb.Model.DTO;
using MagicVillaWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVillaWeb.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> list = new();
            var response = await _villaService.GetAllAsync<ApiResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.isSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreateVilla(VillaDTOCreate model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.CreateAsync<ApiResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.isSuccess)
                {
                    TempData["success"] = "Villa was creared successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            return View(model);
        }
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await _villaService.GetAsync<ApiResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.isSuccess)
            {
                VillaDTO villa = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaDTOUpdate>(villa));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(VillaDTOUpdate model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.UpdateAsync<ApiResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.isSuccess)
                {
                    TempData["success"] = "Villa was updated successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _villaService.GetAsync<ApiResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.isSuccess)
            {

                VillaDTO villa = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(villa);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilla(VillaDTO model)
        {
            var response = await _villaService.DeleteAsync<ApiResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.isSuccess)
            {
                TempData["success"] = "Villa  was deleted";

                return RedirectToAction(nameof(IndexVilla));
            }
            return NotFound();
        }
    }
}
