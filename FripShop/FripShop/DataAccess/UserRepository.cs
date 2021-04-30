using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.Extensions.Logging;

namespace FripShop.DataAccess
{
    public class UserRepository : Repository<User, DTOUser>, IUserRepo
    {
        public UserRepository(FripShopContext context, ILogger<UserRepository> logger, IMapper mapper) : base(context,
            logger, mapper)
        {}

        public DTOUser GetUserByEmail(string email)
        {
            var result = this._set.Single(c => c.Email == email);
            if (result != null)
                return _mapper.Map<DTOUser>(result);
            return null;
        }

        public DTOUser GetUserByUserName(string userName)
        {
            var result = this._set.Single(c => c.UserName == userName);
            if (result != null)
                return _mapper.Map<DTOUser>(result);
            return null;
        }
    }
}
