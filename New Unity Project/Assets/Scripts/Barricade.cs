using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    private Player player = null;
    private bool canBuild = true;
    private float timeCreate = -1f;
    private SpriteRenderer spriteRenderer = null;
    private float lifeBarFloat = 0f;
    private int maxHealth = 0;
    private Color notBuildingColor = Color.white;

    [SerializeField] private GameObject spriteLifeBar = null;
    [SerializeField] private GameObject spriteLifeBarWhite = null;
    [Space]
    [SerializeField] private float timeToCreate = 2f;
    [SerializeField] private int costCreate = 0;
    private int costRepair = 0;
    [SerializeField] private int health = 10;
    public bool isCreate = false;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        notBuildingColor = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
        spriteRenderer.color = notBuildingColor;
        lifeBarFloat = spriteLifeBar.transform.localScale.x;
        maxHealth = health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isCreate && timeCreate == -1f)
        {
            player.buildBarricade = false;
            Destroy(gameObject);
        }

        if (!isCreate && timeCreate == -1f && player.isStayingIdol)
        {
            player.buildBarricade = false;
            Destroy(gameObject);
        }

        if (!isCreate && timeCreate == -1f && timeCreate == -1f)
        {
            transform.position = new Vector3(player.transform.position.x + 2f * player.transform.localScale.x, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.S) && !isCreate)
        {
            if (player.buildBarricade && costCreate <= player.wood && canBuild && timeCreate == -1f)
            {
                player.wood -= costCreate;
                canBuild = false;
                gameObject.transform.parent = null;
                Destroy(GetComponent<Rigidbody2D>());
                player.buildBarricade = false;
                timeCreate = timeToCreate;
                player.isBuilding = true;
                // Play anim here
            }
            else if (player.buildBarricade && (costCreate > player.wood || !canBuild) && timeCreate == -1f)
            {
                player.buildBarricade = false;
                Destroy(gameObject);
            }
        }

        if (timeCreate >= 0f && !isCreate)
        {
            timeCreate -= Time.deltaTime;
            if (spriteRenderer.color.a < 1.0f)
                spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + (0.8f / timeToCreate) * Time.deltaTime);
        }
        else if (timeCreate > -0.5f && timeCreate <= 0f)
        {
            isCreate = true;
            timeCreate = -1f;
            player.isBuilding = false;
        }
    }

    public void AttackBarricade(int damage)
    {
        health -= damage;

        spriteLifeBar.transform.localScale = new Vector3(spriteLifeBar.transform.localScale.x - (lifeBarFloat / maxHealth) * damage, spriteLifeBar.transform.localScale.y, spriteLifeBar.transform.localScale.z);
        spriteLifeBar.transform.localPosition = new Vector3(spriteLifeBar.transform.localPosition.x - ((lifeBarFloat / maxHealth) * damage) / 2, spriteLifeBar.transform.localPosition.y, spriteLifeBar.transform.localPosition.z);
        
        if (health <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ennemy")
            return;

        if (!isCreate)
        {
            canBuild = false;
            Color grey = new Vector4(Color.grey.r, Color.grey.g, Color.grey.b, 0.2f);
            spriteRenderer.color = grey;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ennemy")
            return;

        if (!isCreate)
        {
            if (canBuild)
                return;

            canBuild = true;
            spriteRenderer.color = notBuildingColor;
        }
    }
}
