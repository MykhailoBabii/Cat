using Core.Events;
using Core.ResourceInner;
using Core.Utilities;
using Game.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public enum SpawnerState
    {
        None,
        DelayOnStart,
        Start,
        WaitForSpawn,
        Spawn,
        MaximumEnemies
    }

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemySpawnType;
        [SerializeField] private float _delayBeforeFirstSpawn = 0f;
        [SerializeField] private float _delayBeforeNextSpawn = 5f;
        [SerializeField] private int _enemiesOnStart = 1;
        [SerializeField] private float _spawnRadius = 1f;
        [SerializeField] private int _enemiesCountOnSpawn;
        [SerializeField] private int _maximumEnemiesCount = 10;
        [SerializeField] private string enemyPrefab;
        [SerializeField] private int _enemyLevelMin = 1;
        [SerializeField] private int _enemyLevelMax = 2;

        [SerializeField] private float _spawnHeight = 0.5f;
        [SerializeField] private Transform _parentForEnemy;

        [SerializeField] private EnemyDescriptor _enemyDescriptor;
        [SerializeField] private ResourceProvider _resourceProvider;
        [SerializeField] private float _enemySize;

        private List<EnemyBehaviour> _enemyBehaviours = new List<EnemyBehaviour>();

        private ITimer _spawnerTimer;
        private SpawnerState _state;

        private EnemyData _enemyData;

        private void Awake()
        {
            _spawnerTimer = Timer.Empty();
            _enemyData = _enemyDescriptor.GetEnemyData(_enemySpawnType);

            EventAggregator.Subscribe<EnemyKillEvent>(OnEnemyKillEventHandler);
        }

        private void Start()
        {
            OnDelayOnStart();
        }

        private void OnDestroy()
        {
            EventAggregator.Unsubscribe<EnemyKillEvent>(OnEnemyKillEventHandler);
        }

        private void UpdateState(SpawnerState state)
        {
            _state = state;

            switch (state)
            {
                case SpawnerState.None:
                    break;
                case SpawnerState.DelayOnStart:
                    OnDelayOnStart();
                    break;
                case SpawnerState.Start:
                    OnStart();
                    break;
                case SpawnerState.WaitForSpawn:
                    OnWaitForSpawn();
                    break;
                case SpawnerState.Spawn:
                    OnSpawn();
                    break;
                case SpawnerState.MaximumEnemies:
                    OnMaximumEnemies();
                    break;
                default:
                    break;
            }
        }

        private void OnDelayOnStart()
        {
            _spawnerTimer.SetupNewTime(_delayBeforeFirstSpawn, TimerDirection.Forward, null, OnDelayOnStartComplete);
            _spawnerTimer.Start();
        }

        private void OnDelayOnStartComplete()
        {
            UpdateState(SpawnerState.Start);
        }

        private void OnStart()
        {
            PrepareEnemy(_enemiesOnStart);
        }

        private void OnWaitForSpawn()
        {
            _spawnerTimer.SetupNewTime(_delayBeforeNextSpawn, TimerDirection.Forward, null, OnSpawn);
            _spawnerTimer.Start();
        }

        private void OnSpawn()
        {
            PrepareEnemy(_enemiesCountOnSpawn);
        }

        private void OnMaximumEnemies()
        {
            return;
        }

        public void PrepareEnemy(int count)
        {
            for (int i = 0; i < count; i++)
            {

                if (_enemyBehaviours.Count >= _maximumEnemiesCount)
                {
                    _spawnerTimer.Stop();
                    return;
                }
                
                var enemyObjectPrefab = _resourceProvider.GetPrefab(_enemyData.PrefabId);
                var enemyObject = PoolManager.GetObject(enemyObjectPrefab.gameObject);

                if (enemyObject.GetComponent<CapsuleCollider>())
                {
                    _enemySize = enemyObject.GetComponent<CapsuleCollider>().radius;
                }
                else
                {
                    _enemySize = enemyObject.GetComponent<SphereCollider>().radius;
                }
                
                enemyObject.transform.SetParent(_parentForEnemy);
                enemyObject.transform.position = GetFreePosition(_enemySize);
                enemyObject.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(enemyObject.transform.position);

                var enemyBehaviour = enemyObject.GetComponent<EnemyBehaviour>();
                enemyBehaviour.gameObject.SetActive(true);
                enemyBehaviour.SetEnemyData(_enemyData);
                enemyBehaviour.SetEnemyLevel(_enemyLevelMin);

                _enemyBehaviours.Add(enemyBehaviour);
            }

            UpdateState(SpawnerState.WaitForSpawn);
        }

        private Vector3 GetFreePosition(float _enemySize)
        {
            bool isOverlap = true;
            
            var result = UnityEngine.Random.insideUnitSphere * _spawnRadius + _parentForEnemy.transform.position;
            result.y = _spawnHeight;

            if (_enemyBehaviours.Count == 0)
            {
                return result;
            }

            while (isOverlap)
            {
                isOverlap = true;

                foreach (var item in _enemyBehaviours)
                {
                    if (Vector3.Distance(result, item.transform.position) < _enemySize * 2)
                    {
                        isOverlap = true;

                        result = UnityEngine.Random.insideUnitSphere * _spawnRadius + _parentForEnemy.transform.position;
                        result.y = _spawnHeight;

                        break;
                    }

                    isOverlap = false;

                    break;

                }
            }

            return result;
        }

        private void OnEnemyKillEventHandler(object sender, EnemyKillEvent eventData)
        {
            _spawnerTimer.Stop();
            _enemyBehaviours.Remove(eventData.Enemy);
            UpdateState(SpawnerState.WaitForSpawn);
        }
    }
}
