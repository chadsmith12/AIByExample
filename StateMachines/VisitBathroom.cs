using System;
using Common.FSM;
using Common.Messaging;

namespace StateMachines
{
    public class VisitBathroom : IState<MinersWife>
    {
        public static Lazy<VisitBathroom> Lazy = new Lazy<VisitBathroom>(() => new VisitBathroom());

        private VisitBathroom()
        {
            
        }

        public void Enter(MinersWife entityType)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Walkin' to the can. Need to powda mah prett li'lle nose");
        }

        public void Execute(MinersWife entityType)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Ahhhhhh! Sweet relief!");
            entityType.StateMachine.RevertToPreviousState();
        }

        public void Exit(MinersWife entityType)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Leavin' the Jon");
        }

        public bool OnMessage(MinersWife entity, Telegram telegram)
        {
            return false;
        }

        public static VisitBathroom Instance => Lazy.Value;
    }
}
