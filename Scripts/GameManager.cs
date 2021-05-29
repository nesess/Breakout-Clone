using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject box;
    public int totalBox = 0;
    [SerializeField]
    private GameObject pedal;
    [SerializeField]
    private GameObject ball;
    public static GameManager instance;

    public bool isPlayerDead;
    private bool newLevel = false;

    private int playerLife = 3;
    private Pedal pedalScript;
    

    private void Awake()
    {
        if (GameManager.instance)
        {
            Destroy(base.gameObject);
        }
        else
        {
            GameManager.instance = this;
        }
        pedalScript = pedal.gameObject.GetComponent<Pedal>();
    }

   

    private void newBoxes()
    {
        for (float i = 1.4f; i < 17.4f; i = i + 1.37f)
        {
            for (float k = 8.4f; k > 4f; k = k - 0.6f)
            {
                Vector3 boxPos = new Vector3(i, k, 0);
                int chance;
                chance = Random.Range(0, 10);
                if (chance > 3)
                {
                    GameObject newBox = Instantiate(box, boxPos, Quaternion.identity);
                    totalBox++;
                }

            }
        }

        //GameObject newBox = Instantiate( box,Vector3(1.4, 8.4, 0),, Quaternion.identity);
        //totalBox++;

    }

    public void nextGame()
    {
        isPlayerDead = false;
        newBoxes();
        newLevel = true;
        pedal.SetActive(true);
        ball.SetActive(true);
        pedalScript.resetBall();
        StartCoroutine(nextLevelChecker());
    }

    public void newGame()
    {
        isPlayerDead = false;
        newBoxes();
        newLevel = true;
        playerLife = 3;
        pedal.SetActive(true);
        ball.SetActive(true);
        pedalScript.resetBall();
        StartCoroutine(nextLevelChecker());
        
    }
    
    private IEnumerator nextLevelChecker()
    {
        
        while (newLevel)
        {
            
            yield return new WaitForSeconds(1f);
            if(totalBox == 0)
            {
                newLevel = false;
                isPlayerDead = true;
                pedal.SetActive(false);
                ball.SetActive(false);
                UIManager.instance.nextGame();
                StopCoroutine(nextLevelChecker());
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(playerLife > 1)
            {
                UIManager.instance.decreaseLife(playerLife);
                playerLife--;
                pedalScript.resetBall();
            }
            else
            {
                UIManager.instance.decreaseLife(playerLife);
                newLevel = false;
                isPlayerDead = true;
                pedal.SetActive(false);
                ball.SetActive(false);
                UIManager.instance.dead();
            }
        }
    }
}
