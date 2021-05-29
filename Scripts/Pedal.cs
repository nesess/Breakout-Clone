using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    Rigidbody2D myRigid;
    [SerializeField]
    GameObject ball;
    Rigidbody2D ballRigid;
    bool isStart;
    float xPos;

    [SerializeField]
    private AudioClip bounce;

    private void Awake()
    {
        myRigid = this.gameObject.GetComponent<Rigidbody2D>();
        ballRigid = ball.gameObject.GetComponent<Rigidbody2D>();
        isStart = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        xPos = transform.position.x;
        transform.position = new Vector3(Input.mousePosition.x/Screen.width*16f,transform.position.y,transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 1.35f, 16.4f), transform.position.y, transform.position.z);
        if(isStart)
        {
            ball.transform.position = new Vector3(transform.position.x, ball.transform.position.y, ball.transform.position.z);
        }
        
        
        if(Input.GetKeyDown(KeyCode.Space) && isStart)
        {
            ballRigid.constraints = RigidbodyConstraints2D.None;
            isStart = false;
            if (xPos > transform.position.x)
            {
                ballRigid.velocity = new Vector3(-5f, 10f, 0);
            }
            else if(xPos < transform.position.x)
            {
                ballRigid.velocity = new Vector3(5f, 10f, 0);
            }
            else 
            {
                ballRigid.velocity = new Vector3(0, 10f, 0);
            }

        }
    }

    public void resetBall()
    {
        ballRigid.constraints = RigidbodyConstraints2D.FreezePositionY;
        ballRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        ball.transform.position = new Vector3(transform.position.x,1.5f, ball.transform.position.z);
        isStart = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(bounce, transform.position);
        }
    }
}
