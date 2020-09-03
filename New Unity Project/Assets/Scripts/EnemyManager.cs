using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Do Not Touch")]
    public List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private int wave = 1;

    [Header("Change This")]
    [SerializeField] private int nbEnemyPerWave = 0;
    [SerializeField] private int increaseNbEnemyPerWave = 0;
    [SerializeField] private float increaseSpeedBy = 0;
    [SerializeField] private int increaseDamageBy = 0;
    [Space]
    [SerializeField] private Vector2 leftSpawn = new Vector2(0f, -4.25f);
    [SerializeField] private Vector2 rightSpawn = new Vector2(0f, -4.25f);
    [SerializeField] GameObject enemyPrefab = null;

    private float timer = -1f;
    private int nbEnemy = 0;
    private bool isDay = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isDay)
        {
            if (timer > 0f)
                timer -= Time.deltaTime;
            if (timer <= 0f && timer > -0.2f)
            {
                nbEnemy--;

                if (nbEnemy > 0)
                    timer = 0.5f;
                else
                    timer = -1f;

                SpawnEnemy();
            }
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].health > 0)
                    continue;

                if (enemies[i].left && enemies[i].gameObject.transform.position.x <= leftSpawn.x)
                {
                    Destroy(enemies[enemies.Count - 1].gameObject, 1f);
                    enemies.Remove(enemies[enemies.Count - 1]);
                }
                else if (!enemies[i].left && enemies[i].gameObject.transform.position.x >= rightSpawn.x)
                {
                    Destroy(enemies[enemies.Count - 1].gameObject, 1f);
                    enemies.Remove(enemies[enemies.Count - 1]);
                }
                    
            }
        }
    }

    public void PassNight()
    {
        isDay = false;
        nbEnemy = nbEnemyPerWave + increaseNbEnemyPerWave * wave;
        timer = 0.5f;
    }

    private void SpawnEnemy()
    {
        bool isLeft = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));

        enemies.Add(Instantiate(enemyPrefab, isLeft ? leftSpawn : rightSpawn, Quaternion.identity).GetComponent<Enemy>());
        float speed = UnityEngine.Random.Range(enemies[enemies.Count - 1].speed, enemies[enemies.Count - 1].speed + increaseSpeedBy * wave);
        int damage = UnityEngine.Random.Range(enemies[enemies.Count - 1].damage, 1 + enemies[enemies.Count - 1].damage + increaseDamageBy * wave);

        enemies[enemies.Count - 1].InitializeEnemy(speed, damage, isLeft);
    }

    public void PassDay()
    {
        isDay = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].health = 0;
        }

        /*
        while (enemies.Count != 0)
        {
            Destroy(enemies[enemies.Count - 1].gameObject, 1f);
            enemies.Remove(enemies[enemies.Count - 1]);
        }
        */
        wave++;
    }
}
