using Common.FSM;
using System;
using Common.Messaging;

namespace StateMachines
{
    /// <summary>
    /// State for the miner to enter the mine and dig for a nugget.
    /// In this state the miner should change location to be at the gold mine.
    /// This should happen until his pockets are full.
    /// </summary>
    public sealed class EnterMineAndDigForNugget : IState<Miner>
    {
        private static readonly Lazy<EnterMineAndDigForNugget> Lazy = new Lazy<EnterMineAndDigForNugget>(() => new EnterMineAndDigForNugget());

        private EnterMineAndDigForNugget()
        {

        }

        /// <summary>
        /// Runs when the state is entered.
        /// Changes the location to GoldMine
        /// </summary>
        /// <param name="miner">The miner.</param>
        public void Enter(Miner miner)
        {
            // we are already at the gold mine, do nothing
            if (miner.LocationType == LocationType.GoldMine) return;

            Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: Walkin' to the gold mine!");
            miner.LocationType = LocationType.GoldMine;
        }

        /// <summary>
        /// Runs when a state is updated.
        /// Digs forgold until tired or has enough nuggets.
        /// </summary>
        /// <param name="miner">The miner.</param>
        public void Execute(Miner miner)
        {
            // miner needs to dig for gold until he is carrying too much
            // if he gets tired he stops and changes states
            miner.AddToGoldCarried(1);
            miner.IncreaseFatigue();

            Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: Pickin' up a nugget!");

            // enough gold go to the bank
            if (miner.IsPocketsFull)
            {
                miner.StateMachine.ChangeState(VisitBankAndDepositGold.Instance);
            }

            // thirsty go and get whiskey
            if (miner.IsThirsty)
            {
                miner.StateMachine.ChangeState(QuenchThirst.Instance);
            }
        }

        /// <summary>
        /// Runs when state will exit.
        /// </summary>
        /// <param name="miner">The miner.</param>
        public void Exit(Miner miner)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: Ah'm leavin' the gold mine with mah pockets full o' sweet gold!");
        }

        public bool OnMessage(Miner entity, Telegram telegram)
        {
            return false;
        }

        public static EnterMineAndDigForNugget Instance => Lazy.Value;
    }
}
