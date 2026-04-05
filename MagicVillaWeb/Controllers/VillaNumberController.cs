using AutoMapper;
using MagicVillaUtility;
using MagicVillaWeb.Model;
using MagicVillaWeb.Model.DTO;
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
            var resonse = await _serviceVillaNumber.GetAllAsync<ApiResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resonse.isSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(resonse.Result));
            }
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberDTOCreate numberDTOCreate = new();
           
            var response = await _serviceVilla.GetAllAsync<ApiResponse>(HttpContext.Session.GetString(SD.SessionToken));

            if (response.isSuccess)
            {
                numberDTOCreate.Villas = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));

                return View(numberDTOCreate);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberDTOCreate numberDTOCreate)
        {
            if (ModelState.IsValid) {
                var response = await _serviceVillaNumber.CreateAsync<ApiResponse>(numberDTOCreate, HttpContext.Session.GetString(SD.SessionToken));

                if (response != null && response.isSuccess && response.ErrorMessege.Count == 0 && response.ErrorMessege!= null)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response.ErrorMessege.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessege", response.ErrorMessege.FirstOrDefault());
                    }
                }
            }
            var resp = await _serviceVilla.GetAllAsync<ApiResponse>(HttpContext.Session.GetString(SD.SessionToken));

            if (resp.isSuccess && resp!= null)
            {
                numberDTOCreate.Villas = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(resp.Result));

                return View(numberDTOCreate);
            }

            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateVillaNumber(int VillaNo)
        {
            VillaNumberDTOUpdate dTOUpdate = new();

            var response_obj = await _serviceVillaNumber.GetAsync<ApiResponse>(VillaNo, HttpContext.Session.GetString(SD.SessionToken));

            if (response_obj.isSuccess)
            {
                dTOUpdate = JsonConvert.DeserializeObject<VillaNumberDTOUpdate>(Convert.ToString(response_obj.Result));

                return View(dTOUpdate);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberDTOUpdate dTOUpdate)
        {
            if (ModelState.IsValid)
            {
                var response = await _serviceVillaNumber.UpdateAsync<ApiResponse>(dTOUpdate, HttpContext.Session.GetString(SD.SessionToken));

                if (response.isSuccess && response != null)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }
            return View(dTOUpdate);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteVillaNumber(int VillaNo)
        {
            VillaNumberDTO villa = new();
            var response = await _serviceVillaNumber.GetAsync<ApiResponse>(VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response.isSuccess && response != null)
            {
                villa = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                return View(villa);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDTO villaNumber)
        {
            var response = await _serviceVillaNumber.DeleteAsync<ApiResponse>(villaNumber.VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response.isSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            return NotFound();
        }

    }
}