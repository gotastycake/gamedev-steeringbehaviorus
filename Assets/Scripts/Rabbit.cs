using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    Rigidbody2D rb;
    Animal animal;

    void Start()
    {
        animal = GetComponent<Animal>();
        rb = GetComponent<Rigidbody2D>();
        animal.currentBehaviour = Animal.Behavior.wander;
    }

    void Update()
    {
        if (Time.frameCount % animal.updateDelay != 0) { return; }

        float minDistToNearLiving = float.MaxValue;
        GameObject nearLiving = null;
        Animal[] animals = GameObject.FindObjectsOfType<Animal>();
        if (animals.Length > 0) {
            GameObject[] livings = new GameObject[animals.Length];
            for (int i = 0; i < animals.Length; i ++) {
                livings[i] = animals[i].gameObject;
            }
            nearLiving = livings[0];
            for (int i = 0; i < livings.Length; i ++) {
                GameObject living = livings[i];
                if (living == gameObject) { continue; }
                float dist = Vector3.Distance(transform.position, living.transform.position);
            
                if (dist < minDistToNearLiving) {
                    nearLiving = living;
                    minDistToNearLiving = dist;
                }
            }
        }
        
        if (minDistToNearLiving < animal.minDistToRun && nearLiving != gameObject) {
            animal.currentBehaviour = Animal.Behavior.run;
        } else {
            animal.currentBehaviour = Animal.Behavior.wander;
        }

        Vector3 d = new Vector3();
        switch (animal.currentBehaviour) {
            case Animal.Behavior.wander:
                float x = Random.Range(-animal.wanderForce, animal.wanderForce), y = Random.Range(-animal.wanderForce, animal.wanderForce);
                d = new Vector3(x, y, 0);
                break;
            case Animal.Behavior.run:
                d = (transform.position - nearLiving.transform.position) * animal.maxSpeed;
                break;
        }
        
        if(rb.velocity.magnitude > animal.maxSpeed) {
            rb.velocity = rb.velocity.normalized * animal.maxSpeed;
        }
        d = Game.adjustVector(transform, animal.maxSpeed, d);
        rb.AddForce(d);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Wolf" || other.gameObject.tag == "Border" || other.gameObject.tag == "Bullet") {
            Debug.Log("Rabbit has died from " + other.gameObject.tag);
            Destroy(gameObject);
        }
    }
}
