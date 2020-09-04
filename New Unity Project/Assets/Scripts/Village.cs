using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Village : MonoBehaviour
{
    public int houseNumber = 0;
    [SerializeField] private Text numberOfHouseToDisplay;
    [SerializeField] public int costToBuild = 1;
    [SerializeField] private int costRisePerNewHouse = 1;
    [SerializeField] private Player player;
    [SerializeField] private int gainPerHouse = 2;
    [SerializeField] private GameObject InputTouch = null;
    private int baseGainPerHouse;
    private bool isInVillage;

    private void Start()
    {
        InputTouch.SetActive(false);
        baseGainPerHouse = gainPerHouse;
    }

    private void Update()
    {
        numberOfHouseToDisplay.text = houseNumber.ToString();
        /*if(Input.GetKeyDown(KeyCode.B))
        {
            houseNumber++;
        }*/
        if (Input.GetKeyDown(KeyCode.E) && isInVillage && player.wood >= costToBuild)
        {
            BuildHouse();
        }
        if (player.wood < costToBuild && InputTouch.activeSelf)
            InputTouch.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !isInVillage)
        {
            if (player.wood >= costToBuild)
                InputTouch.SetActive(true);
            isInVillage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" && isInVillage)
        {
            InputTouch.SetActive(false);
            isInVillage = false;
        }
    }

    public void BuildHouse()
    {
        player.wood -= costToBuild;
        houseNumber++;
        costToBuild += costRisePerNewHouse;

    }

    public void GainPassif()
    {
        player.wood += houseNumber * gainPerHouse;
    }

    public void DestroyAHouse()
    {
        houseNumber = houseNumber - 1;

    }

    public void HalfPassivGain()
    {
        gainPerHouse = gainPerHouse / 2;
    }

    public void PlusOneVillager()
    {
        houseNumber = houseNumber + 1;
        Debug.Log(houseNumber);
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
