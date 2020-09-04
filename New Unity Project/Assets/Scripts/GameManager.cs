using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject startGamePanel;
    public GameObject intro;
    public GameObject button;

    Intro introSCript;

    private void Start()
    {
        introSCript = intro.GetComponent<Intro>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && introSCript.introFinish)
        {
            SceneManager.LoadScene("Game");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Instruction()
    {
        instructionPanel.SetActive(true);
    }

    public void Return()
    {
        instructionPanel.SetActive(false);
    }

    public void StartGamePanel()
    {
        startGamePanel.SetActive(true);
    }

    public void StartIntro()
    {
        introSCript.startIntro = true;
        button.SetActive(false);
    }
}
