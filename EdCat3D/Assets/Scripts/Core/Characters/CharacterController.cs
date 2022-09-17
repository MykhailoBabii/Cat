using Core.Events;
using Core.Property;
using Game.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class CharacterController
    {
        private IntProperty _health;
        public IIntReadOnlyProperty Health => _health;
        public int MaxHealth => _characterPropertyDescription.StartHp;

        private CharacterPropertyDescription _characterPropertyDescription;

        public CharacterController(CharacterPropertyDescription characterPropertyDescription)
        {
            _health = new IntProperty(0);
            _characterPropertyDescription = characterPropertyDescription;
        }

        public void DealDamage(int damage)
        {
            var newHealthValue = _health.Value - damage;
            if (newHealthValue <= 0)
            {
                newHealthValue = 0;
                _health.SetValue(newHealthValue, true); 
                EventAggregator.Post(this, new OnPlayerDeathEvent());
            }
            else
            {
                _health.SetValue(newHealthValue, true);
            }          
        }
        /*
        public void IntractionWithCollider(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<EnemyBehaviour>(out var enemy))
            {
                var enemyData = enemy.Enemy.FirstLevelData;
                var damage = enemyData.AttackDamage;
                DealDamage(damage);
            }
        }
        */
        public void InteractionWithTrigger(Collider other)
        {
            // сбор ресурсов
            // активация дверей и тд.
        }
    }

    
}


