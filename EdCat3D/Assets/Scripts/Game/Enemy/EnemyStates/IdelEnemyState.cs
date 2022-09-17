using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Game.EnemyStates
{
    public class IdleEnemyState : BaseEnemyState
    {
        public override EnemyStateType State { get { return EnemyStateType.Idle; } }

        private EnemyBehaviour _enemyBehaviour;
        private EnemyStateMachine _enemyStateMachine;
        private EnemyController _enemyController;

        public IdleEnemyState(EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine, EnemyController enemyController)
        {
            _enemyBehaviour = enemyBehaviour;
            _enemyStateMachine = enemyStateMachine;
            _enemyController = enemyController;

            //_enemyBehaviour.onIdleAction += Enter;
        }

        public override void Enter()
        {
            _enemyController.Idle();
            Debug.Log("is idel");
        }

        public override void Update()
        {
            if (_enemyBehaviour.EnemyIsFound == true)
                _enemyStateMachine.SwitchToState(EnemyStateType.PersecutionEnemy);
        }

        public override void Exit()
        {
            
        }


    }
}


