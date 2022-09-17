using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    public class EnemyController: MonoBehaviour
    {
        private Vector3 randomPosition;

        [SerializeField] private EnemyBehaviour _enemyBehaviour;
        private Vector3 _startPosition;
        private EnemyAnimatorController _enemyAnimator => _enemyBehaviour.EnemyAnimatorController;


        private void Awake()
        {
            _startPosition = transform.position;
            _enemyAnimator.MoveAnimation();

        }

        private void FixedUpdate()
        {
            

            //else _enemyAnimator.MoveAnimation();
        }

        public void Idle()
        {
            _enemyAnimator.IdleAnimation();
        }

        public void Attack()
        {
            StopAllCoroutines();
            StartCoroutine(Attacks());
        }

        public void Damage()
        {
            _enemyAnimator.AttackAnimation();
        }

        public void Run()
        {
            StopAllCoroutines();
            StartCoroutine(RunToTarget());

        }

        public void Patrol()
        {
            StopAllCoroutines();
            StartCoroutine(PatrolPositions());
            StartCoroutine(CheckArrivToPoint());
        }

        IEnumerator Attacks()
        {
            while (true)
            {
                Debug.Log("Damage!!");
                _enemyAnimator.RunAnimation();
                Damage();
                yield return new WaitForSeconds(1);
            }
        }

        IEnumerator RunToTarget()
        {
            while(true)
            {
                _enemyBehaviour.NavMeshAgent.SetDestination(_enemyBehaviour.PlayerTarget.transform.position);
                _enemyAnimator.RunAnimation();

                yield return new WaitForSeconds(0);
            }
        }

        IEnumerator PatrolPositions()
        {
            while (true)
            {
                float patrolRange = 20;
                _enemyAnimator.MoveAnimation();
                randomPosition = new Vector3(UnityEngine.Random.Range(_startPosition.x - patrolRange, _startPosition.x + patrolRange), transform.position.y, UnityEngine.Random.Range(_startPosition.z - patrolRange, _startPosition.z + patrolRange)); //test value
                _enemyBehaviour.NavMeshAgent.SetDestination(randomPosition);
                

                yield return new WaitForSeconds(10);
            }
        }

        IEnumerator CheckArrivToPoint()
        {
            while(true)
            {
                if (_enemyBehaviour.NavMeshAgent.remainingDistance < _enemyBehaviour.NavMeshAgent.stoppingDistance + 0.001f)//test value
                {
                    _enemyAnimator.IdleAnimation();
                }

                else _enemyAnimator.MoveAnimation();

                yield return new WaitForSeconds(0);
            }
            
        }
    }
}