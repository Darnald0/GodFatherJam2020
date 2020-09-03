using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyingTreeSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float atkPerSecond;
    private float timer;
    private bool left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= atkPerSecond)
        {
            EnemyAttack();
            timer = 0;
        }
    }

    private void EnemyAttack(int barricadeHP)
    {
        barricadeHP = barricadeHP - damage;
    }

    private void DestroyTree()
    {

    }
}
