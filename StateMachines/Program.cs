using System;
using System.Threading;
using Common.Entities;

namespace StateMachines
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var minerBob = new Miner((int)Entity.MinerBob);
            var elsa = new MinersWife((int)Entity.Elsa);

            // register them with the manager
            EntityManager.Instance.RegisterEntity(minerBob);
            EntityManager.Instance.RegisterEntity(elsa);

            // run through a few updates for miner bob
            for(var i = 0; i < 40; ++i)
            {
                minerBob.Update();
                elsa.Update();
                Thread.Sleep(800);
            }

            Console.Read();
        }
    }
}