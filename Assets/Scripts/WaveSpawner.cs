using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public List<Enemy> enemies = new();
    public int currentWave;
    protected int waveValue;
    protected List<GameObject> enemiesToSpawn = new();

    [SerializeField] Vector3 spawnPosition;
    [SerializeField] protected float spawnInterval;
    [SerializeField] protected float spawnTimer;
    [SerializeField] protected Text enemyCountText;
    [SerializeField] protected Text waveText;
    protected int lowestCost;

    private void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        int spawnSide = Random.Range(0, 2) * 2 - 1;
        spawnPosition = playerPosition - 25 * spawnSide * Vector3.right;
    }

    private void FixedUpdate()
    {
        enemyCountText.text = "Enemies: " + GetEnemyCount();
        waveText.text = "Wave: " + currentWave.ToString();

        if (enemiesToSpawn.Count > 0 && spawnTimer <= 0)
        {
            Instantiate(enemiesToSpawn[0], spawnPosition, Quaternion.identity);
            enemiesToSpawn.RemoveAt(0);
            spawnTimer = spawnInterval;
        }
        spawnTimer -= Time.deltaTime;
    }

    public void GenerateWave()
    {
        currentWave++;
        waveValue = currentWave * 3;
        lowestCost = GetLowestCost(enemies);

        GenerateEnemies();
    }

    public List<GameObject> GetEnemiesToSpawn()
    {
        return enemiesToSpawn;
    }

    public int GetEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length + enemiesToSpawn.Count;
    }

    private void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new();
        while (waveValue > lowestCost)
        {
            int randomEnemyId = Random.Range(0, enemies.Count);
            int randomEnemyCost = enemies[randomEnemyId].cost;

            if (waveValue - randomEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randomEnemyId].enemyPrefab);
                waveValue -= randomEnemyCost;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    public int GetLowestCost(List<Enemy> enemies)
    {
        int lowestCost = enemies[0].cost;
        for (int index = 1; index < enemies.Count; index++)
        {
            if (enemies[index].cost < lowestCost)
                lowestCost = enemies[index].cost;
        }
        return lowestCost;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
