using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject startGamePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
}
