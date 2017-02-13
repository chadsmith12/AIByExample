using System.Diagnostics;
using Common.Messaging;
using Common.System;

namespace Common.Entities
{
    public abstract class BaseGameEntity
    {
        #region Private Fields
        private int _id;
        private static int _nextValidId;
        #endregion

        #region Protected Fields

        protected Vector2D _position;
        protected Vector2D _scale;
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
        /// Updates the entity with the specified time elapsed.
        /// </summary>
        /// <param name="timeElapsed">The time elapsed.</param>
        public virtual void Update(double timeElapsed)
        {
            
        }

        public void SetScale(Vector2D value)
        {
        }

        public void SetScale(double value)
        {
            
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>True if the message handled, otherwise; false</returns>
        public virtual bool HandleMessage(Telegram message)
        {
            return false;
        }

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

        /// <summary>
        /// Gets or sets the type of the entity.
        /// This is returned as a int code of the entity type that can be converted to your specified enum.
        /// </summary>
        /// <value>
        /// The type of the entity.
        /// </value>
        public int EntityType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this Entity is tagged.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this Entity is tagged; otherwise, <c>false</c>.
        /// </value>
        public bool IsTagged { get; set; }

        /// <summary>
        /// Gets the scale of the Entity.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public Vector2D Scale => _scale;

        #endregion
    }
}
