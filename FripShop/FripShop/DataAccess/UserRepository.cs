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
            if (_set.Count() == 0)
                return null;
            var result = this._set.Where(c => c.Email == email);
            if (result.Count() != 0)
                return _mapper.Map<DTOUser>(result.Single());
            return null;
        }

        public DTOUser GetUserByUserName(string userName)
        {
            if (_set.Count() == 0)
                return null;
            var result = this._set.Where(c => c.UserName == userName);
            if (result.Count() != 0)
                return _mapper.Map<DTOUser>(result.Single());
            return null;
        }
    }
}
