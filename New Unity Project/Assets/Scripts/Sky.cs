using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    [SerializeField] private float timeInSecondToPassTheDay;
    private float timer = 0.0f;
    private int seconds = 0;
    public bool isNight = false;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer;

        if(seconds == timeInSecondToPassTheDay)
        {
            timer = 0;
            if (!isNight)
            {
                isNight = true;
                sr.color = Color.black;
            }
            else
            {
                isNight = false;
                sr.color = Color.blue;
            }
        }
    }
}
