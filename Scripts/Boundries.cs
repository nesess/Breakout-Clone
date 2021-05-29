using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundries : MonoBehaviour
{
    [SerializeField]
    private AudioClip bounce;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(bounce, transform.position);
        }
    }
}
