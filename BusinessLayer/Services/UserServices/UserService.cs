using AutoMapper;
using DataAccessLayer.Abstract;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task AddUserAsync(AddUserRequest addUserRequest)
        {
            var userEntity = _mapper.Map<User>(addUserRequest);
            await _userRepository.Add(userEntity);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                _userRepository.Delete(user);
            }
        }

        public async Task<List<DisplayUserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<DisplayUserResponse>>(users);
        }

        public async Task<GetByIdUserResponse> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetById(id);
            return _mapper.Map<GetByIdUserResponse>(user);
        }

        public async Task<User> TGetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task UpdateUserAsync(UpdateUserRequest updateUserRequest)
        {
            var userEntity = await _userRepository.GetById(updateUserRequest.Id);
            if (userEntity != null)
            {
                _mapper.Map(updateUserRequest, userEntity);
                _userRepository.Update(userEntity);
            }
        }
    }
}
