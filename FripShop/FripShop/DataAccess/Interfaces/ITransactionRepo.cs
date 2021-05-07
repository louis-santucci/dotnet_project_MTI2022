using FripShop.DataAccess.EFModels;
using FripShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.DataAccess.Interfaces
{
    public interface ITransactionRepo : IRepo<Transaction, DTOTransaction>
    {
        /// <summary>
        /// Get all DTO transactions in function of the ID of the DTO user
        /// </summary>
        /// <param name="id">The ID of the wanted user</param>
        /// <returns>The list of wanted DTO transactions</returns>
        public Task<IEnumerable<DTOTransaction>> GetTransactionByUserId(long id);
    }
}
