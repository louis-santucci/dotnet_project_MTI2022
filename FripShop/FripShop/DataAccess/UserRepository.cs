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
    /// <summary>
    /// Repository for User
    /// </summary>
    public class UserRepository : Repository<User, DTOUser>, IUserRepo
    {
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public UserRepository(FripShopContext context, ILogger<UserRepository> logger, IMapper mapper) : base(context,
            logger, mapper)
        {}

        /// <summary>
        /// Get a user from its email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User</returns>
        public DTOUser GetUserByEmail(string email)
        {
            try
            {
                if (_set.Any() == false)
                    return null;
                var result = this._set.Where(c => c.Email == email);
                if (result.Any())
                    return _mapper.Map<DTOUser>(result.Single());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY User -- GetUserByEmail() -- Error : ", ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Get a user from its username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User</returns>
        public DTOUser GetUserByUserName(string userName)
        {
            try
            {
                if (_set.Any() == false)
                    return null;
                var result = this._set.Where(c => c.UserName == userName);
                if (result.Any())
                    return _mapper.Map<DTOUser>(result.Single());
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY User -- GetUserByUserName() -- Error : ", ex.Message);
                return null;
            }

        }
    }
}
