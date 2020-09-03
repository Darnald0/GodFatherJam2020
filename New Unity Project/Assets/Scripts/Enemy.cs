using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Do Not Touch")]
    public Tree tree;
    public Barricade barricade;
    public bool left;

    [Header("Change This")]
    public int health = 3;
    public float speed;
    public int damage;
    [SerializeField] private float timeToAttack;
     
    private float timer;
    private float dep = 0;
    private Rigidbody2D rig;
    private bool isDead = false;
    private Vector2 toGoInDeath = Vector2.zero;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public void InitializeEnemy(float _speed, int _damage, bool _left)
    {
        speed = _speed;
        damage = _damage;
        left = _left;
        dep = left ? 1f : -1f;
    }

    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            dep = left ? -1f : 1f;
            if (barricade != null)
                barricade.nbEnemy--;
            if (tree != null)
                tree.nbEnemy--;
            tree = null;
            barricade = null;
            GetComponent<CircleCollider2D>().enabled = false; // change to capsule collider 2d
        } 
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
