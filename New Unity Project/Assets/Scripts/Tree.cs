using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private int health = 2;
    private float realTimeToDamage = 2f;
    private bool isIn = false;
    private Player player = null;

    private float spriteBarTreeFloat = 0f;

    private float timeToGoUp = 0f;
    private float distToGoUp = 0f;

    [SerializeField] private float timeToDamage = 2f;
    [SerializeField] private int woodsGiven = 2;
    [SerializeField] private GameObject spriteBarTree = null;
    [SerializeField] private GameObject spriteBarWhite = null;
    private ParticleSystem particles = null;

    private void Start()
    {
        realTimeToDamage = timeToDamage;
        spriteBarTreeFloat = spriteBarTree.transform.localScale.x;
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (isIn)
        {
            if (Input.GetKey(KeyCode.E) && realTimeToDamage > 0f && health > 1)
            {
                if (!particles.isPlaying)
                    particles.Play();

                realTimeToDamage -= Time.deltaTime;

                spriteBarTree.transform.localScale = new Vector3(spriteBarTree.transform.localScale.x - (spriteBarTreeFloat / timeToDamage) * Time.deltaTime, spriteBarTree.transform.localScale.y, spriteBarTree.transform.localScale.z);
                spriteBarTree.transform.localPosition = new Vector3(spriteBarTree.transform.localPosition.x - ((spriteBarTreeFloat / timeToDamage) * Time.deltaTime) / 2, spriteBarTree.transform.localPosition.y, spriteBarTree.transform.localPosition.z);
            }
            else if (realTimeToDamage <= 0f)
            {
                realTimeToDamage = timeToDamage;
                health--;
                player.wood += woodsGiven;
                transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                spriteBarTree.transform.localScale = new Vector3(spriteBarTreeFloat, spriteBarTree.transform.localScale.y, spriteBarTree.transform.localScale.z);
                spriteBarTree.transform.localPosition = new Vector3(0f, spriteBarTree.transform.localPosition.y, spriteBarTree.transform.localPosition.z);
            }
            else
            {
                if (particles.isPlaying)
                    particles.Stop();
            }
        }
        else
        {
            if (particles.isPlaying)
                particles.Stop();

            if (realTimeToDamage < timeToDamage && health > 1)
            {
                realTimeToDamage += Time.deltaTime;
                spriteBarTree.transform.localScale = new Vector3(spriteBarTree.transform.localScale.x + (spriteBarTreeFloat / timeToDamage) * Time.deltaTime, spriteBarTree.transform.localScale.y, spriteBarTree.transform.localScale.z);
                spriteBarTree.transform.localPosition = new Vector3(spriteBarTree.transform.localPosition.x +((spriteBarTreeFloat / timeToDamage) * Time.deltaTime) / 2, spriteBarTree.transform.localPosition.y, spriteBarTree.transform.localPosition.z);
            }
            else if (realTimeToDamage > timeToDamage)
            {
                realTimeToDamage = timeToDamage;
                spriteBarTree.transform.localScale = new Vector3(spriteBarTreeFloat, spriteBarTree.transform.localScale.y, spriteBarTree.transform.localScale.z);
                spriteBarTree.transform.localPosition = new Vector3(0f, spriteBarTree.transform.localPosition.y, spriteBarTree.transform.localPosition.z);
            }
        }

        if (health == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void EnnemyAttack(int damage)
    {
        health -= damage;
    }

    public void PassDay()
    {
        if (health == 1)
        {
            health++;
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (player == null)
            player = collision.gameObject.GetComponent<Player>();

        isIn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        isIn = false;
        timeToGoUp = timeToDamage - realTimeToDamage;
        distToGoUp = spriteBarTree.transform.localPosition.y - 0.3f;
    }
}
