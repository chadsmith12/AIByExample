using System;
using Common.FSM;
using Common.Messaging;

namespace StateMachines
{
    public class DoHouseWork : IState<MinersWife>
    {
        public static Lazy<DoHouseWork> Lazy => new Lazy<DoHouseWork>(() => new DoHouseWork());

        private DoHouseWork()
        {
            
        }
        public void Enter(MinersWife entityType)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Time to do some more housework!");
        }

        public void Execute(MinersWife entityType)
        {
            var random = new Random();

            switch (random.Next(0, 3))
            {
                case 0:
                    Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Moppin' the floor");
                    break;
                case 1:
                    Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Washin' the dishes");
                    break;
                case 2:
                    Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Makin' the bed");
                    break;
            }
        }

        public void Exit(MinersWife entityType)
        {

        }

        public bool OnMessage(MinersWife entity, Telegram telegram)
        {
            return false;
        }

        public static DoHouseWork Instance => Lazy.Value;
    }
}
