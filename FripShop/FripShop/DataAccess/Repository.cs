using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FripShop.DataAccess.EFModels;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FripShop.DataAccess
{
    /// <summary>
    /// Generic class for repositories
    /// </summary>
    /// <typeparam name="DbEntity"></typeparam>
    /// <typeparam name="ModelEntity"></typeparam>
    public class Repository<DbEntity, ModelEntity> : IRepo<DbEntity, ModelEntity>
        where DbEntity : class, new()
        where ModelEntity : class, IDTO, new()
    {

        protected DbSet<DbEntity> _set;
        protected FripShopContext _context;
        protected ILogger _logger;
        protected IMapper _mapper;

        /// <summary>
        /// Generic Repository contructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public Repository(FripShopContext context, ILogger logger, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _set = _context.Set<DbEntity>();
        }

        /// <summary>
        /// Generic Repository Get
        /// </summary>
        /// <param name="includeString"></param>
        /// <returns></returns>
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
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- Get() -- Error : ", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Generic Repository Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- Insert() -- Error : ", ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Generic Repository Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- Update() -- Error : ", ex.Message);

                return null;
            }
            return _mapper.Map<ModelEntity>(dbEntity);

        }

        /// <summary>
        /// Generic Repository Delete
        /// </summary>
        /// <param name="idEntity"></param>
        /// <returns></returns>
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
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- Delete() -- Error : ", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Generic Repository Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ModelEntity> GetById(long id)
        {
            try
            {
                var result = await _set.FindAsync(id);
                if (result != null)
                    return _mapper.Map<ModelEntity>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- GetById() -- Error : ", ex.Message);
                return null;
            }

            return null;
        }

        /// <summary>
        /// Generic Repository Count
        /// </summary>
        /// <returns></returns>
        public Task<int> Count()
        {
            try
            {
                return _set.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"REPOSITORY {typeof(ModelEntity)} -- Count() -- Error : ", ex.Message);
                return null;
            }
        }
    }
}
