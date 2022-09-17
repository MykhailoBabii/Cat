using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    [CreateAssetMenu(fileName = "EnemyDescriptor", menuName = "Create/Enemies Descriptor")]
    public class EnemyDescriptor : ScriptableObject
    { 
        [SerializeField] private List<EnemyData> _enemyDatas = new List<EnemyData>();

        private readonly Dictionary<EnemyType, EnemyData> _enemyDataDictionary = new Dictionary<EnemyType, EnemyData>();

        public EnemyData GetEnemyData(EnemyType enemies)
        {
            return _enemyDataDictionary[enemies];
        }

        private void OnEnable()
        {
            EnemyDataDictionary();
        }

        private void EnemyDataDictionary()
        {
            foreach (var enemyData in _enemyDatas)
            {
                _enemyDataDictionary[enemyData.Type] = enemyData;
            }

        }
    }

    public enum CombatType
    {
        Melee,
        Range
    }

    [System.Serializable]
    public class EnemyData
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private CombatType _combatType;
        [SerializeField] private string _prefabId;
        [SerializeField] private string _uiSpriteId;

        [SerializeField] private List<EnemyDataByLevel> _enemyDataByLevels = new List<EnemyDataByLevel>();

        public EnemyType Type => _enemyType;
        public CombatType Combat => _combatType;

        public string PrefabId => _prefabId;
        public string UISpriteId => _uiSpriteId;

        public EnemyDataByLevel GetDataByLevel(int level)
        {
            var result = _enemyDataByLevels.Find(data => data.Level == level);
            return result;
        }

        public EnemyDataByLevel FirstLevelData => GetDataByLevel(1);
    }


    [Serializable]
    public class EnemyDataByLevel
    {
        [SerializeField] private int _level;
        [SerializeField] private float _reactionRadius;
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _healthPoints;
        [SerializeField] private int _armor;
        [SerializeField] private int _attackDamageMin;
        [SerializeField] private int _attackDamageMax;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _respawnProbability;
        [SerializeField] private float _criticalDamageProbability;
        [SerializeField] private int _criticalDamagePower;
        [SerializeField] private int _experiencePoints;
        [SerializeField] private int _enemySize;
        [SerializeField] private List<LootData> _lootDatas = new List<LootData>();

        public int Level => _level;
        public float ReactionRadius => _reactionRadius;
        public float AttackRadius => _attackRadius;
        public float MovementSpeed => _movementSpeed;
        public int HealthPoints => _healthPoints;
        public int Armor => _armor;
        public int AttackDamageMin => _attackDamageMin;
        public int AttackDamageMax => _attackDamageMax;
        public float AttackSpeed => _attackSpeed;
        public float RespawnProbability => _respawnProbability;
        public float CriticalDamageProbability => _criticalDamageProbability;
        public int CriticalDamagePower => _criticalDamagePower;
        public int XP => _experiencePoints;
        public int Size => _enemySize;

        public int AttackDamage => UnityEngine.Random.Range(_attackDamageMin, _attackDamageMax+1);

        public IReadOnlyList<LootData> Loots => _lootDatas;
    }

    [Serializable]
    public class LootData
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private int _count;

        public ItemType Type => _itemType;
        public int Count => _count;
    }
}
