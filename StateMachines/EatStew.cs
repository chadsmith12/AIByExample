using System;
using Common.FSM;
using Common.Messaging;

namespace StateMachines
{
    public class EatStew : IState<Miner>
    {
        public static readonly Lazy<EatStew> Lazy = new Lazy<EatStew>(() => new EatStew());

        private EatStew()
        {

        }
        public void Enter(Miner entityType)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Smells Reaaal good Elsa");
        }

        public void Execute(Miner entityType)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Tastes real good too!");
            entityType.StateMachine.RevertToPreviousState();
        }

        public void Exit(Miner entityType)
        {
            Console.Write($"\n{Constants.GetNameOfEntity((Entity)entityType.Id)}: Thankya hun! Ahh better get back to watever ahh wuz doin!");
        }

        public bool OnMessage(Miner entity, Telegram telegram)
        {
            return false;
        }

        public static EatStew Instance => Lazy.Value;
    }
}
