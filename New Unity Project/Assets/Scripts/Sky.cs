using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sky : MonoBehaviour
{
    private float timer = 0.0f;
    private int seconds = 0;
    public bool isNight = false;
    public int numberOfDay = 1;
    [SerializeField] private float timeInSecondToPassTheDay;
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject village;
    [SerializeField] private GameObject idol;
    private SpriteRenderer sr;
    private Image fadeImage;

    Village villageScript;
    // Start is called before the first frame update
    void Start()
    {
        
        fadeImage = fade.GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();
        villageScript = village.GetComponent<Village>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer;

        if (seconds == timeInSecondToPassTheDay)
        {
            if (!isNight)
            {
                StartCoroutine(FadeImageNight());
                isNight = true;
            }
            else if (isNight)
            {
                StartCoroutine(FadeImageDay());
                villageScript.GainPassif();
                villageScript.ResetPassifGain(); 
                isNight = false;
                numberOfDay++;
                idol.GetComponent<Idol>().CheckDay();
            }
            timer = 0;
        }
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

        sr.color = nightColor;

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

        sr.color = dayColor;

        tempColor = fadeImage.color;
        for (float j = 1; j >= 0; j -= 0.01f)
        {
            tempColor.a = j;
            fadeImage.color = tempColor;
            yield return null;
        }
    }
}
