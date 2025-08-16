using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;

namespace MagicVilla_VillaApi
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<Villa, VillaDTOCreate>().ReverseMap();
            CreateMap<Villa, VillaDTOUpdate>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDTOCreate>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDTOUpdate>().ReverseMap();
        }
    }
}
