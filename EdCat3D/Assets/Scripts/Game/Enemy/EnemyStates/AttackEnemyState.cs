using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Game.EnemyStates
{
    public class AttackEnemyState : BaseEnemyState
    {
        public override EnemyStateType State { get { return EnemyStateType.Attack; } }

        private EnemyBehaviour _enemyBehaviour;
        private EnemyStateMachine _enemyStateMachine;
        private EnemyController _enemyController;

        public AttackEnemyState(EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine, EnemyController enemyController)
        {
            _enemyBehaviour = enemyBehaviour;
            _enemyStateMachine = enemyStateMachine;
            _enemyController = enemyController;

        }

        public override void Enter()
        {
            _enemyController.Attack();

            Debug.Log("Attack state");
        }

        public override void Update()
        {
            if (_enemyBehaviour.EnemyIsNear == false)
                _enemyStateMachine.SwitchToState(EnemyStateType.PersecutionEnemy);
        }

        public override void Exit()
        {
            
        }


    }
}


