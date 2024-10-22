using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Newtonsoft.Json;

namespace Invader.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private TextAsset _enemyDataJson;
        [SerializeField] private List<EnemyData> _enemyDataList;
        [SerializeField] private SpawnArea _spawnArea;
        [SerializeField] private float _spawnRate = 2.0f;
        
        [SerializeReference] private float _baseEnemySpeed;

        private EnemyFactory _enemyFactory;
        private float _nextSpawn;
        
        private void Start()
        {
            InitializeFactory();
            
            if (_spawnArea == null)
            {   
                Debug.LogError("Spawn area is not assigned.");
            }
        }
        
        private void Update()
        {
            HandleSpawning();
        }
        
        private void HandleSpawning()
        {
            if (Time.time < _nextSpawn)
            {
                return;
            }
            
            SpawnEnemy();
            _nextSpawn = Time.time + _spawnRate;
        }
        
        [ContextMenu("Load Enemy Data")]
        private void LoadEnemyData()
        {
            EnemyDataList enemyDataList = JsonConvert.DeserializeObject<EnemyDataList>(_enemyDataJson.text);
            _enemyDataList = enemyDataList.Enemies;
            _baseEnemySpeed = enemyDataList.BaseEnemySpeed;
            
            Debug.Log($"Loaded {_enemyDataList.Count} enemies with base speed {_baseEnemySpeed}.");
            
            
            ValidateSpawnChances();
        }

        [ContextMenu("Save Enemy Data")]
        private void SaveEnemyData()
        {
            EnemyDataList enemyDataList = new EnemyDataList
            {
                Enemies = _enemyDataList
            };

            string json = JsonUtility.ToJson(enemyDataList, true);
            System.IO.File.WriteAllText(Application.dataPath + "/Enemies/EnemyData.json", json);
        }
        
        private void InitializeFactory()
        {
            _enemyFactory = new EnemyFactory();
        }

        private void SpawnEnemy()
        {
            EnemyData selectedEnemyData = SelectEnemyBasedOnChance();
            if (selectedEnemyData == null)
            {
                Debug.LogError("Enemy data is not set.");
                return;
            }
            
            Vector3 spawnPosition = _spawnArea.GetRandomPosition();
            GameObject enemy = _enemyFactory.CreateEnemy(transform,
                selectedEnemyData,
                _baseEnemySpeed
            );

            if (enemy != null)
            {
                enemy.transform.position = spawnPosition;
            }
        }

        private EnemyData  SelectEnemyBasedOnChance()
        {
            float totalChance = 0f;
            foreach (var enemyData in _enemyDataList)
            {
                totalChance += enemyData.SpawnChance;
            }

            float randomValue = Random.Range(0f, totalChance);
            float cumulative = 0f;

            foreach (var enemyData in _enemyDataList)
            {
                cumulative += enemyData.SpawnChance;
                if (randomValue <= cumulative)
                {
                    return enemyData;
                }
            }

            return null;
        }

        private void ValidateSpawnChances()
        {
            float totalChance = 0f;
            foreach (var enemyData in _enemyDataList)
            {
                totalChance += enemyData.SpawnChance;
            }

            var isTotalChanceValid = Mathf.Abs(totalChance - 100f) < Mathf.Epsilon;

            if (!isTotalChanceValid)
            {
                 return;
            }

            Debug.LogWarning($"Total spawn chance is {totalChance}%, adjusting to 100%.");

            foreach (var enemyData in _enemyDataList)
            {
                enemyData.SpawnChance = (enemyData.SpawnChance / totalChance) * 100f;
            }
        }
    }
}
