using AutoMapper;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Repository.Interfaces;
using MagicVilla_VillaApi.Service.Interfaces;

namespace MagicVilla_VillaApi.Service
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IMapper _mapper;

        public VillaNumberService(IVillaNumberRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _villaNumberRepository = repository;
        }

        public async Task Update(VillaNumberDTOUpdate dto)
        {
            var villa = await _villaNumberRepository.Get(x => dto.VillaNo == x.VillaNo, true);
            if (villa == null)
                throw new Exception("Villa not found");

            _mapper.Map(dto, villa);
            villa.DateTimeUpdate = DateTime.UtcNow;

            await _villaNumberRepository.Save();
        }
    }
}
