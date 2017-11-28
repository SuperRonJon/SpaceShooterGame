using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    PlayerController player;

    // Use this for initialization
    void Awake()
    {
        GameObject go = GameObject.FindWithTag("Player");
        if (go != null)
            player = go.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.AddBomb();
            Destroy(gameObject);
        }
    }
}
