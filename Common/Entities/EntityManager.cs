using System;
using System.Collections.Generic;

namespace Common.Entities
{
    /// <summary>
    /// Acts as a database of the instantiated entities.
    /// References to entities are cross referenced by their id.
    /// Is a singleton class.
    /// </summary>
    public class EntityManager
    {
        #region Private Fields

        private readonly Dictionary<int, BaseGameEntity> _entityMap;
        #endregion

        public static readonly Lazy<EntityManager> Lazy = new Lazy<EntityManager>(() => new EntityManager());

        private EntityManager()
        {
            _entityMap = new Dictionary<int, BaseGameEntity>();
        }

        #region Public Methods

        /// <summary>
        /// Registers the entity into the internal database.
        /// Uses the id of the entity as a key.
        /// </summary>
        /// <param name="newEntity">The new entity.</param>
        public void RegisterEntity(BaseGameEntity newEntity)
        {
            _entityMap.Add(newEntity.Id, newEntity);
        }

        /// <summary>
        /// Gets the entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity found.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public BaseGameEntity GetEntityById(int id)
        {
            if(!_entityMap.ContainsKey(id))
                throw new KeyNotFoundException($"Invalid Id: {id} Not Found in EntityManager");

            return _entityMap[id];
        }
        #endregion

        /// <summary>
        /// Gets the instance of the entity manager.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static EntityManager Instance => Lazy.Value;
    }
}
