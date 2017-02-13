using System;
using System.Collections.Generic;
using System.Text;
using Common.Entities;
using Common.FSM;
using Common.Messaging;

namespace StateMachines
{
    public class MinersWife : BaseGameEntity
    {
        #region Private Fields

        private readonly StateMachine<MinersWife> _stateMachine;

        #endregion
        public MinersWife(int id) : base(id)
        {
            Location = LocationType.Shack;
            IsCooking = false;

            _stateMachine = new StateMachine<MinersWife>(this)
            {
                CurrentState = DoHouseWork.Instance,
                GlobalState = WifesGlobalState.Instance
            };
        }

        public override void Update()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            _stateMachine.Update();
        }

        public override bool HandleMessage(Telegram message)
        {
            return _stateMachine.HandleMessage(message);
        }

        public StateMachine<MinersWife> StateMachine => _stateMachine;

        public LocationType Location { get; set; }

        public bool IsCooking { get; set; }
    }
}
