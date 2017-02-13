using System;

namespace StateMachines
{
    public class Constants
    {
        public const int ComfortLevel = 5;
        public const int MaxNuggets = 3;
        public const int ThirstLevel = 5;
        public const int TirednessThreshold = 5;

        /// <summary>
        /// Gets the name of entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>the entit name as a string</returns>
        /// <exception cref="ArgumentOutOfRangeException">entity - null</exception>
        public static string GetNameOfEntity(Entity entity)
        {
            switch (entity)
            {
                case Entity.MinerBob:
                    return "Miner Bob";
                case Entity.Elsa:
                    return "Elsa";
                default:
                    throw new ArgumentOutOfRangeException(nameof(entity), entity, null);
            }
        }
    }

    public enum Entity
    {
        MinerBob = 1,
        Elsa = 2
    }

    public enum MessageType
    {
        MsgHiHoneyImHome = 1,
        MsgStewReady = 2
    }
}
