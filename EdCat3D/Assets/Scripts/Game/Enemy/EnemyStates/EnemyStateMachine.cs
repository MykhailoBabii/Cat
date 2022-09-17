using System;
using System.Collections;
using System.Collections.Generic;
using Game.EnemyStates;
using UnityEngine;


namespace Game.EnemyStates
{
    public interface IEnemyStateMachine
    {
        IEnemyState Current { get; }
        void SwitchToState(EnemyStateType state);
    }

    public interface IEnemyStateSwitcher
    {
        void SwitchToState(EnemyStateType state);
    }

    public class EnemyStateMachine : IEnemyStateMachine
    {
        private Dictionary<EnemyStateType, IEnemyState> _states = new Dictionary<EnemyStateType, IEnemyState>();

        private IEnemyState _current;

        public IEnemyState Current { get { return _current; } }

        public void Init(params IEnemyState[] states)
        {
            foreach (var state in states)
            {
                _states[state.State] = state;
            }
        }

        public void SwitchToState(EnemyStateType state)
        {
            /*
            if (_current != null)
            {
                _current.Exit();
            }
            */

            _current = _states[state];
            _current.Enter();
            _current.Update();
        }


        public void WaitForUpdate()
        {
            _current?.Update();
        }
    }
}

