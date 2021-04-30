﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.Dbo;
using Microsoft.Extensions.Logging;

namespace FripShop.DataAccess
{
    public class UserRepository : Repository<User, DboUser>, IUserRepo
    {
        public UserRepository(FripShopContext context, ILogger<UserRepository> logger, IMapper mapper) : base(context, logger, mapper) {}
    }
}
