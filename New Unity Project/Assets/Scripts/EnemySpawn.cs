using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private int numberOfEnemyPerWave;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;

    [SerializeField] private GameObject enemy;
    
    public void SpawnWave()
    {
        Instantiate(enemy, leftSpawn);
    }
}
