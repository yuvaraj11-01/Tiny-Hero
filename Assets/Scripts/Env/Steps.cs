using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var player = collision.GetComponent<TopDownPlayerController>();
            player.MovementSpeed /= 1.5f; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var player = collision.GetComponent<TopDownPlayerController>();
            player.MovementSpeed *= 1.5f;
        }

    }

}
