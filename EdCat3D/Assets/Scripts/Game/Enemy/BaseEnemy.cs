using System;
using System.Collections;
using System.Collections.Generic;
using Game.EnemyStates;
using UnityEngine;
using UnityEngine.AI;


namespace Game.Enemy
{
    public enum AttackType
    {
        СloseСombat,
        RangedСombat
    }

    public interface IMonster
    {
        int Level { get; }
        int HealthPoints { get; }
        int Armor { get; }
        int AttackDamageMin { get; }
        int AttackDamageMax { get; }
        int CriticalDamagePower { get; }
        int XP { get; }
        int Size { get; }

        float ReactionRadius { get; }
        float AttackRadius { get; }
        float MovementSpeed { get; }
        float AttackSpeed { get; }
        float RespawnProbability { get; }
        float CriticalDamageProbability { get; }
    }

    public abstract class BaseEnemy : EnemyBehaviour, IMonster
    {
        [SerializeField] private EnemyDataByLevel _enemyDataByLevel;
        
        public EnemyDataByLevel EnemyDataByLevel => _enemyDataByLevel;

        public int Level => _enemyDataByLevel.Level;
        public int HealthPoints => _enemyDataByLevel.HealthPoints;
        public int Armor => _enemyDataByLevel.Armor;
        public int AttackDamageMin => _enemyDataByLevel.AttackDamageMin;
        public int AttackDamageMax => _enemyDataByLevel.AttackDamageMax;
        public int CriticalDamagePower => _enemyDataByLevel.CriticalDamagePower;
        public int XP => _enemyDataByLevel.XP;
        public int Size => _enemyDataByLevel.Size;

        public float ReactionRadius => _enemyDataByLevel.ReactionRadius;
        public float AttackRadius => _enemyDataByLevel.AttackRadius;
        public float MovementSpeed => _enemyDataByLevel.MovementSpeed;
        public float AttackSpeed => _enemyDataByLevel.AttackSpeed;
        public float RespawnProbability => _enemyDataByLevel.RespawnProbability;
        public float CriticalDamageProbability => _enemyDataByLevel.CriticalDamageProbability;
    }
}

