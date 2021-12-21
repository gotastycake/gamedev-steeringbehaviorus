using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public int secondsToDie = 10, secondsFromFood = 5;
    Rigidbody2D rb;
    Animal animal;
    void Start()
    {
        animal = GetComponent<Animal>();
        animal.currentBehaviour = Animal.Behavior.wander;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (secondsToDie - Time.timeSinceLevelLoad < 0) {
            Debug.Log("Wolf has died from hunger");
            Destroy(gameObject);
        }
        float minDistToNearLiving = float.MaxValue;
        GameObject[] livings = GameObject.FindGameObjectsWithTag("Prey");
        GameObject nearLiving = null;
        if (livings.Length > 0) {
            nearLiving = livings[0];
            for (int i = 0; i < livings.Length; i ++) {
                GameObject living = livings[i];
                float dist = Vector3.Distance(transform.position, living.transform.position);

                if (dist < minDistToNearLiving) {
                    nearLiving = living;
                    minDistToNearLiving = dist;
                }
            }
        }
        

        if (minDistToNearLiving < animal.minDistToRun) {
            animal.currentBehaviour = Animal.Behavior.run;
        } else {
            animal.currentBehaviour = Animal.Behavior.wander;
        }

        Vector3 d = new Vector3();
        float x, y;
        switch (animal.currentBehaviour) {
            case Animal.Behavior.wander:
                x = Random.Range(-animal.wanderForce, animal.wanderForce);
                y = Random.Range(-animal.wanderForce, animal.wanderForce);
                d = new Vector3(x, y, 0);
                break;
            case Animal.Behavior.run:
                d = (nearLiving.transform.position - transform.position) * animal.maxSpeed;
                break;
        }

        if(rb.velocity.magnitude > animal.maxSpeed) {
            rb.velocity = rb.velocity.normalized * animal.maxSpeed;
        }
        d = Game.adjustVector(transform, animal.maxSpeed, d);
        rb.AddForce(d);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Border" || other.gameObject.tag == "Bullet") {
            Debug.Log("Wolf has died from " + other.gameObject.tag);
            Destroy(gameObject);
        } else {
            secondsToDie += secondsFromFood;
        }
    }

}
