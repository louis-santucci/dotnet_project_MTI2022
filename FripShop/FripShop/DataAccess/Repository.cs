using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FripShop.DataAccess
{
    public class Repository<DbEntity, ModelEntity> : IRepo<DbEntity, ModelEntity>
        where DbEntity : class, new()
        where ModelEntity : class, Dbo.IDbo, new()
    {

        private DbSet<DbEntity> _set;
        protected FripShopContext _context;
        protected ILogger _logger;
        protected IMapper _mapper;

        public Repository(FripShopContext context, ILogger logger, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _set = _context.Set<DbEntity>();
        }

        public virtual async Task<IEnumerable<ModelEntity>> Get(string includeString = "")
        {
            try
            {
                List<DbEntity> query = null;
                if (String.IsNullOrEmpty(includeString))
                {
                    query = await _set.AsNoTracking().ToListAsync();
                }
                else
                {
                    query = await _set.Include(includeString).AsNoTracking().ToListAsync();
                }

                return _mapper.Map<ModelEntity[]>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- Get() -- Error on db : ", ex);
                return null;
            }
        }

        public virtual async Task<ModelEntity> Insert(ModelEntity entity)
        {
            DbEntity dbEntity = _mapper.Map<DbEntity>(entity);
            _set.Add(dbEntity);
            try
            {
                await _context.SaveChangesAsync();
                ModelEntity newEntity = _mapper.Map<ModelEntity>(dbEntity);
                return newEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- Insert() -- Error on db : ", ex);
                return null;
            }

        }

        public virtual async Task<ModelEntity> Update(ModelEntity entity)
        {
            DbEntity dbEntity = _set.Find(entity.Id);


            if (dbEntity == null)
            {
                return null;
            }
            _mapper.Map(entity, dbEntity);
            if (!_context.ChangeTracker.HasChanges())
            {
                return entity;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- Update() -- Error on db : ", ex);

                return null;
            }
            return _mapper.Map<ModelEntity>(dbEntity);

        }

        public virtual async Task<bool> Delete(long idEntity)
        {
            DbEntity dbEntity = _set.Find(idEntity);


            if (dbEntity == null)
            {
                return false;
            }
            _set.Remove(dbEntity);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- Delete() -- Error on db : ", ex);
                return false;
            }
        }

        public Task<int> Count()
        {
            try
            {
                return _set.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- Count() -- Error on db : ", ex);
                return null;
            }
        }
    }
}
