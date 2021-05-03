using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.DataAccess.Interfaces
{
    /// <summary>
    /// Generic interface for repositories
    /// </summary>
    /// <typeparam name="DbEntity">The EntityFrameworkCore model</typeparam>
    /// <typeparam name="ModelEntity">The DTO model</typeparam>
    public interface IRepo<DbEntity, ModelEntity>
    {
        Task<IEnumerable<ModelEntity>> Get(string includeString = "");
        Task<ModelEntity> Insert(ModelEntity entity);
        Task<ModelEntity> Update(ModelEntity entity);
        Task<bool> Delete(long idEntity);

        Task<ModelEntity> GetById(long id);
        Task<int> Count();
    }
}
