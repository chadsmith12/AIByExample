using Common.Entities;
using Common.Messaging;

namespace Common.FSM
{
    public interface IState<in T> where T : BaseGameEntity
    {
        /// <summary>
        /// Runs when the state is entered.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        void Enter(T entityType);

        /// <summary>
        /// Runs when a state is updated.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        void Execute(T entityType);

        /// <summary>
        /// Runs when state will exit.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        void Exit(T entityType);

        /// <summary>
        /// Called when the entity receives a message from the message dispatcher.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="telegram">The telegram.</param>
        /// <returns></returns>
        bool OnMessage(T entity, Telegram telegram);
    }
}
