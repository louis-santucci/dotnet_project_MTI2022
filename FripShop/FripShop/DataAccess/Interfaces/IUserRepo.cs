using System;
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
        /// <summary>
        /// Gets a user in function of its email
        /// </summary>
        /// <param name="email">The email of the wanted user</param>
        /// <returns>The wanted user, null otherwise</returns>
        DTOUser GetUserByEmail(string email);

        /// <summary>
        /// Gets a user in function of its username
        /// </summary>
        /// <param name="userName">The username of the wan,ted user</param>
        /// <returns>The wanted user, null otherwise</returns>
        DTOUser GetUserByUserName(string userName);
    }
}
