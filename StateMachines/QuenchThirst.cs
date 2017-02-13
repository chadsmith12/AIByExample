using Common.FSM;
using System;
using Common.Messaging;

namespace StateMachines
{
    public sealed class QuenchThirst : IState<Miner>
    {
        private static readonly Lazy<QuenchThirst> Lazy = new Lazy<QuenchThirst>(() => new QuenchThirst());

        private QuenchThirst()
        {

        }

        public void Enter(Miner miner)
        {
            // if we already at the saloon don't do anything
            if(miner.LocationType == LocationType.Saloon)
            {
                return;
            }

            miner.LocationType = LocationType.Saloon;
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: Boy, ah sure is thusty. Walkin' to the saloon");
        }

        public void Execute(Miner miner)
        {
            // we are still thirsty
            if(miner.IsThirsty)
            {
                miner.BuyAndDrinkWhiskey();
                Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: That's mighty fine sippin' liquer");
                miner.StateMachine.ChangeState(EnterMineAndDigForNugget.Instance);
            }
            else
            {
                Console.Write("\nERROR! \nERROR! \nERROR!");
            }
        }

        public void Exit(Miner miner)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)miner.Id)}: leavin' the saloon, feelin' good");
        }

        public bool OnMessage(Miner entity, Telegram telegram)
        {
            return false;
        }

        public static QuenchThirst Instance => Lazy.Value;
    }
}
