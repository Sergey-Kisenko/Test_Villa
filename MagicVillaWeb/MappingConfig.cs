using AutoMapper;
using MagicVillaWeb.Model.DTO;

namespace MagicVillaWeb
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, VillaDTOCreate>().ReverseMap();
            CreateMap<VillaDTO, VillaDTOUpdate>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaDTOCreate>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberDTOUpdate>().ReverseMap();
        }
    }
}
