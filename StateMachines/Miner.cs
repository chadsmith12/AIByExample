using Common.Entities;
using Common.FSM;
using Common.Messaging;

namespace StateMachines
{
    public class Miner : BaseGameEntity
    {
        #region Private Fields
        private readonly StateMachine<Miner> _stateMachine;
        private int _thirst;
        private int _fatigue;
        #endregion

        #region Constructor
        public Miner(int id) : base(id)
        {
            LocationType = LocationType.Shack;
            Wealth = 0;
            _thirst = 0;
            _fatigue = 0;

            // setup a new state machine
            _stateMachine = new StateMachine<Miner>(this)
            {
                CurrentState = GoHomeAndSleepTillRested.Instance
            };
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates this entity.
        /// Gets called each step and is used by each entity to update their state.
        /// </summary>
        public override void Update()
        {
            ++_thirst;
            _stateMachine.Update();
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// True if the message handled, otherwise; false
        /// </returns>
        public override bool HandleMessage(Telegram message)
        {
            return _stateMachine.HandleMessage(message);
        }

        /// <summary>
        /// Adds to gold carried.
        /// </summary>
        /// <param name="gold">The gold.</param>
        public void AddToGoldCarried(int gold)
        {
            GoldCarried += gold;

            if (GoldCarried < 0)
                GoldCarried = 0;
        }

        /// <summary>
        /// Adds to wealth of the miner.
        /// </summary>
        /// <param name="value">The value.</param>
        public void AddToWealth(int value)
        {
            Wealth += value;

            if (Wealth < 0)
                Wealth = 0;
        }

        /// <summary>
        /// Increases the fatigue.
        /// </summary>
        public void IncreaseFatigue()
        {
            _fatigue += 1;
        }

        /// <summary>
        /// Decreases the fatigue.
        /// </summary>
        public void DecreaseFatigue()
        {
            _fatigue -= 1;
        }

        /// <summary>
        /// Quenches thirst by buying a whiskey for 2 gold
        /// </summary>
        public void BuyAndDrinkWhiskey()
        {
            _thirst = 0;
            Wealth -= 2;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the type of the location.
        /// </summary>
        /// <value>
        /// The type of the location.
        /// </value>
        public LocationType LocationType { get; set; }

        /// <summary>
        /// Gets or sets the gold carried.
        /// </summary>
        /// <value>
        /// The gold carried.
        /// </value>
        public int GoldCarried { get; set; }

        /// <summary>
        /// Gets a value indicating whether this miners pockets are full.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is pockets full; otherwise, <c>false</c>.
        /// </value>
        public bool IsPocketsFull => GoldCarried >= Constants.MaxNuggets;

        /// <summary>
        /// Gets a value indicating whether this miner is fatigued.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is fatigued; otherwise, <c>false</c>.
        /// </value>
        public bool IsFatigued => _fatigue > Constants.TirednessThreshold;

        /// <summary>
        /// Gets or sets the wealth.
        /// </summary>
        /// <value>
        /// The wealth.
        /// </value>
        public int Wealth { get; set; }

        /// <summary>
        /// Gets a value indicating whether this miner is thirsty.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is thirsty; otherwise, <c>false</c>.
        /// </value>
        public bool IsThirsty => _thirst >= Constants.ThirstLevel;

        /// <summary>
        /// Gets the state machine assosiated with the miner.
        /// </summary>
        public StateMachine<Miner> StateMachine => _stateMachine;
        #endregion
    }
}
