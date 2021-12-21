using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static float minBorderDist = 2f;
    public GameObject rabbit, doe, wolf, doeGroup;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Shared.rabbits; i ++) {
            GameObject.Instantiate(rabbit, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f), 0), Quaternion.identity);
        }
        for (int i = 0; i < Shared.wolves; i ++) {
            GameObject.Instantiate(wolf, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f), 0), Quaternion.identity);
        }
        for (int i = 0; i < Shared.doeGroups; i ++) {
            GameObject.Instantiate(doeGroup, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f), 0), Quaternion.identity);
        }
        for (int i = 0; i < Shared.does; i ++) {
            GameObject.Instantiate(doe, new Vector3(Random.Range(-12f, 12f), Random.Range(-7f, 7f), 0), Quaternion.identity);
        }
    }

    void Update()
    {
        
    }

    public static Vector3 adjustVector (Transform transform, float maxSpeed, Vector3 d) {
        float distanceL = Mathf.Abs(transform.position.x - -12f);
        float distanceR = Mathf.Abs(transform.position.x - 12f);
        float distanceT = Mathf.Abs(transform.position.y - -7f);
        float distanceB = Mathf.Abs(transform.position.y - 7f);

        if (distanceL < minBorderDist) { d.x = maxSpeed*2; }
        if (distanceR < minBorderDist) { d.x = -maxSpeed*2; }
        if (distanceT < minBorderDist) { d.y = maxSpeed*2; }
        if (distanceB < minBorderDist) { d.y = -maxSpeed*2; }
        return d;
    }

    public void OnMainMenuButton() {
        SceneManager.LoadScene("Menu");
    }
}
