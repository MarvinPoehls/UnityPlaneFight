using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private EnemyPointerWindow enemyPointerWindow;
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private List<GameObject> enemies;
    
    private void Start()
    {
        waveSpawner.GenerateWave();
        enemies = waveSpawner.GetEnemiesToSpawn();

        foreach (GameObject enemy in enemies)
        {
            Debug.Log(enemy.transform.position);
            enemyPointerWindow.CreatePointer(enemy.transform);
        }
    }

    private void Update()
    {
        if (waveSpawner.GetEnemyCount() == 0)
        {
            waveSpawner.GenerateWave();
            enemies = waveSpawner.GetEnemiesToSpawn();

            foreach (GameObject enemy in enemies)
            {
                enemyPointerWindow.CreatePointer(enemy.transform);
            }
        }
    }
}
