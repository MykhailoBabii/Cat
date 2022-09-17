using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Game.EnemyStates;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private EnemyStateMachine _enemyStateMachine;
        private EnemyData _enemyData;
        private int _enemyLevel;

        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private CharacterControllerView _playerTarget;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyController _enemyController;
        [SerializeField] private EnemyAnimatorController _enemyAnimatorController;

        public EnemyType enemyType => _enemyType;
        public CharacterControllerView PlayerTarget => _playerTarget;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public EnemyController EnemyController => _enemyController;
        public EnemyAnimatorController EnemyAnimatorController => _enemyAnimatorController;

        public bool EnemyIsFound { get; private set; } = false;
        public bool EnemyIsNear { get; private set; } = false;


        private void Awake()
        {
            InitializeStartSettings();
        }

        private void Update()
        {
            _enemyStateMachine.WaitForUpdate();
        }

        public void InitializeStartSettings()
        {
            _enemyStateMachine = new EnemyStateMachine();

            var persecutionEnemyState = new PersecutionEnemyState(this, _enemyStateMachine, _enemyController);
            var idelEnemyState = new IdleEnemyState(this, _enemyStateMachine, _enemyController);
            var attackEnemyState = new AttackEnemyState(this, _enemyStateMachine, _enemyController);
            var patrolEnemyState = new PatrolEnemyState(this, _enemyStateMachine, _enemyController);
            var doSomethingEnemyState = new DoSomethingEnemyState();

            _enemyStateMachine.Init(persecutionEnemyState, idelEnemyState, attackEnemyState, patrolEnemyState, doSomethingEnemyState);

            _enemyStateMachine.SwitchToState(EnemyStateType.Patrol);
        }

        public void SetEnemyData(EnemyData enemyData)
        {
            _enemyData = enemyData;
        }

        public void SetEnemyLevel(int level)
        {
            _enemyLevel = level;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterControllerView playerTarget))
            {
                _playerTarget = playerTarget;
                EnemyIsFound = true;
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CharacterControllerView playerTarget))
            {
                EnemyIsNear = true;
            }
        }
        
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CharacterControllerView playerTarget))
            {
                EnemyIsNear = false;
            }
        }
        /*
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out CharacterControllerView playerTarget))
            {
                if (Vector3.Distance(transform.position, _playerTarget.transform.position) < 8)//test value //_enemyData.GetDataByLevel(_enemyLevel).AttackRadius)
                {
                    EnemyIsNear = true;
                }

                else EnemyIsNear = false;
            }
        }
        */

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CharacterControllerView playerTarget))
            {
                Debug.Log("Enemy is Lost");
                EnemyIsFound = false;
            }
        }
    }
}