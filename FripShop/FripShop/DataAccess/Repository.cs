using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess.Interfaces;

namespace FripShop.DataAccess
{
    public class Repository<DbEntity, ModelEntity> : IRepo<DbEntity, ModelEntity>
    {
        public Task<IEnumerable<ModelEntity>> Get(string includeString = "")
        {
            throw new NotImplementedException();
        }

        public Task<ModelEntity> Insert(ModelEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<ModelEntity> Update(ModelEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(long idEntity)
        {
            throw new NotImplementedException();
        }
    }
}
