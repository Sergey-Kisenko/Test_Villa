using MagicVilla_VillaApi.Model.DTO;

namespace MagicVilla_VillaApi.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> DBVillaStore = new List<VillaDTO> {
                new VillaDTO { Id=1, Name="Test1" }, new VillaDTO { Id=2, Name="Test2" }
            };
    }
}
