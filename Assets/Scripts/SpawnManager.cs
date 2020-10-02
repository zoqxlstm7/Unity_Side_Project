﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Variables
    [SerializeField] int initialSpawnCount = 5;
    [SerializeField] float betweenSpawnTime = 1.0f;

    int spawnCount = 0;
    int remainSpawnCount = 0;

    int waveCount = 1;

    float startSpawnTime = 0.0f;
    #endregion Variables

    #region Unity Methods
    private void Start()
    {
        spawnCount = initialSpawnCount;
        remainSpawnCount = initialSpawnCount;
    }

    private void Update()
    {
        if(spawnCount > 0 && Time.time - startSpawnTime > betweenSpawnTime)
        {
            SpawnRandomTile();

            spawnCount--;
            startSpawnTime = Time.time;
        }
    }
    #endregion Unity Methods

    #region Helper Methods
    void SpawnRandomTile()
    {
        List<Transform> tiles = InGameSceneManager.instance.MapGenerator.tileList;

        int randomIndex = Random.Range(0, tiles.Count);
        StartCoroutine(FlashTileWithPingPong(tiles[randomIndex].transform));
    }

    IEnumerator FlashTileWithPingPong(Transform tileTransform)
    {
        float flashDelayTime = 3.0f;
        float tileFlashTime = 2.0f;

        Material material = tileTransform.GetComponent<Renderer>().material;
        Color originColor = material.color;
        Color flashColor = Color.red;

        float currentDelayTime = 0.0f;

        while (currentDelayTime < flashDelayTime)
        {
            material.color = Color.Lerp(originColor, flashColor, Mathf.PingPong(Time.time * tileFlashTime, 1));

            currentDelayTime += Time.deltaTime;

            yield return null;
        }

        material.color = originColor;

        SpawnEnemy(tileTransform.position);
    }

    public void SpawnEnemy(Vector3 appearPos)
    {
        CacheData[] enemyCache = InGameSceneManager.instance.EnemyManager.CacheDatas;
        int randomIndex = Random.Range(0, enemyCache.Length);

        Enemy enemy = InGameSceneManager.instance.EnemyManager.Generate(enemyCache[randomIndex].filePath, appearPos);
        enemy.SetHealth(waveCount);
    }

    public void CheckRemainEnemy()
    {
        remainSpawnCount--;

        if(remainSpawnCount <= 0)
        {
            waveCount++;
            StartCoroutine(SpawnWithDelay());
        }
    }

    IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(2.0f);
        spawnCount = initialSpawnCount + waveCount;
        remainSpawnCount = spawnCount;

        betweenSpawnTime = 1 / (1 + waveCount * 0.01f);
    }
    #endregion Helper Methods
}
