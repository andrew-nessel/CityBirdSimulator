using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenuScript : MonoBehaviour {

    public bool isGameOver = false;
    public bool isVictory = false;

    public GameObject endMenuUI;
    public Text endMenuText;
    public Text endMenuScore;

    // Update is called once per frame
    void Update()
    {

    }

    public void Victory()
    {
        endMenuText.text = "You Won!";
        endMenuUI.SetActive(true);
    }

    public void Defeat()
    {
        endMenuText.text = "You Lost!";
        endMenuUI.SetActive(true);
    }


    public void Restart()
    {
        endMenuUI.SetActive(false);
        SceneManager.LoadScene("Demo_Level");
    }
    

    public void Menu()
    {
        endMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateScore(int score)
    {
        endMenuScore.text = score.ToString();
    }
}
