using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FripShop.DataAccess.Interfaces
{
    public interface IRepo<DbEntity, ModelEntity>
    {
        Task<IEnumerable<ModelEntity>> Get(string includeString = "");
        Task<ModelEntity> Insert(ModelEntity entity);
        Task<ModelEntity> Update(ModelEntity entity);
        Task<bool> Delete(long idEntity);
    }
}
