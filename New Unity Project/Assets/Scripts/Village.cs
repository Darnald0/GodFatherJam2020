﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    private int houseNumber = 0;
    [SerializeField] public int costToBuild = 1;
    [SerializeField] private int costRisePerNewHouse = 1;
    [SerializeField] private int gainPerHouse = 2;
    [SerializeField] private GameObject player;
    private bool isInVillage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && isInVillage && player.GetComponent<Player>().wood >= costToBuild)
        {
            BuildHouse();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isInVillage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isInVillage = false;
        }
    }

    public void BuildHouse()
    {
        player.GetComponent<Player>().wood -= costToBuild;
        houseNumber++;
        costToBuild += costRisePerNewHouse;
    }

    public void GainPassif()
    {
        player.GetComponent<Player>().wood += houseNumber * gainPerHouse;
    }
}
