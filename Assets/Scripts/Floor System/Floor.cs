using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ.Floors
{
    public class Floor : MonoBehaviour
    {
        [SerializeField] bool usePooling = false;
        [SerializeField] List<EnemySettings> possibleEnemies;

        public UnityEvent onFinishFloor;

        Dictionary<EnemySettings, Stack<Entity>> pool = new Dictionary<EnemySettings, Stack<Entity>>();

        int activeEntities = 0;

        void Awake()
        {
            if (usePooling)
            {
                for (int i = 0, j; i < possibleEnemies.Count; i++)
                {
                    pool.Add(possibleEnemies[i], new Stack<Entity>());
                    for (j = 0; j < possibleEnemies[i].maxToSpawn; j++)
                    {
                        pool[possibleEnemies[i]].Push(Instantiate(possibleEnemies[i].prefab).GetComponent<Entity>());
                    }
                }
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            SpawnEnemies();
        }


        public void SpawnEnemies()
        {
            for (int i = 0; i < possibleEnemies.Count; i++)
            {
                StartCoroutine(SpawnEnemy(possibleEnemies[i]));
            }
        }

        public void OnEntityDeath(Entity entity)
        {
            activeEntities--;

            if (activeEntities <= 0)
            {
                FinishFloor();
            }
        }

        void FinishFloor()
        {
            onFinishFloor?.Invoke();
        }

        IEnumerator SpawnEnemy(EnemySettings enemy)
        {
            float toSpawn = Random.Range(enemy.minToSpawn, enemy.maxToSpawn);
            int spawned = 0;

            while (spawned < toSpawn)
            {
                if (usePooling)
                {
                    pool[enemy].Peek().OnDeath.AddListener(OnEntityDeath);
                    pool[enemy].Pop().gameObject.SetActive(true);
                }
                else
                {
                    Instantiate(enemy.prefab).GetComponent<Entity>().OnDeath.AddListener(OnEntityDeath);
                }

                spawned++;
                activeEntities++;
                yield return new WaitForSeconds(enemy.useRandomSpawnRate ? Random.Range(enemy.spawnRate, enemy.maxSpawnRate) : enemy.spawnRate);
            }
        }
    }

    [System.Serializable]
    public class EnemySettings
    {
        [Header("Prefab Settings")]
        public string name;
        public GameObject prefab;

        private Entity entity;
        public Entity Entity
        {
            get
            {
                if (!entity)
                    entity = prefab.GetComponent<Entity>();
                return entity;
            }
        }


        [Header("Spawn Settings")]
        public int minToSpawn;
        public int maxToSpawn;

        public bool useRandomSpawnRate = false;

        [Tooltip("When 'Use Random Spawn Rate' is checked spawnRate will be used as minimum")]
        [Range(0f, 10f)] public float spawnRate;
        [Range(0f, 10f)] public float maxSpawnRate;
    }
}