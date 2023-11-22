using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController pc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pc.Isavailable = true;
            Destroy(gameObject);
        }        
    }
}
