using Core.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private CharacterType _character;
        [SerializeField] private Animator _animator;

        public CharacterType Character => _character;
        public void PlayAnimation(string animationName)
        {
            _animator.Play(animationName);
        }
    }
}