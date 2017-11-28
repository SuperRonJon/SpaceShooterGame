using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject explosion;
    public GameObject bomb;
    public GameObject weaponUpgrade;
    public float speed;

    int health = 100;
    Transform player;
    PlayerController playerController;
    float rotationSpeed = 180f;
    
	
	// Update is called once per frame
	void Update () {
        if(player == null)
        {
            GameObject go = GameObject.FindWithTag("Player");

            if(go != null)
            {
                player = go.transform;
                playerController = go.GetComponent<PlayerController>();
            }
        }

        if (player == null)
            return;

        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion rot = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotationSpeed * Time.deltaTime);

        transform.position += transform.up * speed * Time.deltaTime;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            playerController.LoseLife();
            Die();
            //RemoveFromList();
        }
        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            TakeDamage(playerController.damageDelt);
        }
            
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
        //RemoveFromList();

    }

    public void Die()
    {
        //Debug.Log("EnemyDies");

        int bomb = Random.Range(1, 15);

        if (bomb ==1)
        {
            Debug.Log("Spawn bomb");
            SpawnBomb();
        }
        else if (bomb == 2)
        {
            Debug.Log("Spawn upgrade");

            SpawnUpgrade();
        }

        Instantiate(explosion, transform.position, Quaternion.identity);
        RemoveFromList();
        Destroy(gameObject);
        GameController.instance.score++;
        GameController.instance.UpdateScoreText();

    }

    void SpawnBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }

    void SpawnUpgrade()
    {
        Instantiate(weaponUpgrade, transform.position, Quaternion.identity);
    }

    public void RemoveFromList()
    {
        GameController.instance.activeEnemies.Remove(this);
    }
}
