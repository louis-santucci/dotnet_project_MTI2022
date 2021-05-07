using AutoMapper;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.DataAccess
{
    /// <summary>
    /// Repository from Transaction
    /// </summary>
    public class TransactionRepository : Repository<Transaction, DTOTransaction>, ITransactionRepo
    {
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public TransactionRepository(FripShopContext context, ILogger<TransactionRepository> logger, IMapper mapper) : base(context, logger, mapper) { }
        
        /// <summary>
        /// Get a transaction from a user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Transaction</returns>
        public async Task<IEnumerable<DTOTransaction>> GetTransactionByUserId(long id)
        {
            try
            {
                var UserTransactions = _context.Transactions.Where(transaction => transaction.BuyerId == id).ToList();
                return _mapper.Map<IEnumerable<DTOTransaction>>(UserTransactions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY Transaction -- GetTransactionByUserId() -- Error : ", ex.Message);
                return null;
            }
        }
    }
}
