using System;
using System.Collections.Generic;
using System.Text;
using Common.FSM;
using Common.Messaging;

namespace StateMachines
{
    public class WifesGlobalState : IState<MinersWife>
    {
        public static Lazy<WifesGlobalState> Lazy = new Lazy<WifesGlobalState>(() => new WifesGlobalState());

        private WifesGlobalState()
        {
            
        }

        public void Enter(MinersWife entityType)
        {
            // Nothing
        }

        public void Execute(MinersWife entityType)
        {
            var random = new Random();
            if (random.NextDouble() < 0.1 && !entityType.StateMachine.IsInState(VisitBathroom.Instance))
            {
                entityType.StateMachine.ChangeState(VisitBathroom.Instance);
            }
        }

        public void Exit(MinersWife entityType)
        {
            // Nothing
        }

        public bool OnMessage(MinersWife entity, Telegram telegram)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Green;

            switch ((MessageType) telegram.MessageType)
            {
                case MessageType.MsgHiHoneyImHome:
                    Console.Write($"\nMessage handled by {Constants.GetNameOfEntity((Entity) entity.Id)} at time: {DateTime.Now:t}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"\n{Constants.GetNameOfEntity((Entity) entity.Id)}: Hi honey. Let me make you some mah fine country stew");
                    entity.StateMachine.ChangeState(CookStew.Instance);
                    return true;
            }

            return false;
        }

        public static WifesGlobalState Instance => Lazy.Value;
    }
}
