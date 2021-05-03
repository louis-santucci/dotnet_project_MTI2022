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
        /// <summary>
        /// Get all the DTO entities of the database
        /// </summary>
        /// <param name="includeString">Some entities to add to the final query</param>
        /// <returns>An enumerable list of entities present in the database </returns>
        Task<IEnumerable<ModelEntity>> Get(string includeString = "");
        
        /// <summary>
        /// Inserts a new DTO entity in the database
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The new created entity, or null if failed</returns>
        Task<ModelEntity> Insert(ModelEntity entity);
        
        /// <summary>
        /// Updates an existing DTO entity in the database
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The updated entity, or null if failed</returns>
        Task<ModelEntity> Update(ModelEntity entity);
        
        /// <summary>
        /// Deletes an existing DTO entity in the database
        /// </summary>
        /// <param name="idEntity">The ID of the DTO entity to delete</param>
        /// <returns>True if deleted, false if failed</returns>
        Task<bool> Delete(long idEntity);

        /// <summary>
        /// Get a DTO entity in function of its ID
        /// </summary>
        /// <param name="id">The ID of the wanted DTO entity</param>
        /// <returns>The wanted entity if found, null otherwise</returns>
        Task<ModelEntity> GetById(long id);
    }
}
