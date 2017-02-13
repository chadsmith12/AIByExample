using System;

namespace Common.Messaging
{
    /// <summary>
    /// Structure to store all the relevant information that is sent in a message between two entities.
    /// Holds who sent it, who is receiving it, what the message is (type), time stamp, and more.
    /// Implements <see cref="IComparable{T}"/> and <seealso cref="IComparable"/>
    /// </summary>
    public struct Telegram : IComparable<Telegram>, IComparable
    {
        #region Public Fields
        public const double SmallestDelay = 0.25;
        /// <summary>
        /// The entity that sent this telegram.
        /// </summary>
        public int Sender;
        /// <summary>
        /// The entity that is to receive the telegram.
        /// </summary>
        public int Receiver;
        /// <summary>
        /// The message type
        /// </summary>
        public int MessageType;
        /// <summary>
        /// The time deplay that the message should be dispatched.
        /// </summary>
        public double DispatchTime;
        /// <summary>
        /// The extra information that may accompany the message
        /// </summary>
        public object ExtraInfo;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of a <see cref="Telegram"/> to get it ready to send in a message.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="receiver">The receiver.</param>
        /// <param name="message">The message.</param>
        /// <param name="extraInfo">The extra information.</param>
        public Telegram(double time, int sender, int receiver, int message, object extraInfo = null)
        {
            DispatchTime = time;
            Sender = sender;
            Receiver = receiver;
            MessageType = message;
            ExtraInfo = extraInfo;
        }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this telegram.
        /// String is in the format of Time Sender Receiver Message.
        /// Good for debugging to print out telegram information.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this telegram.
        /// </returns>
        public override string ToString()
        {
            return $"Time: {this.DispatchTime} Sender: {this.Sender} Receiver: {this.Receiver} Message: {this.MessageType}";
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(Telegram other)
        {
            return Sender == other.Sender && Receiver == other.Receiver && MessageType == other.MessageType && (Math.Abs(DispatchTime - other.DispatchTime) < SmallestDelay) && Equals(ExtraInfo, other.ExtraInfo);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Telegram && Equals((Telegram)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Sender;
                hashCode = (hashCode * 397) ^ Receiver;
                hashCode = (hashCode * 397) ^ MessageType;
                hashCode = (hashCode * 397) ^ DispatchTime.GetHashCode();
                hashCode = (hashCode * 397) ^ (ExtraInfo?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
        #endregion

        #region IComparable        
        /// <summary>
        /// Compares the current instance with another Telegram of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">Telegram to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />. Greater than zero This instance follows <paramref name="other" /> in the sort order.
        /// </returns>
        public int CompareTo(Telegram other)
        {
            // returns < 0 if less than, 0 if ==, and > 0 if greater than
            if (this > other)
                return 1;

            if (this < other)
                return -1;

            return 0;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj.GetType() != GetType())
                return -1;

            return CompareTo((Telegram) obj);
        }
        #endregion

        #region Overloaded Operators
        /// <summary>
        /// Implements the operator ==.
        /// Two telegrams dispatch times must be smaller than SmallestDelay before they are considered unique.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Telegram lhs, Telegram rhs)
        {
            return (Math.Abs(lhs.DispatchTime - rhs.DispatchTime) < SmallestDelay) && (lhs.Sender == rhs.Sender) && (lhs.Receiver == rhs.Receiver) && (lhs.MessageType == rhs.MessageType);
        }

        /// <summary>
        /// Implements the operator !=.
        /// Uses the overloaded == operator with a ! to check if two telegrams !=.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Telegram lhs, Telegram rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// Checks if the dispatch time of the lhs is &lt; the dispatch time of the rhs.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(Telegram lhs, Telegram rhs)
        {
            if (lhs == rhs)
                return false;

            return lhs.DispatchTime < rhs.DispatchTime;
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// Checks if the dispatch time of the lhs is &gt; the dispatch time of the rhs.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(Telegram lhs, Telegram rhs)
        {
            if (lhs == rhs)
                return false;

            return lhs.DispatchTime > rhs.DispatchTime;
        }
        #endregion
    }
}