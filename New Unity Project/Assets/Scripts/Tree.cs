using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Do Not Touch")]
    public int nbEnemy = 0;
    public int health = 2;

    [Header("Change This")]
    [SerializeField] private float playerTimeToDamage = 2f;
    [SerializeField] private float enemyTimeToDamage = 10f;
    [SerializeField] private int woodsGiven = 2;
    [SerializeField] private GameObject spriteBarTree = null;
    [SerializeField] private GameObject spriteBarWhite = null;
    [SerializeField] private GameObject InputInfo = null;
    [Space]
    public SpriteRenderer bigTree = null;
    public SpriteRenderer littleTree = null;

    private float realTimeToDamage = 2f;
    private float realEnemyTimeToDamage = 2f;
    private bool isIn = false;
    private Player player = null;
    private float spriteBarTreeFloat = 0f;
    private float timeToGoUp = 0f;
    private float distToGoUp = 0f;
    private ParticleSystem particles = null;
    private BoxCollider2D boxCollider = null;

    PlayMultipleSound soundScript;
    private bool isWoodGettingCut = false;
    public AudioSource aS;

    private void Start()
    {
        realTimeToDamage = playerTimeToDamage;
        realEnemyTimeToDamage = enemyTimeToDamage;
        spriteBarTreeFloat = spriteBarTree.transform.localScale.x;
        particles = GetComponentInChildren<ParticleSystem>();
        soundScript = GetComponent<PlayMultipleSound>();
        boxCollider = GetComponent<BoxCollider2D>();

        InputInfo.SetActive(false);
    }

    private void Update()
    {
        if (isIn && nbEnemy == 0)
        {
            if (Input.GetKeyDown(KeyCode.E) && realTimeToDamage > 0f && health > 1)
            {
                    soundScript.PlaySound(TYPE_AUDIO.CuttingTree);

            }
            if (Input.GetKeyUp(KeyCode.E) && realTimeToDamage > 0f && health > 1)
            {
                aS.Stop();
            }

            if (Input.GetKey(KeyCode.E) && realTimeToDamage > 0f && health > 1)
            {
                if (!particles.isPlaying)
                {
                    particles.Play();
                }

                if (!player.isCuttingWood)
                    player.isCuttingWood = true;

                player.CuttingTree();

                realTimeToDamage -= Time.deltaTime;

                spriteBarTree.transform.localScale = new Vector3(spriteBarTree.transform.localScale.x - (spriteBarTreeFloat / playerTimeToDamage) * Time.deltaTime, spriteBarTree.transform.localScale.y, spriteBarTree.transform.localScale.z);
                spriteBarTree.transform.localPosition = new Vector3(spriteBarTree.transform.localPosition.x - ((spriteBarTreeFloat / playerTimeToDamage) * Time.deltaTime) / 2, spriteBarTree.transform.localPosition.y, spriteBarTree.transform.localPosition.z);
            }
            else if (realTimeToDamage <= 0f)
            {
                aS.Stop();
                realTimeToDamage = playerTimeToDamage;
                health--;
                player.wood += woodsGiven;
                soundScript.PlaySound(TYPE_AUDIO.GetWood);
                spriteBarTree.transform.localScale = new Vector3(spriteBarTreeFloat, spriteBarTree.transform.localScale.y, spriteBarTree.transform.localScale.z);
                spriteBarTree.transform.localPosition = new Vector3(0f, spriteBarTree.transform.localPosition.y, spriteBarTree.transform.localPosition.z);
                if (player != null && player.isCuttingWood)
                    player.isCuttingWood = false;
                if (particles.isPlaying)
                    particles.Stop();
            }
            else
            {
                if (player != null && player.isCuttingWood)
                    player.isCuttingWood = false;
            }
        }
        else if (!isIn && nbEnemy == 0)
        {
            if (particles.isPlaying)
                particles.Stop();

            if (realTimeToDamage < playerTimeToDamage && health > 1)
            {
                realTimeToDamage += Time.deltaTime;
                spriteBarTree.transform.localScale = new Vector3(spriteBarTree.transform.localScale.x + (spriteBarTreeFloat / playerTimeToDamage) * Time.deltaTime, spriteBarTree.transform.localScale.y, spriteBarTree.transform.localScale.z);
                spriteBarTree.transform.localPosition = new Vector3(spriteBarTree.transform.localPosition.x +((spriteBarTreeFloat / playerTimeToDamage) * Time.deltaTime) / 2, spriteBarTree.transform.localPosition.y, spriteBarTree.transform.localPosition.z);
            }
            else if (realTimeToDamage > playerTimeToDamage)
            {
                realTimeToDamage = playerTimeToDamage;
                spriteBarTree.transform.localScale = new Vector3(spriteBarTreeFloat, spriteBarTree.transform.localScale.y, spriteBarTree.transform.localScale.z);
                spriteBarTree.transform.localPosition = new Vector3(0f, spriteBarTree.transform.localPosition.y, spriteBarTree.transform.localPosition.z);
            }
        }

        if (nbEnemy > 0 && realEnemyTimeToDamage > 0f)
        {
            if (!particles.isPlaying)
                particles.Play();

            if (player != null && player.isCuttingWood)
                player.isCuttingWood = false;

            realEnemyTimeToDamage -= nbEnemy * Time.deltaTime;

            spriteBarTree.transform.localScale = new Vector3(spriteBarTree.transform.localScale.x - (spriteBarTreeFloat / enemyTimeToDamage) * nbEnemy * Time.deltaTime, spriteBarTree.transform.localScale.y, spriteBarTree.transform.localScale.z);
            spriteBarTree.transform.localPosition = new Vector3(spriteBarTree.transform.localPosition.x - ((spriteBarTreeFloat / enemyTimeToDamage) * nbEnemy * Time.deltaTime) / 2, spriteBarTree.transform.localPosition.y, spriteBarTree.transform.localPosition.z);
        }
        else if (nbEnemy > 0 && realEnemyTimeToDamage <= 0f)
        {
            health = 0;
        }
        else if (particles.isPlaying && nbEnemy == 0)
            particles.Stop();

        if (health <= 0)
        {
            if (boxCollider.enabled)
            {
                boxCollider.enabled = false;
                nbEnemy = 0;
            }
        }
        else
        {
            if (!boxCollider.enabled)
                boxCollider.enabled = true;
        }
    }

    public void PassDay()
    {
        if (health == 1)
        {
            health++;
        }
    }

    public void DestroyTree()
    {
        health = 0;
    }

    public void DoubleHealth()
    {
        health = health * 2;
    }

    public void ShowLife(bool show)
    {
        if (!spriteBarTree.activeSelf && show)
        {
            spriteBarTree.SetActive(true);
            spriteBarWhite.SetActive(true);
        }
        else if (spriteBarTree.activeSelf && !show)
        {
            spriteBarTree.SetActive(false);
            spriteBarWhite.SetActive(false);
        }
    }

    public Tree IsAlive()
    {
        if (health <= 0)
            return null;
        else
            return this;
    }

    public void DamageTree(int damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player == null)
                player = collision.gameObject.GetComponent<Player>();

            if (!InputInfo.activeSelf)
                InputInfo.SetActive(true);

            isIn = true;
        }
        else if (collision.gameObject.CompareTag("Enemy") && health > 0)
        {
            if (nbEnemy < 3)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy.tree == null && enemy.barricade == null)
                {
                    enemy.tree = this;
                    
                    collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x + (enemy.left ? 1f : -1f) * nbEnemy, collision.gameObject.transform.position.y);
                    nbEnemy++;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (InputInfo.activeSelf)
            InputInfo.SetActive(false);

        isIn = false;
        timeToGoUp = playerTimeToDamage - realTimeToDamage;
        distToGoUp = spriteBarTree.transform.localPosition.y - 0.3f;
    }
}
