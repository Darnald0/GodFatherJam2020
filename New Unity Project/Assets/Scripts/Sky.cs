using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sky : MonoBehaviour
{
    private float timer = 0.0f;
    private int seconds = 0;
    public bool isNight = false;
    public int numberOfDay = 0;
    [SerializeField] private float timeInSecondToPassTheDay;
    [SerializeField] private Sprite dayColor;
    [SerializeField] private Sprite nightColor;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Village villageScript;
    [SerializeField] private Idol idol;
    [SerializeField] private TreeManager treeManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private Sprite[] dayList;
    [SerializeField] private GameObject dayFeedback;
    [SerializeField] private GameObject bg1;
    [SerializeField] private GameObject bg2;
    private SpriteRenderer sr;
    private Image daySr;
    PlayMultipleSound soundScript;
    SpriteRenderer bg1sr;
    SpriteRenderer bg2sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        soundScript = GetComponent<PlayMultipleSound>();
        soundScript.PlaySound(TYPE_AUDIO.MusicDay);
        daySr = dayFeedback.GetComponent<Image>();

        bg1sr = bg1.GetComponent<SpriteRenderer>();
        bg2sr = bg2.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer;

        if (seconds == timeInSecondToPassTheDay)
        {
            PassDay();
        }
    }

    private void PassDay()
    {
        if (!isNight)
        {
            soundScript.PlaySound(TYPE_AUDIO.MusicNight);
            StartCoroutine(FadeImageNight());
            isNight = true;
            enemyManager.PassNight();
            idol.CheckIfEnoughOffering(idol.minimalNumberOfWoodNeeded, idol.numberOfWoodOffered);
        }
        else if (isNight)
        {
            soundScript.PlaySound(TYPE_AUDIO.MusicDay);
            StartCoroutine(FadeImageDay());
            isNight = false;
            villageScript.GainPassif();
            villageScript.ResetPassifGain();
            idol.CheckDay();
            treeManager.PassDay();
            enemyManager.PassDay();
            numberOfDay++;
            daySr.sprite = dayList[numberOfDay - 2]; 
        }
        timer = 0;
    }

    IEnumerator FadeImageNight()
    {
        var tempColor = fadeImage.color;
        for (float i = 0; i <= 1; i += 0.01f)
        {
            tempColor.a = i;
            fadeImage.color = tempColor;
            yield return null;
        }

        sr.sprite = nightColor;
        bg1sr.sprite = nightColor;
        bg2sr.sprite = nightColor;
        Debug.Log("night");

        tempColor = fadeImage.color;
        for (float j = 1; j >= 0.2f; j -= 0.01f)
        {
            tempColor.a = j;
            fadeImage.color = tempColor;
            yield return null;
        }
    }

    IEnumerator FadeImageDay()
    {
        var tempColor = fadeImage.color;
        for (float i = 0; i <= 1; i += 0.01f)
        {
            tempColor.a = i;
            fadeImage.color = tempColor;
            yield return null;
        }

        sr.sprite = dayColor;
        bg1sr.sprite = dayColor;
        bg2sr.sprite = dayColor;

        tempColor = fadeImage.color;
        for (float j = 1; j >= 0; j -= 0.01f)
        {
            tempColor.a = j;
            fadeImage.color = tempColor;
            yield return null;
        }
    }
}
