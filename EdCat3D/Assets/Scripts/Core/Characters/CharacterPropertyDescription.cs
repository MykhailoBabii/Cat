using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = " CharacterPropertyDescription", menuName = "Create/ CharacterPropertyDescription")]

    public class CharacterPropertyDescription : ScriptableObject
    {
        [SerializeField] private int _startHP;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private int _attackPower;

        public int StartHp => _startHP;
        public float MovementSpeed => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float AttackSpeed => _attackSpeed;
        public int AttackPower => _attackPower;
    }

    
}

