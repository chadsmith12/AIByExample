using System;
using System.Collections.Generic;
using System.Linq;
using Common.Entities;

namespace Common.Messaging
{
    /// <summary>
    /// Handles the sending of messages between two entities. This handles both messages that are to be sent immediately or with a delay.
    /// Messages that are to be sent at a later date are stored inside of a priority queue to keep the messages sorted based on their precedence (when should be send).
    /// Messages are send with respect to their time stamp.
    /// </summary>
    public static class MessageDispatcher
    {
        #region Constants        
        /// <summary>
        /// Signals that we should send the message immediately.
        /// </summary>
        public const double SendMessageImmediately = 0.0;

        /// <summary>
        /// Signals that the sender id of the messageof irrelevant.
        /// </summary>
        public const int SenderIdIrrelevant = -1;
        #endregion
        #region Private Fields

        private static readonly SortedSet<Telegram> PriorityQueue = new SortedSet<Telegram>();

        // Helper method that is utilized by DispatchMessage or DispatchDelayedMessages.
        // Calls the message handling member function of the receiving entity with the newly created telegram.
        private static void Discharge(BaseGameEntity entity, Telegram message)
        {
            if (!entity.HandleMessage(message))
            {
#if DEBUG
                Console.WriteLine("Message not handled...");
#endif
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Called when an entity needs to send a message to another entity.
        /// If the message is to be sent immediately then delay must be &lt; OR == 0.
        /// </summary>
        /// <param name="delay">The delay.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="receiver">The receiver.</param>
        /// <param name="message">The message.</param>
        /// <param name="extraInfo">The extra information to store inside the Telegram object to tbe sent</param>
        public static void SendMessage(double delay, int sender, int receiver, int message, object extraInfo)
        {
            var entityReceive = EntityManager.Instance.GetEntityById(receiver);
            var telegram = new Telegram(0, sender, receiver, message, extraInfo);

            // there is no delay go ahead and send the message
            if (delay <= 0.0)
            {
                Discharge(entityReceive, telegram);
            }
            // calculate the time when the message should be sent
            else
            {
                var currentTime = DateTime.Now.Ticks;
                telegram.DispatchTime = currentTime + delay;

                PriorityQueue.Add(telegram);
            }
        }

        /// <summary>
        /// Sends the delayed messages stored inside of the priority queue.
        /// The stored messages are examined each update step and checks to front of the priority queue to see if any messages have expired time stapmps.
        /// </summary>
        public static void SendDelayedMessages()
        {
            var currentTime = DateTime.Now.Ticks;

            // peek at the queue to see if any messages need dispatching.
            // remove the messages from the front of the queue that have expired time stamps
            while ((PriorityQueue.FirstOrDefault().DispatchTime < currentTime) && (PriorityQueue.FirstOrDefault().DispatchTime > 0))
            {
                var telegram = PriorityQueue.FirstOrDefault();
                var receiver = EntityManager.Instance.GetEntityById(telegram.Receiver);

                // send the message to the receiver
                Discharge(receiver, telegram);

                PriorityQueue.Remove(PriorityQueue.FirstOrDefault());
            }
        }

        #endregion
    }
}
