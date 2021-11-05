using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnEnemyTime = 1f;
    [SerializeField] float spawnTimeVariance = 1f;
    [SerializeField] float minimumSpawnTime = 0.5f;
    [SerializeField] bool isLooping = false;

    void Start()
    {
        // isLooping = true;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        do
        {
            bool spawnFromLeft = SpawnFromLeft();
            Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(GetRandomSpawnTime());
        }
        while (isLooping);
    }

    bool SpawnFromLeft()
    {
        return Random.Range(0f, 1f) < 0.5f ? true : false;
    }

    Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(0, 0);
    }

    float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(spawnEnemyTime - spawnTimeVariance,
                                        spawnEnemyTime + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
