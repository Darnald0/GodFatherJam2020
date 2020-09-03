using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Village : MonoBehaviour
{
    private int houseNumber = 0;
    [SerializeField] private Text numberOfHouseToDisplay;
    [SerializeField] public int costToBuild = 1;
    [SerializeField] private int costRisePerNewHouse = 1;
    [SerializeField] private Player player;
    [SerializeField] private int gainPerHouse = 2;
    private int baseGainPerHouse;
    private bool isInVillage;

    private void Start()
    {
        baseGainPerHouse = gainPerHouse;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && isInVillage && player.wood >= costToBuild)
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
        player.wood -= costToBuild;
        houseNumber++;
        costToBuild += costRisePerNewHouse;
        numberOfHouseToDisplay.text = houseNumber.ToString();
    }

    public void GainPassif()
    {
        player.wood += houseNumber * gainPerHouse;
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
