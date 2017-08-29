using Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.ViewModels
{
    public class UsersListViewModel
    {
        public UserDto[] UsersList { get; set; }
    }
}