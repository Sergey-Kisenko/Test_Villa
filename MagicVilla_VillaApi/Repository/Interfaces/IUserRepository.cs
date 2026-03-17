using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;

namespace MagicVilla_VillaApi.Repository.Interfaces
{
    public interface IUserRepository
    {
        public bool IsUniqueUser(string username);
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        public Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
