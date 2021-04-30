using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess.EFModels;
using FripShop.Dbo;

namespace FripShop.DataAccess.Interfaces
{
    public interface IUserRepo : IRepo<User, DboUser> { }
}
