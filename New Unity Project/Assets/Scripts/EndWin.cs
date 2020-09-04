using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndWin : MonoBehaviour
{
    [SerializeField] private GameObject ALL = null;
    [SerializeField] private Image imgEnding = null;
    [SerializeField] private Sprite Lose = null;
    [SerializeField] private Sprite Win = null;

    public void YouWin()
    {
        ALL.SetActive(false);
        imgEnding.color = Color.white;
        imgEnding.sprite = Win;
    }

    public void YouLose()
    {
        ALL.SetActive(false);
        imgEnding.color = Color.white;
        imgEnding.sprite = Lose;
    }

    private void Update()
    {
        if (imgEnding.sprite != null && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            SceneManager.LoadSceneAsync(0);
        }
    }
}
