using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.EnemyStates
{
    public enum EnemyStateType
    {
        Idle,
        Patrol,
        DoSomeAction,
        PersecutionEnemy,
        Attack
    }

    public interface IEnemyState
    {
        EnemyStateType State { get; }

        
        void Enter();
        void Update();
        void Exit();
    }

    public abstract class BaseEnemyState : IEnemyState
    {
        public abstract EnemyStateType State { get; }

        
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}
