using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Game.EnemyStates
{
    public class PatrolEnemyState : BaseEnemyState
    {
        public override EnemyStateType State { get { return EnemyStateType.Patrol; } }

        private EnemyBehaviour _enemyBehaviour;
        private EnemyStateMachine _enemyStateMachine;
        private EnemyController _enemyController;

        public PatrolEnemyState(EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine, EnemyController enemyController)
        {
            _enemyBehaviour = enemyBehaviour;
            _enemyStateMachine = enemyStateMachine;
            _enemyController = enemyController;

            //_enemyBehaviour.onPatrolAction += Enter;
        }

        public override void Enter()
        {
            Debug.Log("Patrol state");

            _enemyController.Patrol();
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


