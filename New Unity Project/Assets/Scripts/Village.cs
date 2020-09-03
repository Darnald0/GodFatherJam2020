using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Village : MonoBehaviour
{
    private int houseNumber = 0;
    [SerializeField] private GameObject numberOfHouse;
    [SerializeField] public int costToBuild = 1;
    [SerializeField] private int costRisePerNewHouse = 1;
    [SerializeField] private GameObject player;
    [SerializeField] private int gainPerHouse = 2;
    private int baseGainPerHouse;
    private bool isInVillage;
    private Text numberOfHouseToDisplay;

    private void Start()
    {
        baseGainPerHouse = gainPerHouse;
        numberOfHouseToDisplay = numberOfHouse.GetComponent<Text>();
    }

    private void Update()
    {
        numberOfHouseToDisplay.text = "" + houseNumber;
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

    public void DestroyAHouse()
    {
        houseNumber--;
    }

    public void HalfPassivGain()
    {
        gainPerHouse = gainPerHouse / 2;
    }

    public void PlusOneVillager()
    {
        houseNumber++;
    }

    public void DoublePassifGain()
    {
        gainPerHouse = gainPerHouse * 2;
    }

    public void ResetPassifGain()
    {
        gainPerHouse = baseGainPerHouse;
    }
}
