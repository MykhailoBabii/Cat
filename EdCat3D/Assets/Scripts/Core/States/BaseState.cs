using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.States
{
    public enum ApplicationStates
    {
        Intro,
        Gameplay,
        Lobby,
        Loading
    }

    public enum GameplayStates
    {
        Prepare,
        Lobby,
        Gameplay,
        Finish
    }


    public interface IState<T>
    {
        T State { get; }

        void Prepare();
        void Enter();
        void Exit();
        void InitStateComplitationCallBack(System.Action complitationCallBack);
    }

    public abstract class BaseState<T> : IState<T>
    {
        protected Action _completationCallBack;

        public abstract T State { get; }

        public abstract void Enter();

        public abstract void Exit();

        public void InitStateComplitationCallBack(Action complitationCallBack)
        {
            _completationCallBack = complitationCallBack;
        }

        public virtual void Prepare()
        {

        }
        protected virtual void CompleteState()
        {
            if (_completationCallBack != null)
            {
                _completationCallBack.Invoke();
            }
        }
    }
}