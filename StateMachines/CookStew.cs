using System;
using Common.FSM;
using Common.Messaging;

namespace StateMachines
{
    public class CookStew : IState<MinersWife>
    {
        public static Lazy<CookStew> Lazy = new Lazy<CookStew>(() => new CookStew());

        private CookStew()
        {
            
        }

        public void Enter(MinersWife entityType)
        {
            // we are already cooking do nothing
            if (entityType.IsCooking) return;

            Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Puttin' the stew in the oven");

            // delayed message to myself to remind to take stew out
            MessageDispatcher.SendMessage(0.000001, entityType.Id, entityType.Id, (int)MessageType.MsgStewReady, null);

            entityType.IsCooking = true;
        }

        public void Execute(MinersWife entityType)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Fussin' over food");
        }

        public void Exit(MinersWife entityType)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Puttin' the stew on the table");
        }

        public bool OnMessage(MinersWife entity, Telegram telegram)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            switch ((MessageType) telegram.MessageType)
            {
                case MessageType.MsgStewReady:
                    Console.Write($"\nMessage received by {Constants.GetNameOfEntity((Entity) entity.Id)} at time: {DateTime.Now:h:mm:ss tt zz}");
                    Console.Write($"\n{Constants.GetNameOfEntity((Entity) entity.Id)}: StewReady! Lets eat!!");

                    // let the hubby know the stew is ready
                    MessageDispatcher.SendMessage(MessageDispatcher.SendMessageImmediately, entity.Id, (int) Entity.MinerBob, (int) MessageType.MsgStewReady, null);
                    entity.IsCooking = false;
                    entity.StateMachine.CurrentState = DoHouseWork.Instance;
                    return true;
                case MessageType.MsgHiHoneyImHome:
                default:
                    return false;
            }
        }

        public static CookStew Instance => Lazy.Value;
    }
}
