using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Game.EnemyStates
{
    public class DoSomethingEnemyState : BaseEnemyState
    {
        public override EnemyStateType State { get { return EnemyStateType.DoSomeAction; } }

        
        public DoSomethingEnemyState()
        {
            
        }

        public override void Enter()
        {
            Debug.Log("Do Something");
        }

        public override void Update()
        {

        }

        public override void Exit()
        {

        }


    }
}


