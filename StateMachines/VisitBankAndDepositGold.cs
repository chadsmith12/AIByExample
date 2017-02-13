using Common.FSM;
using System;
using Common.Messaging;

namespace StateMachines
{
    public sealed class VisitBankAndDepositGold : IState<Miner>
    {
        private static readonly Lazy<VisitBankAndDepositGold> Lazy = new Lazy<VisitBankAndDepositGold>(() => new VisitBankAndDepositGold());

        private VisitBankAndDepositGold()
        {

        }

        public void Enter(Miner miner)
        {
            // if we are alreay at the bank we don't need to change locations, just return
            if (miner.LocationType == LocationType.Bank)
            {
                return;
            }

            Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: Goin' to the bank. Yes siree!");
            miner.LocationType = LocationType.Bank;
        }

        public void Execute(Miner miner)
        {
            // we need to deposit gold
            miner.AddToWealth(miner.GoldCarried);
            miner.GoldCarried = 0;

            Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: Depositing Gold. Total Savings Now: {miner.Wealth}");
            // can we earn a rest yet?
            if (miner.Wealth >= Constants.ComfortLevel)
            {
                Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: WoooHoo! Rich enough for now! Back home to mah lil' lady!");
                miner.StateMachine.ChangeState(GoHomeAndSleepTillRested.Instance);
            }
            else
            {
                miner.StateMachine.ChangeState(EnterMineAndDigForNugget.Instance);
            }
        }

        public void Exit(Miner miner)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: Leavin' the bank");
        }

        public bool OnMessage(Miner entity, Telegram telegram)
        {
            return false;
        }

        public static VisitBankAndDepositGold Instance => Lazy.Value;
    }
}
