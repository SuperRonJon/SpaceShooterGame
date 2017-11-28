using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public float scrollSpeed = 2f;
    public float fireRate = .75f;
    public int damageDelt = 50;
    float nextFire = 0;

    public GameObject laserPrefab;
    public Transform firePoint;

    BoxCollider2D collider;
    Rigidbody2D rb;
   
    int lives = 3;
    int upgrades = 0;
    int bombs = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update () {
        Vector2 position = rb.transform.position;
        position.x += scrollSpeed * Time.deltaTime;
        //position.x += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        position.y += Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        position.y = Mathf.Clamp(position.y, -4.5f, 4.5f);

        rb.MovePosition(position);

        if (Input.GetButton("Fire1") && nextFire >= fireRate)
        {
            nextFire = 0;
            Instantiate(laserPrefab, firePoint.position, laserPrefab.transform.rotation);
        }

        nextFire += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) /*&& bombs > 0*/)
        {
            UseBomb();
        }
	}

    public int GetLives()
    {
        return lives;
    }

    public void LoseLife()
    {
        StartCoroutine(DisableCollider());
        lives--;
        GameController.instance.UpdateLivesText();
        if (lives <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("player is dead");
    }

    IEnumerator DisableCollider()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(.5f);
        collider.enabled = true;
    }

    public void UpgradeWeapon()
    {
        upgrades++;
        if(upgrades == 1)
        {
            fireRate = 0.2f;
            damageDelt = 50;
        }
        else if(upgrades > 1)
        {
            StartCoroutine(IncreaseFirerate(0.1f));
        }
    }

    public void AddBomb()
    {
        bombs++;
    }

    IEnumerator IncreaseFirerate(float rate)
    {
        float currRate = fireRate;
        fireRate = rate;

        yield return new WaitForSeconds(7f);
        fireRate = currRate;
    }

    void UseBomb()
    {
        bombs--;
        GameController.instance.KillAllEnemies();
    }
}
