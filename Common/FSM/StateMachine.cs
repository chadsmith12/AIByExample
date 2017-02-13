using Common.Entities;
using System.Diagnostics;
using Common.Messaging;

namespace Common.FSM
{
    /// <summary>
    /// Encapsulates all the state related data.
    /// Entities will delegtae the managment of current states to this state machine.
    /// Keeps track of the curent states, global states, and the previous states.
    /// </summary>
    /// <typeparam name="TEntityType">The entity type</typeparam>
    public class StateMachine<TEntityType> where TEntityType : BaseGameEntity 
    {
        #region Private Fields
        // entity that owns this current instance
        private readonly TEntityType _owner;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the state machine.
        /// Sets all the states to null by default, and sets the owner for this state machine.
        /// </summary>
        /// <param name="owner">The owner</param>
        public StateMachine(TEntityType owner)
        {
            _owner = owner;
            CurrentState = null;
            PreviousState = null;
            GlobalState = null;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Call to update the state machine.
        /// Calls the execute functions for the global and/or current states
        /// </summary>
        public void Update()
        {
            GlobalState?.Execute(_owner);
            CurrentState?.Execute(_owner);
        }

        /// <summary>
        /// Changes to the given new state.
        /// The current state before this will become the previous state and exits the state.
        /// Sets the current state to the new state passed in and enters the state.
        /// </summary>
        /// <param name="newState">The new state to change too.</param>
        public void ChangeState(IState<TEntityType> newState)
        {
            Debug.Assert(newState != null, "StateMachine.ChangeState: trying to change to a null state");

            PreviousState = CurrentState;
            CurrentState.Exit(_owner);

            CurrentState = newState;
            CurrentState.Enter(_owner);
        }

        /// <summary>
        /// Changes the state back to the state that was previous to this
        /// </summary>
        public void RevertToPreviousState()
        {
            ChangeState(PreviousState);
        }

        /// <summary>
        /// Checks if current state is of the type of the IState passed in
        /// </summary>
        /// <param name="state"></param>
        /// <returns>true if the current state's type is equal to the type of the object passed in, otherwise; false</returns>
        public bool IsInState(IState<TEntityType> state)
        {
            return state.GetType() == CurrentState.GetType();
        }

        public bool HandleMessage(Telegram message)
        {
            // first check if the current state is valid and it can handle this message
            if (CurrentState != null && CurrentState.OnMessage(_owner, message))
            {
                return true;
            }

            // if not check if the global state has been implemented and send the message to it
            if (GlobalState != null && GlobalState.OnMessage(_owner, message))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gives the type of the current state as a string.
        /// This can be used for debugging purposes.
        /// </summary>
        /// <returns>The curent state type as a string.</returns>
        public override string ToString()
        {
            return nameof(CurrentState);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the current state for the state machine.
        /// </summary>
        public IState<TEntityType> CurrentState { get; set; }

        /// <summary>
        /// Gets or sets the previoius state for the state machine.
        /// </summary>
        public IState<TEntityType> PreviousState { get; set; }

        /// <summary>
        /// Gets or sets the global state for the state machine.
        /// </summary>
        public IState<TEntityType> GlobalState { get; set; }

        #endregion
    }
}
