using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject[] textIntro;
    public GameObject imageBackground;

    private int currentIndex = 0;
    public bool introFinish = false;
    public bool startIntro = false;
    GameManager gmScript;

    void Start()
    {
        gmScript = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        if (startIntro)
        {
            textIntro[currentIndex].SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.E) && startIntro)
        {
            currentIndex++;
            if(currentIndex == textIntro.Length)
            {
                gmScript.StartGamePanel();
                introFinish = true;
            }
        }
    }
}
