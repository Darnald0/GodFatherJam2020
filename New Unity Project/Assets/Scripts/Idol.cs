using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idol : MonoBehaviour
{
    [SerializeField] private int numberOfWoodNeeded;
    [SerializeField] private int numberOfDayWithoutOfferingBeforeDisaster = 3;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject sky;
    private bool isInIdol;

    void Update()
    {
        //malus
        if (numberOfDayWithoutOfferingBeforeDisaster == sky.GetComponent<Sky>().numberOfDay)
        {
            Debug.Log("disaster");
        }

        //bonus
        if(Input.GetKeyDown(KeyCode.S) && player.GetComponent<Player>().wood >= numberOfWoodNeeded)
        {
            Debug.Log("offering");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            isInIdol = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            isInIdol = false;
        }
    }
}
