using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Model.DTO
{
    public class VillaDTO 
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

    }
}
