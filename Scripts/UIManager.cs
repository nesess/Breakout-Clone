using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //game panel
    [SerializeField]
    private GameObject gamePanel;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Image life1;
    [SerializeField]
    private Image life2;
    [SerializeField]
    private Image life3;

    //main panel

    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Text nameText;
    private string name;
    [SerializeField]
    private InputField nameInput;

    //high scores
    [SerializeField]
    private GameObject highScorePanel;

    [SerializeField]
    private Text scoreName1;
    [SerializeField]
    private Text scoreName2;
    [SerializeField]
    private Text scoreName3;
    [SerializeField]
    private Text scoreName4;
    [SerializeField]
    private Text scoreName5;

    [SerializeField]
    private Text scoreNum1;
    [SerializeField]
    private Text scoreNum2;
    [SerializeField]
    private Text scoreNum3;
    [SerializeField]
    private Text scoreNum4;
    [SerializeField]
    private Text scoreNum5;


    //game over screen
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text gameOverScore;

    // nextGame screen
    [SerializeField]
    private GameObject nextGamePanel;
    [SerializeField]
    private Text nextGameScore;
    [SerializeField]
    private Text nextGamCong;
    


    private int totalScore;
    public static UIManager instance;
    



    private void Awake()
    {
        if (UIManager.instance)
        {
            Destroy(base.gameObject);
        }
        else
        {
            UIManager.instance = this;
        }
        
    }

    private void updateTexts()
    {
        
        scoreName1.text = PlayerPrefs.GetString("scoreName1", "---");
        scoreName2.text = PlayerPrefs.GetString("scoreName2", "---");
        scoreName3.text = PlayerPrefs.GetString("scoreName3", "---");
        scoreName4.text = PlayerPrefs.GetString("scoreName4", "---");
        scoreName5.text = PlayerPrefs.GetString("scoreName5", "---");

        scoreNum1.text = PlayerPrefs.GetInt("scoreNum1",0).ToString();
        scoreNum2.text = PlayerPrefs.GetInt("scoreNum2",0).ToString();
        scoreNum3.text = PlayerPrefs.GetInt("scoreNum3",0).ToString();
        scoreNum4.text = PlayerPrefs.GetInt("scoreNum4",0).ToString();
        scoreNum5.text = PlayerPrefs.GetInt("scoreNum5",0).ToString();
    }

    private void refillLifes()
    {
        life3.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        life2.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        life1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void decreaseLife(int playerLife)
    {
        if(playerLife == 3)
        {
            life3.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (playerLife == 2)
        {
            life2.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (playerLife == 1)
        {
            life1.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void increaseScore(int score)
    {
        totalScore += score;
        scoreText.text = "Score: " + totalScore;
    }

    public void newGame()
    {
        name = nameText.text;
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        refillLifes();
        GameManager.instance.newGame();
        nameInput.text = "";
        nameText.text = "";
        

    }

    public void exitGame()
    {
       
            Application.Quit();
    }

    public void dead()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverScore.text = "Score: " + totalScore;
        StartCoroutine(gameOver());
        highScoreAranger();
        totalScore = 0;
        
    }

    private void highScoreAranger()
    {
        if(totalScore >= PlayerPrefs.GetInt("scoreNum1", 0))
        {
            PlayerPrefs.SetInt("scoreNum5", PlayerPrefs.GetInt("scoreNum4"));
            PlayerPrefs.SetInt("scoreNum4", PlayerPrefs.GetInt("scoreNum3"));
            PlayerPrefs.SetInt("scoreNum3", PlayerPrefs.GetInt("scoreNum2"));
            PlayerPrefs.SetInt("scoreNum2", PlayerPrefs.GetInt("scoreNum1"));
            PlayerPrefs.SetInt("scoreNum1", totalScore);

            PlayerPrefs.SetString("scoreName5", PlayerPrefs.GetString("scoreName4"));
            PlayerPrefs.SetString("scoreName4", PlayerPrefs.GetString("scoreName3"));
            PlayerPrefs.SetString("scoreName3", PlayerPrefs.GetString("scoreName2"));
            PlayerPrefs.SetString("scoreName2", PlayerPrefs.GetString("scoreName1"));
            PlayerPrefs.SetString("scoreName1",   name);
        }
        else if(totalScore >= PlayerPrefs.GetInt("scoreNum2", 0))
        {
            PlayerPrefs.SetInt("scoreNum5", PlayerPrefs.GetInt("scoreNum4"));
            PlayerPrefs.SetInt("scoreNum4", PlayerPrefs.GetInt("scoreNum3"));
            PlayerPrefs.SetInt("scoreNum3", PlayerPrefs.GetInt("scoreNum2"));
            PlayerPrefs.SetInt("scoreNum2", totalScore);

            PlayerPrefs.SetString("scoreName5", PlayerPrefs.GetString("scoreName4"));
            PlayerPrefs.SetString("scoreName4", PlayerPrefs.GetString("scoreName3"));
            PlayerPrefs.SetString("scoreName3", PlayerPrefs.GetString("scoreName2")); 
            PlayerPrefs.SetString("scoreName2",  name);
        }
        else if (totalScore >= PlayerPrefs.GetInt("scoreNum3", 0))
        {
            PlayerPrefs.SetInt("scoreNum5", PlayerPrefs.GetInt("scoreNum4"));
            PlayerPrefs.SetInt("scoreNum4", PlayerPrefs.GetInt("scoreNum3"));
            PlayerPrefs.SetInt("scoreNum3", totalScore);

            PlayerPrefs.SetString("scoreName5", PlayerPrefs.GetString("scoreName4"));
            PlayerPrefs.SetString("scoreName4", PlayerPrefs.GetString("scoreName3"));
            PlayerPrefs.SetString("scoreName3",  name);
        }
        else if (totalScore >= PlayerPrefs.GetInt("scoreNum4", 0))
        {
            PlayerPrefs.SetInt("scoreNum5", PlayerPrefs.GetInt("scoreNum4"));
            PlayerPrefs.SetInt("scoreNu4", totalScore);

            PlayerPrefs.SetString("scoreName5", PlayerPrefs.GetString("scoreName4"));
            PlayerPrefs.SetString("scoreName4",  name);
        }
        else if (totalScore >= PlayerPrefs.GetInt("scoreNum5", 0))
        {
            PlayerPrefs.SetInt("scoreNum5", totalScore);

            PlayerPrefs.SetString("scoreName5",  name);
        }

    }

    private IEnumerator gameOver()
    {
        while(true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
       
    }

    public void nextGame()
    {
        gamePanel.SetActive(false);
        nextGamePanel.SetActive(true);
        totalScore += 1000;
        nextGameScore.text = "Score: " + totalScore;
        StartCoroutine(congrats());
        scoreText.text = "Score: " + totalScore;

    }

    public void nextGameButton()
    {
        
        nextGamePanel.SetActive(false);
        gamePanel.SetActive(true);
        GameManager.instance.nextGame();
    }

    private IEnumerator congrats()
    {
        while (true)
        {
            nextGamCong.text = "CONGRATULATİONS";
            yield return new WaitForSeconds(0.5f);
            nextGamCong.text = "";
            yield return new WaitForSeconds(0.5f);
        }

    }


    public void gameOverMainMenu()
    {
        StopCoroutine(gameOver());
        gameOverPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void menuHighScores()
    {
        menuPanel.SetActive(false);
        highScorePanel.SetActive(true);
        updateTexts();
    }

    public void highScoresMenu()
    {
        highScorePanel.SetActive(false);
        menuPanel.SetActive(true);
        
    }

}
