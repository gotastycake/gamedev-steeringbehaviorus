using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeGroup : MonoBehaviour
{
    // Start is called before the first frame update
    public float wanderForce = 1f;
    public int does = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 60 != 0) { return; }

        float x = Random.Range(-wanderForce, wanderForce), y = Random.Range(-wanderForce, wanderForce);
        Vector3 d = new Vector3(x, y, 0);

        transform.position += d;
        float normX = Mathf.Min(Mathf.Max(-12.5f, transform.position.x), 12.5f);
        float normY = Mathf.Min(Mathf.Max(-7.5f, transform.position.y), 7.5f);
        transform.position = new Vector3(normX, normY, 0);
    }

    public void IncDoes() {
        does ++;
    }
}
