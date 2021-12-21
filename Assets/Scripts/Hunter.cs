using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunter : MonoBehaviour
{
    public int bullets = 20;
    public float maxSpeed = 2;

    public Text bulletsText, resultText;
    Rigidbody2D rb;
    public GameObject button;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 d = new Vector3(inputX * maxSpeed, inputY * maxSpeed, 0);

        rb.AddForce(d);
        if(rb.velocity.magnitude > maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButtonDown(0) && bullets > 0) {
            GameObject.Instantiate(button, transform.position, transform.rotation);
            bullets --;
            bulletsText.text = "Bullets: " + bullets;
        }

        Animal[] animals = GameObject.FindObjectsOfType<Animal>();
        if (animals.Length == 1 && animals[0].gameObject == gameObject) {
            OnGameEnded(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Border" || other.gameObject.tag == "Wolf") {
            Debug.Log("Hunter has died from " + other.gameObject.tag);
            OnGameEnded(false);
            Destroy(gameObject);
        }
    }

    void OnGameEnded(bool isWin) {
        if (isWin) {
            resultText.text = "You win!";
        } else {
            resultText.text = "You lose!";
        }
        resultText.transform.parent.gameObject.SetActive(true);
    }

}
