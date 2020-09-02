using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    private int houseNumber = 0;
    [SerializeField] public int costToBuild = 1;
    [SerializeField] private int costRisePerNewHouse = 1;
    [SerializeField] private int gainPerHouse = 2;
    public GameObject player;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && Input.GetKeyDown(KeyCode.S))
        {
            if (player.GetComponent<Player>().wood >= costToBuild)
            {
                BuildHouse();
            }
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
