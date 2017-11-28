using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour {

    PlayerController player;

	// Use this for initialization
	void Awake () {
        GameObject go = GameObject.FindWithTag("Player");
        if(go != null)
            player = go.GetComponent<PlayerController>();
	}
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.UpgradeWeapon();
            Destroy(gameObject);
        }
    }
}
