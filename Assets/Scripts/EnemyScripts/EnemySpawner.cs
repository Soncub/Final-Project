using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int enemyCount;
    public GameObject[] enemyTypes;
    public float spawnInterval;
}

public class EnemySpawner : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    public Transform[] spawnPoints;
    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;

    public UpgradeMenu upgrades;
    public PlayerController player;

    void Start()
    {
        upgrades = GameObject.Find("Canvas").GetComponent<UpgradeMenu>(); 
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (totalEnemies.Length == 0 && !canSpawn && (currentWaveNumber + 1 != waves.Count))
        {
            upgrades.BringMenuUp();
            SpawnNextWave();
            player.Save();
        }
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.enemyTypes[Random.Range(0, currentWave.enemyTypes.Length)];
            Transform randomSpawner = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpawner.position, Quaternion.identity);
            currentWave.enemyCount--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            if (currentWave.enemyCount == 0)
            {
                canSpawn = false;
            }
        }
    }

    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
    }
}