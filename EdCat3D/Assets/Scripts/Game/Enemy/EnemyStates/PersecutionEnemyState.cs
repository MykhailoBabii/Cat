using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Game.EnemyStates
{
    

    public class PersecutionEnemyState : BaseEnemyState
    {
        public Action OnMoveToTargetAction;

        public override EnemyStateType State { get { return EnemyStateType.PersecutionEnemy; } }

        private EnemyBehaviour _enemyBehaviour;
        private EnemyStateMachine _enemyStateMachine;
        private EnemyController _enemyController;

        public PersecutionEnemyState(EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine, EnemyController enemyController)
        {
            _enemyBehaviour = enemyBehaviour;
            _enemyStateMachine = enemyStateMachine;
            _enemyController = enemyController;

            //_enemyBehaviour.onRunAction += Enter;
        }


        public override void Enter()
        {
            Debug.Log("Player is found state");

            _enemyController.Run();
        }

        public override void Update()
        {
            if (_enemyBehaviour.EnemyIsFound == false)
                _enemyStateMachine.SwitchToState(EnemyStateType.Patrol);

            if (_enemyBehaviour.EnemyIsNear == true)
                _enemyStateMachine.SwitchToState(EnemyStateType.Attack);
        }

        public override void Exit()
        {
            

        }


        
    }
}


