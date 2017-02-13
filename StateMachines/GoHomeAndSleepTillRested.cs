using Common.FSM;
using System;
using Common.Messaging;

namespace StateMachines
{
    public sealed class GoHomeAndSleepTillRested : IState<Miner>
    {
        public static readonly Lazy<GoHomeAndSleepTillRested> Lazy = new Lazy<GoHomeAndSleepTillRested>(() => new GoHomeAndSleepTillRested());

        private GoHomeAndSleepTillRested()
        {

        }
        public void Enter(Miner miner)
        {
            if(miner.LocationType == LocationType.Shack)
            {
                return;
            }

            Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: Walkin' Home");
            miner.LocationType = LocationType.Shack;

            // let the wife know I'm home
            MessageDispatcher.SendMessage(MessageDispatcher.SendMessageImmediately, miner.Id, (int)Entity.Elsa, (int)MessageType.MsgHiHoneyImHome, null);
        }

        public void Execute(Miner miner)
        {
            // if the miner is not fatigued go ahead and start digging again
            if(!miner.IsFatigued)
            {
                Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: What a God Darn fantastic nap! Time to find more gold!");
                miner.StateMachine.ChangeState(EnterMineAndDigForNugget.Instance);
            }
            else
            {
                // we are still sleeping
                miner.DecreaseFatigue();
                Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: zzZZzz");
            }
        }

        public void Exit(Miner miner)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: Leaving the house");
        }

        public bool OnMessage(Miner entity, Telegram telegram)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            switch ((MessageType) telegram.MessageType)
            {
                case MessageType.MsgStewReady:
                    Console.Write($"\nMessage handled by {Constants.GetNameOfEntity((Entity) entity.Id)} at {DateTime.Now:h:mm:ss tt zz}");
                    Console.Write($"\n{Constants.GetNameOfEntity((Entity) entity.Id)}: Okay Hun, ahm a comin'!");
                    entity.StateMachine.CurrentState = EatStew.Instance;
                    return true;
                default:
                    return false;
            }
        }

        public static GoHomeAndSleepTillRested Instance => Lazy.Value;
    }
}
