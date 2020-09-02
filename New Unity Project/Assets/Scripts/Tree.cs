using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private int health = 2;
    private float realTimeToDamage = 2f;
    private bool isIn = false;
    private Player player = null;

    private float timeToGoUp = 0f;
    private float distToGoUp = 0f;

    [SerializeField] private float timeToDamage = 2f;
    [SerializeField] private int woodsGiven = 2;
    [SerializeField] private GameObject spriteBarTree = null;
    [SerializeField] private GameObject spriteBarWhite = null;

    public KeyCode touche = KeyCode.None;



    private void Start()
    {
        realTimeToDamage = timeToDamage;
    }

    private void Update()
    {
        if (isIn)
        {
            if (Input.GetKey(KeyCode.E) && realTimeToDamage > 0f && health > 1)
            {
                realTimeToDamage -= Time.deltaTime;
                spriteBarTree.transform.localPosition = new Vector3(spriteBarTree.transform.localPosition.x, spriteBarTree.transform.localPosition.y - (spriteBarTree.transform.localScale.y / timeToDamage) * Time.deltaTime, spriteBarTree.transform.localPosition.z);
            }
            else if (realTimeToDamage <= 0f)
            {
                realTimeToDamage = timeToDamage;
                health--;
                player.wood += woodsGiven;
                transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                spriteBarTree.transform.localPosition = new Vector3(spriteBarTree.transform.localPosition.x, 0.3f, spriteBarTree.transform.localPosition.z);
            }
        }
        else
        {
            if (realTimeToDamage < timeToDamage && health > 1)
            {
                realTimeToDamage += Time.deltaTime;
                spriteBarTree.transform.localPosition = new Vector3(spriteBarTree.transform.localPosition.x, spriteBarTree.transform.localPosition.y - ( distToGoUp / timeToGoUp) * Time.deltaTime, spriteBarTree.transform.localPosition.z);
            }
            else if (realTimeToDamage > timeToDamage)
            {
                realTimeToDamage = timeToDamage;
                spriteBarTree.transform.localPosition = new Vector3(spriteBarTree.transform.localPosition.x, 0.3f, spriteBarTree.transform.localPosition.z);
            }
        }

        if (health == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void EnnemyAttack()
    {

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
