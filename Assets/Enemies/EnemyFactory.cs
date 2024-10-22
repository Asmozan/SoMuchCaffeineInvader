using System.Collections.Generic;
using Invader.General;
using UnityEngine.Pool;
using UnityEngine;

namespace Invader.Enemies
{
    public class EnemyFactory
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private Dictionary<string, ObjectPool<GameObject>> _enemyPools;

        public EnemyFactory()
        {
            _enemyPools = new Dictionary<string, ObjectPool<GameObject>>();
        }

        public GameObject CreateEnemy(Transform parent, EnemyData data, float baseSpeed)
        {
            if (data.Prefab == null)
            {
                Debug.LogError($"Prefab for enemy type {data.Type} is not assigned.");
                return null;
            }

            if (!_enemyPools.ContainsKey(data.Type))
            {
                _enemyPools[data.Type] = CreatePoolForEnemyType(parent, data);
            }

            ObjectPool<GameObject> pool = _enemyPools[data.Type];
            GameObject enemy = pool.Get();

            InitializeEnemy(enemy, data, baseSpeed, pool);

            return enemy;
        }

        private ObjectPool<GameObject> CreatePoolForEnemyType(Transform parent, EnemyData data)
        {
            return new ObjectPool<GameObject>(
                createFunc: () =>
                {
                    GameObject enemy = GameObject.Instantiate(data.Prefab, parent);
                    return enemy;
                },
                actionOnGet: enemy =>
                {
                    enemy.SetActive(true);
                    AssignRandomColor(enemy);
                },
                actionOnRelease: enemy =>
                {
                    enemy.SetActive(false);
                },
                actionOnDestroy: GameObject.Destroy,
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 100
            );
        }

        private void InitializeEnemy(GameObject enemy, EnemyData data, float baseSpeed, ObjectPool<GameObject> pool)
        {
            enemy.transform.position = Vector3.zero;
            enemy.transform.rotation = Quaternion.identity;

            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent == null)
            {
                Debug.LogError("Enemy component is missing on the instantiated enemy prefab.");
                return;
            }

            enemyComponent.Initialize(data.Health, 
                baseSpeed * data.MovementSpeedMultiplier,
                data.ScorePoints,
                data.ExperiencePoints);

            enemyComponent.ReturnToPool = () => {
                ResetEnemy(enemy);
                pool.Release(enemy);
                enemyComponent.ReturnToPool = null;
            };
            
            AssignRandomColor(enemy);
        }

        private void ResetEnemy(GameObject enemy)
        {
            Health healthComponent = enemy.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.ResetHealth();
            }
        }
        
        private void AssignRandomColor(GameObject enemy)
        {
            if (enemy.TryGetComponent(out Renderer renderer))
            {
                MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
                renderer.GetPropertyBlock(propertyBlock);
                propertyBlock.SetColor(BaseColor, Random.ColorHSV());
                renderer.SetPropertyBlock(propertyBlock);
            }
        }
    }
}
