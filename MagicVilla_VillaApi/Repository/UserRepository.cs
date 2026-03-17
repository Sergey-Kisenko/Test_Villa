using AutoMapper;
using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Repository.Interfaces;

namespace MagicVilla_VillaApi.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDBContext db;
        private readonly IMapper mapper;
        public UserRepository(ApplicationDBContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public bool IsUniqueUser(string username)
        {
            if (db.LocalUsers.FirstOrDefault(x => x.UserName == username) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            LocalUser newUser = new LocalUser{
                Name = registerationRequestDTO.Name,
                Password = registerationRequestDTO.Password,
                Role = registerationRequestDTO.Role,
                UserName = registerationRequestDTO.UserName
            };   
            db.LocalUsers.Add(newUser);
            await db.SaveChangesAsync();
            newUser.Password = "";
            return newUser;
        }
    }
}
