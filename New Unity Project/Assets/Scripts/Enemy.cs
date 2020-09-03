using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float timeToAttack;
    [Space]
    public Tree tree;
    public Barricade barricade;
    private float timer;
    private float dep = 0;
    public bool left;
    private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        dep = left ? 1f : -1f;
    }


    private void FixedUpdate()
    {
        if (tree == null && barricade == null)
        {
            rig.velocity = new Vector2(dep * 10f * speed * Time.deltaTime, rig.velocity.y);
        }
        else if (rig.velocity.x > 0.1f || rig.velocity.x < -0.1f)
            rig.velocity = new Vector2(0f, rig.velocity.y);

        if (tree != null)
            tree = tree.IsAlive();

        if (barricade != null)
        {
            timer += Time.fixedDeltaTime;
            AttackBarricade();
            barricade = barricade.IsAlive();
        }
    }

    private void AttackBarricade()
    {
        if (timer >= timeToAttack)
        {
            barricade.AttackBarricade(damage);
            timer = 0f;
        }
    }
}
