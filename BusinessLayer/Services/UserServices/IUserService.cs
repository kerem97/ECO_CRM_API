using DtoLayer.User.Requests;
using DtoLayer.User.Responses;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.UserServices
{
    public interface IUserService
    {
        Task<List<DisplayUserResponse>> GetAllUsersAsync();
        Task<GetByIdUserResponse> GetUserByIdAsync(int id);
        Task AddUserAsync(AddUserRequest addUserRequest);
        Task UpdateUserAsync(UpdateUserRequest updateUserRequest);
        Task DeleteUserAsync(int id);
        Task<User> TGetUserByUsernameAsync(string username);
    }
}
