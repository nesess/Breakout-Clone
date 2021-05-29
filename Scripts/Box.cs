using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private SpriteRenderer mySprite;

    
    private int health;
    private int myScore;
    private Color red = new Color(255, 0, 0);
    private Color yellow = new Color(255, 255, 0);
    private Color green = new Color(0, 255, 0);
    [SerializeField]
    private AudioClip bounce;

    void Start()
    {
        
        mySprite = this.gameObject.GetComponent<SpriteRenderer>();
        
        int myLevel = Random.Range(0,10);
        
        if(myLevel >8)
        {
            health = 3;
            mySprite.color = red;
            myScore = 30;
        }
        else if(myLevel>4)
        {
            health = 2;
            mySprite.color = yellow;
            myScore = 20;
        }
        else
        {
            health = 1;
            mySprite.color = green;
            myScore = 10;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isPlayerDead)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(health == 3)
            {
                AudioSource.PlayClipAtPoint(bounce, transform.position);
                health = 2;
                mySprite.color = yellow;
            }
            else if(health == 2)
            {
                AudioSource.PlayClipAtPoint(bounce, transform.position);
                health = 1;
                mySprite.color = green;
            }
            else
            {
                AudioSource.PlayClipAtPoint(bounce, transform.position);
                UIManager.instance.increaseScore(myScore);
                GameManager.instance.totalBox--;
                Destroy(this.gameObject);
            }
        }
    }
}
