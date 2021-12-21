using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxSpeed = 20;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.Translate(new Vector3(0,1,0));
    
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(new Vector3(0, 0.8f, 0));

        if(rb.velocity.magnitude > maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if (Mathf.Abs(transform.position.x) > 1000 || Mathf.Abs(transform.position.y) > 1000) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }
}
