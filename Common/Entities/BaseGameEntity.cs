using System.Diagnostics;
using Common.Messaging;

namespace Common.Entities
{
    public abstract class BaseGameEntity
    {
        #region Private Fields
        private int _id;
        private static int _nextValidId;
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseGameEntity"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected BaseGameEntity(int id)
        {
            Id = id;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Updates this entity.
        /// Gets called each step and is used by each entity to update their state.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>True if the message handled, otherwise; false</returns>
        public abstract bool HandleMessage(Telegram message);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the identifier for this entity.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                Debug.Assert(value >= _nextValidId, "BaseGameEntity.Id - Invalid Id - Can't Set");
                _id = value;
                _nextValidId = _id + 1;
            }
        }
        #endregion
    }
}
