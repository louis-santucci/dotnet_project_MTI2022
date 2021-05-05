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
    public class TransactionRepository : Repository<Transaction, DTOTransaction>, ITransactionRepo
    {
        public TransactionRepository(FripShopContext context, ILogger<TransactionRepository> logger, IMapper mapper) : base(context, logger, mapper) { }
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
