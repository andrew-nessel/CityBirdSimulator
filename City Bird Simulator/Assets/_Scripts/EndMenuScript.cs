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
    public Text endMenuScoreText;
    public GameObject Medal1Got;
    public GameObject Medal2Got;
    public GameObject Medal3Got;
    public GameObject Medal1Lost;
    public GameObject Medal2Lost;
    public GameObject Medal3Lost;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Victory()
    {
        endMenuText.text = "Mission Accomplished";
        endMenuUI.SetActive(true);
    }

    public void Defeat()
    {
        endMenuText.text = "Mission Failed";
        endMenuUI.SetActive(true);
        endMenuScoreText.text = "";
        endMenuScore.text = "";
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

    public void UpdateScore(int score, int medals)
    {
        endMenuScore.text = score.ToString();
        if(medals == 3)
        {
            Medal1Got.SetActive(true);
            Medal1Lost.SetActive(false);
            Medal2Got.SetActive(true);
            Medal2Lost.SetActive(false);
            Medal3Got.SetActive(true);
            Medal3Lost.SetActive(false);
        }
        else if (medals == 2)
        {
            Medal1Got.SetActive(true);
            Medal1Lost.SetActive(false);
            Medal2Got.SetActive(true);
            Medal2Lost.SetActive(false);
            Medal3Got.SetActive(false);
            Medal3Lost.SetActive(true);
        }
        else if (medals == 1)
        {
            Medal1Got.SetActive(true);
            Medal1Lost.SetActive(false);
            Medal2Got.SetActive(false);
            Medal2Lost.SetActive(true);
            Medal3Got.SetActive(false);
            Medal3Lost.SetActive(true);
        }
        else
        {
            Medal1Got.SetActive(false);
            Medal1Lost.SetActive(true);
            Medal2Got.SetActive(false);
            Medal2Lost.SetActive(true);
            Medal3Got.SetActive(false);
            Medal3Lost.SetActive(true);
        }
    }
}
