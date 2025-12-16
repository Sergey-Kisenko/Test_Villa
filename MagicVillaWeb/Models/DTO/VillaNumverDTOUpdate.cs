namespace MagicVillaWeb.Model.DTO
{
    public class VillaNumberDTOUpdate
    {
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime DateTimeUpdate { get; set; }
        public int VillaId { get; set; }
        public List<VillaDTO> villaDTOList { get; set; }

    }
}
