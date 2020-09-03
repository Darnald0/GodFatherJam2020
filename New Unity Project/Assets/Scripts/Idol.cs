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
    [SerializeField] private GameObject godVoice;
    private Text godVoiceDisplay;
    private Text numberOfOfferingNeededDisplay;
    public int minimalNumberOfWoodNeeded;
    private bool isInIdol;
    private int currentDay;
    public int numberOfWoodOffered;
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
        godVoiceDisplay = godVoice.GetComponent<Text>();
        barricadeManagerScript = barricadeManager.GetComponent<BarricadeManager>();
        playerScript = player.GetComponent<Player>();
        skyScript = sky.GetComponent<Sky>();
        villageScript = village.GetComponent<Village>();
        treeManagerScript = treeManager.GetComponent<TreeManager>();
        numberOfOfferingNeededDisplay = numberOfOfferingNeeded.GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !alreadyMadeAnOffering && !offeringState && isInIdol && !skyScript.isNight)
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

            if(Input.GetKeyDown(KeyCode.Z))
            {
                offeringNumber.SetActive(false);
                offeringState = false;
                player.isStayingIdol = false;
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
                            playerScript.wood -= 3;
                            numberOfWoodOffered = 3;
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
                            playerScript.wood -= 6;
                            numberOfWoodOffered = 6;
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
                            playerScript.wood -= 9;
                            numberOfWoodOffered = 9;
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
                break;

            case 3:
                numberOfWoodOffered = 0;
                minimalNumberOfWoodNeeded = 3;
                numberOfOfferingNeededDisplay.text = minimalNumberOfWoodNeeded.ToString();
                break;

            case 4:
                numberOfWoodOffered = 0;
                minimalNumberOfWoodNeeded = 4;
                numberOfOfferingNeededDisplay.text = minimalNumberOfWoodNeeded.ToString();
                break;
            case 5:
                numberOfWoodOffered = 0;
                minimalNumberOfWoodNeeded = 5;
                numberOfOfferingNeededDisplay.text = minimalNumberOfWoodNeeded.ToString();
                break;
            case 6:
                Debug.Log("gg");
                break;
        }
    }

    public void CheckIfEnoughOffering(int quantityOfOfferingNeeded, int quantityOfOffering)
    {
        if (quantityOfOffering < quantityOfOfferingNeeded)
        {
            Debuff();
        }
    }

    private void Debuff()
    {
        int randomDebuff = Random.Range(1, 5);
        switch (randomDebuff)
        {
            case 1:
                playerScript.HalfWood();
                godVoiceDisplay.color = new Color(0, 0, 0, 1);
                godVoiceDisplay.text = "Si je ne peux pas l'avoir, personne ne l'aura !";
                StartCoroutine(GodVoiceFade());
                break;
            case 2:
                treeManagerScript.DamageTree();
                godVoiceDisplay.color = new Color(0, 0, 0, 1);
                godVoiceDisplay.text = "Roulez jeunesse !";
                StartCoroutine(GodVoiceFade());
                break;
            case 3:
                treeManagerScript.DestroyTree();
                    godVoiceDisplay.color = new Color(0, 0, 0, 1);
                godVoiceDisplay.text = "Pas de bras, pas d'arbre";
                StartCoroutine(GodVoiceFade());
                break;
            case 4:
                villageScript.DestroyAHouse();
                    godVoiceDisplay.color = new Color(0, 0, 0, 1);
                godVoiceDisplay.text = "Du balais !";
                StartCoroutine(GodVoiceFade());
                break;
            case 5:
                villageScript.HalfPassivGain();
                    godVoiceDisplay.color = new Color(0, 0, 0, 1);
                godVoiceDisplay.text = "Bande de faibles";
                StartCoroutine(GodVoiceFade());
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
                    godVoiceDisplay.color = new Color(0, 0, 0, 1);
                    godVoiceDisplay.text = "Voilà une nouvelle âme pure pour ton clan !";
                    StartCoroutine(GodVoiceFade());
                }
                else
                {
                    playerScript.DoubleWood();
                    godVoiceDisplay.color = new Color(0, 0, 0, 1);
                    godVoiceDisplay.text = "Un peu de bois avec ton bois ?";
                    StartCoroutine(GodVoiceFade());
                }

                break;
            case 2:
                randomBuff = Random.Range(1, 2);
                if (randomBuff == 1)
                {
                    treeManagerScript.DoubleTreeLife();
                    godVoiceDisplay.color = new Color(0, 0, 0, 1);
                    godVoiceDisplay.text = "J'ai béni la forêt de ma protection";
                    StartCoroutine(GodVoiceFade());
                }
                else //if (randomBuff == 2)
                {
                    villageScript.DoublePassifGain();
                    godVoiceDisplay.color = new Color(0, 0, 0, 1);
                    godVoiceDisplay.text = "Mate moi ces biscotos";
                    StartCoroutine(GodVoiceFade());
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
                    godVoiceDisplay.color = new Color(0, 0, 0, 1);
                    godVoiceDisplay.text = "Vous ne passerez pas !";
                    StartCoroutine(GodVoiceFade());
                }
                else
                {
                    //cut wave in half
                    godVoiceDisplay.color = new Color(0, 0, 0, 1);
                    godVoiceDisplay.text = "Allez, ça dégage";
                    StartCoroutine(GodVoiceFade());
                }
                break;
        }
    }

    IEnumerator GodVoiceFade()
    {
        yield return new WaitForSeconds(3);
        var tempColor = godVoiceDisplay.color;
        for (float i = 1; i >= 0; i -= 0.001f)
        {
            tempColor.a = i;
            godVoiceDisplay.color = tempColor;
            yield return null;
        }
    }
}
