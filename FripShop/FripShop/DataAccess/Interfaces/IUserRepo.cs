﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess.EFModels;
using FripShop.DTO;

namespace FripShop.DataAccess.Interfaces
{
    /// <summary>
    /// Interface for user repository
    /// </summary>
    public interface IUserRepo : IRepo<User, DTOUser>
    {
        Task<DTOUser> GetUserByEmail(string email);
        Task<DTOUser> GetUserByUserName(string userName);
    }
}
