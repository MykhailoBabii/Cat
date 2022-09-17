using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Idle()
    { 
        _animator.SetTrigger("idle");
    }

    public void Run()
    {
       _animator.SetBool("run", true);
    }

    public void Attack()
    {
        _animator.SetTrigger("attack");
    }

    public void Take()
    {
        _animator.SetTrigger("take");
    }

    public void Death()
    {
        _animator.SetTrigger("death");
    }
    public enum Player
    {
       idle,
       take,
       run,
       attack,
       death
    }


}


   


