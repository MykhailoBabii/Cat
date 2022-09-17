using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.States
{
    public interface IStateMachine<T>
    {
        IState<T> Current { get; }
        void SwitchToState(T state);
    }

    public class BaseStateMachine<T> : IStateMachine<T>
    {
        private Dictionary<T, IState<T>> _states =
            new Dictionary<T, IState<T>>();

        private IState<T> _current;

        public IState<T> Current { get { return _current; } }

        public BaseStateMachine(params IState<T>[] states)
        {
            foreach (var state in states)
            {
                _states[state.State] = state;
            }
        }

        public void SwitchToState(T state)
        {
            var nextState = _states[state];
            if (_current != null)
            {
                _current.Exit();
            }

            _current = nextState;
            _current.Prepare();
            _current.Enter();
        }

    }
}