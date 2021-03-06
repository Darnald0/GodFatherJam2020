﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [Header("Do Not Touch")]
    public bool isCreate = false;
    public int nbEnemy = 0;

    [Header("Change This")]
    [SerializeField] private int health = 10;
    [SerializeField] private int costCreate = 0;
    [SerializeField] private float timeToCreate = 2f;
    [SerializeField] private GameObject spriteLifeBar = null;
    [SerializeField] private GameObject spriteLifeBarWhite = null;
    
    private int costRepair = 0;

    private Player player = null;
    public int canBuild = 0;
    private float timeCreate = -1f;
    private SpriteRenderer spriteRenderer = null;
    private float lifeBarFloat = 0f;
    private int maxHealth = 0;
    private Color notBuildingColor = Color.white;

    PlayOneSound soundScript;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        notBuildingColor = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
        spriteRenderer.color = notBuildingColor;
        lifeBarFloat = spriteLifeBar.transform.localScale.x;
        maxHealth = health;
        soundScript = GetComponent<PlayOneSound>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && !isCreate && timeCreate == -1f)
        {
            player.buildBarricade = false;
            Destroy(gameObject);
        }

        if (!isCreate && timeCreate == -1f && timeCreate == -1f)
        {
            transform.position = new Vector3(player.transform.position.x - 6f * player.transform.localScale.x, transform.position.y);
            if (costCreate > player.wood)
            {
                Color red = new Vector4(Color.red.r, Color.red.g, Color.red.b, 0.2f);
                spriteRenderer.color = red;
            }
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !isCreate)
        {
            if (player.buildBarricade && costCreate <= player.wood && canBuild == 0 && timeCreate == -1f)
            {
                player.wood -= costCreate;
                canBuild = 0;
                gameObject.transform.parent = player.barricadeManager.transform;
                player.barricadeManager.barricades.Add(this);
                Destroy(GetComponent<Rigidbody2D>());
                player.buildBarricade = false;
                timeCreate = timeToCreate;
                player.isBuilding = true;
                // Play anim here
            }
            else if (player.buildBarricade && (costCreate > player.wood || canBuild > 0) && timeCreate == -1f)
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

        if (health <= 0 && isCreate)
        {
            soundScript.PlaySound();
            Destroy(gameObject);
        }
    }

    public void DoubleHealth()
    {
        health = health * 2;
    }

    public void AttackBarricade(int damage)
    {
        health -= damage;

        spriteLifeBar.transform.localScale = new Vector3(spriteLifeBar.transform.localScale.x - (lifeBarFloat / maxHealth) * damage, spriteLifeBar.transform.localScale.y, spriteLifeBar.transform.localScale.z);
        spriteLifeBar.transform.localPosition = new Vector3(spriteLifeBar.transform.localPosition.x - ((lifeBarFloat / maxHealth) * damage) / 2, spriteLifeBar.transform.localPosition.y, spriteLifeBar.transform.localPosition.z);
    }

    public Barricade IsAlive()
    {
        if (health <= 0)
            return null;
        else
            return this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && isCreate)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.barricade == null && enemy.tree == null)
            {
                enemy.barricade = this;
                collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x + (enemy.left ? 1f : -1f) * (float)nbEnemy / 4f, collision.gameObject.transform.position.y);
                nbEnemy++;
            }
        }
        else if (collision.tag != "Enemy" && !isCreate)
        {
            if (costCreate <= player.wood)
                canBuild++;
            Color red = new Vector4(Color.red.r, Color.red.g, Color.red.b, 0.2f);
            spriteRenderer.color = red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ennemy")
            return;

        if (!isCreate)
        {
            canBuild--;
            if (canBuild == 0 && costCreate <= player.wood)
            {
                spriteRenderer.color = notBuildingColor;
                return;
            }
        }
    }
}
