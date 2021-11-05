using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnRate = 1f;
    [SerializeField] float spawnTimeVariance = 1f;
    [SerializeField] float minimumSpawnTime = 0.5f;
    [SerializeField] bool isLooping = false;
    GameObject instance;

    void Start()
    {
        isLooping = true;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        do
        {
            bool spawnFromLeft = SpawnFromLeft();
            float x = spawnFromLeft ? 0f : 1f;
            float y = GetRandomYPosition();
            Vector3 p = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0));
            instance = Instantiate(enemyPrefab, p, Quaternion.identity);
            instance.GetComponent<HellicopterController>().SetMoveDirection(spawnFromLeft);
            yield return new WaitForSeconds(GetRandomSpawnTime());
        }
        while (isLooping);
    }

    bool SpawnFromLeft()
    {
        return Random.Range(0f, 1f) < 0.5f ? true : false;
    }

    float GetRandomYPosition()
    {
        return Random.Range(0.7f, 0.9f);
    }

    float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(spawnRate - spawnTimeVariance,
                                        spawnRate + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
