using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.API.Dtos;
using UserManagement.API.Models;

namespace UserManagement.API.AutoMapperProfiles
{
    public class UserAutoMapperProfiles: Profile
    {
        public UserAutoMapperProfiles()
        {
            //CreateMap<List<UserResultDto>, List<User>>();
            CreateMap<User,UserResultDto > ();
        }
    }
}
