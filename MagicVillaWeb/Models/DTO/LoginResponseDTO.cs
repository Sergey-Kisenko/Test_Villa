using MagicVillaWeb.Models.DTO;

namespace MagicVillaWeb.Model.DTO
{
    public class LoginResponseDTO //
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
