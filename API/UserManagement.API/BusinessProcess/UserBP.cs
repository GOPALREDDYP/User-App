using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.API.Data;
using UserManagement.API.Dtos;
using UserManagement.API.Models;

namespace UserManagement.API.BusinessProcess
{
    public class UserBP : IUserBP
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserBP(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;

        }

        public List<UserResultDto> AddUser(string firstName, string lastName)
        {
            try
            {
                List<User> existingUsers = _userRepository.GetUsers();

                if (existingUsers.Any(u => u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"The user Name : {firstName} {lastName}  is already exist,");
                }

                User user = new Models.User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                };

                existingUsers.Add(user);

                List<User> result = _userRepository.AddUser(existingUsers);
                return _mapper.Map<List<UserResultDto>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserResultDto> GetUsers()
        {
            List<UserResultDto> result = null;
            try
            {

                List<User> users = _userRepository.GetUsers();

                if (users.Any())
                {
                    result = _mapper.Map<List<UserResultDto>>(users);
                }

                return result ?? new List<UserResultDto>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
