using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Do Not Touch")]
    public Tree tree;
    public Barricade barricade;
    public bool left;
    public int health = 1;

    [Header("Change This")]
    public float speed;
    public int damage;
    [SerializeField] private float timeToAttack;
     
    private float timer;
    private float dep = 0;
    private Rigidbody2D rig;
    private bool isDead = false;
    private Vector2 toGoInDeath = Vector2.zero;
    private bool playerIsIn = false;
    private Player player = null;
    private bool rightFace = true;
    private Animator animator = null;

    PlayMultipleSound soundScript;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        soundScript = GetComponent<PlayMultipleSound>();
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
        if (dep < 0 && !rightFace)
            Flip();
        else if (dep > 0 && rightFace)
            Flip();

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
            GetComponent<CapsuleCollider2D>().enabled = false;
        } 

        if (Input.GetKeyDown(KeyCode.A) && health > 0 && !isDead && playerIsIn && player.timeToAttack <= 0f)
        {
            soundScript.PlaySound(TYPE_AUDIO.Cris);
            health = 0;
            playerIsIn = false;
            player.ResetAttack();

            if (!animator.GetBool("Scared"))
            {
                animator.SetBool("Running", false);
                animator.SetBool("Cutting", false);
                animator.SetBool("Scared", true);
                soundScript.PlaySound(TYPE_AUDIO.Peur);
            }
        }
    }


    private void FixedUpdate()
    {
        if (tree == null && barricade == null)
        {
            if (health > 0)
            {
                if (!animator.GetBool("Running"))
                {
                    animator.SetBool("Cutting", false);
                    animator.SetBool("Running", true);
                }
            }
            else
            {
                if (!animator.GetBool("Walking"))
                {
                    animator.SetBool("Scared", false);
                    animator.SetBool("Cutting", false);
                    animator.SetBool("Walking", true);
                }
            }
            rig.velocity = new Vector2(dep * 10f * speed * Time.deltaTime, rig.velocity.y);
        }
        else if (rig.velocity.x > 0.1f || rig.velocity.x < -0.1f)
            rig.velocity = new Vector2(0f, rig.velocity.y);

        if (tree != null)
        {
            tree = tree.IsAlive();
            IsCutting();
        }

        if (barricade != null)
        {
            timer += Time.fixedDeltaTime;
            AttackBarricade();
            barricade = barricade.IsAlive();
            IsCutting();
        }
    }

    public void IsCutting()
    {
        if (!animator.GetBool("Cutting"))
        {
            animator.SetBool("Running", false);
            animator.SetBool("Cutting", true);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (health <= 0)
            return;

        if (collision.CompareTag("Player") && !playerIsIn)
        {
            if (player == null)
                player = collision.gameObject.GetComponent<Player>();
            if (!player.isFocusingEnemy)
            {
                playerIsIn = true;
                player.isFocusingEnemy = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerIsIn)
        {
            playerIsIn = false;
            player.isFocusingEnemy = false;
        }
    }

    private void Flip()
    {
        rightFace = !rightFace;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
