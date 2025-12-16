using AutoMapper;
using MagicVillaWeb.Model;
using MagicVillaWeb.Model.DTO;
using MagicVillaWeb.Models;
using MagicVillaWeb.Models.ViewModelDTO;
using MagicVillaWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVillaWeb.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _serviceVillaNumber;
        private readonly IMapper _mapper;
        private readonly IVillaService _serviceVilla;

        public VillaNumberController(IVillaNumberService serviceVillaNumber, IVillaService serviceVilla, IMapper mapper)
        {
            _serviceVillaNumber = serviceVillaNumber;
            _serviceVilla = serviceVilla;
            _mapper = mapper;
        }
        [HttpGet]
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
        [HttpGet]
        public async Task<IActionResult> CreateVillaNumber()
        {
            List<VillaDTO> villas = new();
            var response = await _serviceVilla.GetAllAsync<ApiResponse>();

            if (response.isSuccess)
            {
                villas = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(villas);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberDTOCreate numberDTOCreate)
        {
            var response = await _serviceVillaNumber.CreateAsync<ApiResponse>(numberDTOCreate);

            if (response != null && response.isSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            return View(numberDTOCreate);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateVillaNumber(int id)
        {
            List<VillaDTO> villas = new();
            VillaNumberDTOUpdate dTOUpdate = new();
            VillaNumberViewModel updateVilla = new();

            updateVilla.Villas = villas;
            updateVilla.VillaNumberDTOUpdate = dTOUpdate;

            var response_list = await _serviceVilla.GetAllAsync<ApiResponse>();
            var response_obj = await _serviceVillaNumber.GetAsync<ApiResponse>(id);

            if (response_list.isSuccess && response_obj.isSuccess)
            {
                updateVilla.Villas = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response_list.Result));
                updateVilla.VillaNumberDTOUpdate = JsonConvert.DeserializeObject<VillaNumberDTOUpdate>(Convert.ToString(response_obj.Result));

                return View(updateVilla);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberViewModel villaNumberViewModel)
        {
            var response = await _serviceVillaNumber.UpdateAsync<ApiResponse>(villaNumberViewModel.VillaNumberDTOUpdate);

            if (response.isSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            return View(villaNumberViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteVillaNumber(int id)
        {
            VillaNumberDTO villa = new();
            var response = await _serviceVillaNumber.GetAsync<ApiResponse>(id);
            if (response.isSuccess)
            {
                villa = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
            }
            return View(villa);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDTO villaNumber)
        {
            VillaNumberDTO villa = new();
            var response = await _serviceVillaNumber.DeleteAsync<ApiResponse>(villaNumber.VillaNo);
            if (response.isSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            return NotFound();
        }

    }
}
