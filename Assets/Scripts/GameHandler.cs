using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private EnemyPointerWindow enemyPointerWindow;
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private List<GameObject> enemies;

    private float timer;
    private float startTimer;
    private void Start()
    {
        waveSpawner.GenerateWave();
    }

    private void Update()
    {
        if (waveSpawner.GetEnemyCount() == 0 && timer <= 0)
        {
            waveSpawner.GenerateWave();
            timer = startTimer;
        }

        timer -= Time.deltaTime;
    }
}
