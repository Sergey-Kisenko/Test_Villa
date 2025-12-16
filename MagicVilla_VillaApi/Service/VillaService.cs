using AutoMapper;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Repository.Interfaces;
using MagicVilla_VillaApi.Service.Interfaces;

namespace MagicVilla_VillaApi.Service
{
    public class VillaService : IVillaServices
    {
        private readonly IVillaRepository _villaRepository;
        private readonly IMapper _mapper;


        public VillaService(IVillaRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _villaRepository = repository;
        }

        public async Task Update(VillaDTOUpdate dto)
        {
            var villa = await _villaRepository.Get(x=>dto.Id == x.Id);
            if (villa == null)
                throw new Exception("Villa not found");

            _mapper.Map(dto, villa);
            villa.UpdateDate = DateTime.UtcNow;

            await _villaRepository.Save();


        }
    }
}
