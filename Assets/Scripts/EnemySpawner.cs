using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public bool generateSpawnZone = false;
    [Tooltip("Enemy spawn rate in spawns/second")]
    public AnimationCurve spawnRateCurve;

    [Header("Spawner References")]
    public GameObject enemyPrefab;
    public BoxCollider spawnZone;

    private ObjectPool enemyPool;
    private float spawnTimer;
    private float spawnCurTime;
    private float spawnPerSec;

    private void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Spawner has no enemy prefab! Disabling.");
            gameObject.SetActive(false);
        }
        if (generateSpawnZone)
            GenerateSpawnZone();
        if (spawnZone == null)
        {
            Debug.LogWarning("Spawner has no spawn zone, will generate spawn zone");
            GenerateSpawnZone();
        }

        InvokeRepeating("SpawnRateUpdate", 0f, 2f);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        spawnCurTime += Time.deltaTime;
        if (spawnTimer >= spawnPerSec)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    /// <summary>
    /// Spawns an enemy from the enemyPool at a random location within spawn zone.
    /// </summary>
    private void SpawnEnemy()
    {
        if (enemyPool == null)
        {
            enemyPool = ObjectPoolManager.Instance.AddPool(enemyPrefab.tag, enemyPrefab, 200);
        }

        Vector3 spawnPos = GetRandomInZone();
        enemyPool.Instantiate(spawnPos, Quaternion.identity);
    }

    private Vector3 GetRandomInZone()
    {
        Vector3 rand01 = new Vector3(Random.value, Random.value, Random.value) - Vector3.one * 0.5f;
        Vector3 origin = Vector3.zero;

        origin.x = rand01.x * spawnZone.size.x;
        origin.y = rand01.y * spawnZone.size.y;
        origin.z = rand01.z * spawnZone.size.z;

        origin += spawnZone.transform.position + spawnZone.center;

        return origin;
    }

    private void GenerateSpawnZone()
    {
        spawnZone = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;

        spawnZone.center = new Vector3(0f, 0f, GameManager.ScreenBoundsMax.z * 2f);
        spawnZone.size = new Vector3(GameManager.ScreenBoundsMax.x * 2f, 1f, 5f);
    }

    private void SpawnRateUpdate()
    {
        spawnPerSec = 1f / spawnRateCurve.Evaluate(spawnCurTime);
    }
}
