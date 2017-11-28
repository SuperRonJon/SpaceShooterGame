using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour {

    public float speed = 10f;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    private void Update()
    {
        if(!spriteRenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        rb.velocity = Vector2.right * speed;
	}
}
