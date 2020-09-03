using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Idol : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject sky;
    [SerializeField] private GameObject village;
    [SerializeField] private GameObject numberOfOfferingNeeded;
    [SerializeField] private GameObject offeringNumber;
    [SerializeField] private GameObject[] arrayOffering;
    [SerializeField] private GameObject treeManager;
    [SerializeField] private GameObject barricadeManager;
    private Text numberOfOfferingNeededDisplay;
    private int minimalNumberOfWoodNeeded;
    private bool isInIdol;
    private int currentDay;
    private int numberOfWoodOffered;
    private bool alreadyMadeAnOffering = false;
    private bool offeringState = false;
    private int offeringIndex = 0;

    Player playerScript;
    Sky skyScript;
    Village villageScript;
    TreeManager treeManagerScript;
    BarricadeManager barricadeManagerScript;

    private void Start()
    {
        barricadeManagerScript = barricadeManager.GetComponent<BarricadeManager>();
        playerScript = player.GetComponent<Player>();
        skyScript = sky.GetComponent<Sky>();
        villageScript = village.GetComponent<Village>();
        treeManagerScript = treeManager.GetComponent<TreeManager>();
        numberOfOfferingNeededDisplay = numberOfOfferingNeeded.GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !alreadyMadeAnOffering && !offeringState && isInIdol)
        {
            offeringNumber.SetActive(true);
            player.isStayingIdol = true;
            offeringState = true;
        }
        else
        {

            arrayOffering[offeringIndex].GetComponent<Text>().fontStyle = FontStyle.Bold;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (offeringIndex > 0)
                {
                    arrayOffering[offeringIndex].GetComponent<Text>().fontStyle = FontStyle.Normal;
                    offeringIndex--;
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (offeringIndex < 3)
                {
                    arrayOffering[offeringIndex].GetComponent<Text>().fontStyle = FontStyle.Normal;
                    offeringIndex++;
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (offeringIndex)
                {
                    case 0:
                        offeringNumber.SetActive(false);
                        offeringState = false;
                        player.isStayingIdol = false;
                        break;
                    case 1:
                        if (playerScript.wood >= 3)
                        {
                            Buff(offeringIndex);
                            alreadyMadeAnOffering = true;
                            offeringNumber.SetActive(false);
                            offeringState = false;
                            player.isStayingIdol = false;
                        }
                        break;
                    case 2:
                        if (playerScript.wood >= 6)
                        {
                            Buff(offeringIndex);
                            alreadyMadeAnOffering = true;
                            offeringNumber.SetActive(false);
                            offeringState = false;
                            player.isStayingIdol = false;
                        }
                        break;
                    case 3:
                        if (playerScript.wood >= 9)
                        {
                            Buff(offeringIndex);
                            alreadyMadeAnOffering = true;
                            offeringNumber.SetActive(false);
                            offeringState = false;
                            player.isStayingIdol = false;
                        }
                        break;
                    default:
                        Debug.Log("Offering Error");
                        break;
                }
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isInIdol = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isInIdol = false;
        }
    }

    public void CheckDay()
    {
        alreadyMadeAnOffering = false;
        currentDay = sky.GetComponent<Sky>().numberOfDay;

        switch (currentDay)
        {
            case 1:
                numberOfWoodOffered = 0;
                break;

            case 2:
                numberOfWoodOffered = 0;
                minimalNumberOfWoodNeeded = 2;
                numberOfOfferingNeededDisplay.text = minimalNumberOfWoodNeeded.ToString();
                if (numberOfWoodOffered < minimalNumberOfWoodNeeded)
                {
                    Debuff();
                }
                break;

            case 3:
                numberOfWoodOffered = 0;
                minimalNumberOfWoodNeeded = 3;
                numberOfOfferingNeededDisplay.text = minimalNumberOfWoodNeeded.ToString();
                if (numberOfWoodOffered < minimalNumberOfWoodNeeded)
                {
                    Debuff();
                }
                break;

            case 4:
                numberOfWoodOffered = 0;
                minimalNumberOfWoodNeeded = 4;
                numberOfOfferingNeededDisplay.text = minimalNumberOfWoodNeeded.ToString();
                if (numberOfWoodOffered < minimalNumberOfWoodNeeded)
                {
                    Debuff();
                }
                break;
            case 5:
                numberOfWoodOffered = 0;
                minimalNumberOfWoodNeeded = 5;
                numberOfOfferingNeededDisplay.text = minimalNumberOfWoodNeeded.ToString();
                if (numberOfWoodOffered < minimalNumberOfWoodNeeded)
                {
                    Debuff();
                }
                break;
            case 6:
                Debug.Log("gg");
                break;
        }
    }

    private void Debuff()
    {
        int randomDebuff = Random.Range(1, 5);
        switch (randomDebuff)
        {
            case 1:
                playerScript.HalfWood();
                break;
            case 2:
                treeManagerScript.DamageTree();
                break;
            case 3:
                treeManagerScript.DestroyTree();
                break;
            case 4:
                villageScript.DestroyAHouse();
                break;
            case 5:
                villageScript.HalfPassivGain();
                break;
            default:
                Debug.Log("Debbuff Error");
                break;
        }
    }

    public void Buff(int numberOfOffering)
    {
        int randomBuff;
        switch (numberOfOffering)
        {
            case 1:
                randomBuff = Random.Range(1, 2);
                if (randomBuff == 1)
                {
                    villageScript.PlusOneVillager();
                }
                else
                {
                    playerScript.DoubleWood();
                }

                break;
            case 2:
                randomBuff = Random.Range(1, 2);
                if (randomBuff == 1)
                {
                    treeManagerScript.DoubleTreeLife();
                }
                else //if (randomBuff == 2)
                {
                    villageScript.DoublePassifGain();
                }
                //else
                //{
                //    //+1 tree in tree list
                //}

                break;
            case 3:
                randomBuff = Random.Range(1, 2);
                if (randomBuff == 1)
                {
                    barricadeManagerScript.DoubleBarricadeHealth();
                }
                else
                {
                    //cut wave in half
                }
                break;
        }
    }
}
