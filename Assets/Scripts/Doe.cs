using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doe : MonoBehaviour
{
    public enum Behavior {wander, run};
    public float minGroupPosition = 1;
    public GameObject group;
    Rigidbody2D rb;
    Animal animal;
    void Start()
    {
        animal = GetComponent<Animal>();
        DoeGroup[] groups = GameObject.FindObjectsOfType<DoeGroup>();
        while (!group) {
            int rand = Random.Range(0, groups.Length);
            if (groups[rand].does < 10) {
                groups[rand].IncDoes();
                group = groups[rand].gameObject;
            }
        }

        animal.currentBehaviour = Animal.Behavior.wander;
        rb = GetComponent<Rigidbody2D>();

    }

    
    void Update()
    {
        if (Time.frameCount % animal.updateDelay != 0) { return; }

        float minDistToNearLiving = float.MaxValue;
        Wolf[] wolfs = GameObject.FindObjectsOfType<Wolf>();
        Hunter hunter = GameObject.FindObjectOfType<Hunter>();
        GameObject[] livings = new GameObject[wolfs.Length + 1];
        for (int i = 0; i < wolfs.Length; i ++) {
            livings[i] = wolfs[i].gameObject;
        }

        if (hunter != null) {
            livings[livings.Length - 1] = hunter.gameObject;
        }

        GameObject nearLiving = null;
        if (livings.Length > 0) {
            
            nearLiving = livings[0];
            for (int i = 0; i < livings.Length; i ++) {
                GameObject living = livings[i];
                if (living == gameObject || living == null) { continue; }
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
                if (Vector3.Distance(transform.position, group.transform.position) > minGroupPosition) {
                    d += group.transform.position - transform.position;
                } else {
                    float x = Random.Range(-animal.wanderForce, animal.wanderForce), y = Random.Range(-animal.wanderForce, animal.wanderForce);
                    d = new Vector3(x, y, 0);
                }

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
            Debug.Log("Doe has died from " + other.gameObject.tag);
            Destroy(gameObject);
        }
    }
}
