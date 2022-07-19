using PagesCommon.DTOs;

namespace PagesServices.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Authenticate(UserLoginRequestDTO model);
    }

}
