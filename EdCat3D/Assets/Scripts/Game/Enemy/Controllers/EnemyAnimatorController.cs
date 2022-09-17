using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyAnimatorController : MonoBehaviour
    {
        [SerializeField] private EnemyBehaviour _enemyBehaviour;
        [SerializeField] private Animator _animator;
        
        private const string Idle = "idle";
        private const string Move = "move";
        private const string Run = "run";
        private const string Attack = "attack";


        public void IdleAnimation()
        {
            _animator.SetTrigger(Idle);
        }

        public void AttackAnimation()
        {
            _animator.SetTrigger(Attack);
        }

        public void MoveAnimation()
        {
            _animator.SetTrigger(Move);
        }

        public void RunAnimation()
        {
            _animator.SetTrigger(Run);
        }

        public void DieAnimation()
        {

        }
    }
}


