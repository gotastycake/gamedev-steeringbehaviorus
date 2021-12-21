using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        GameObject.Find("Rabbit").GetComponent<Scrollbar>().value = Shared.rabbits / 20f;
        GameObject.Find("Doe").GetComponent<Scrollbar>().value = Shared.does / 30f;
        GameObject.Find("DoeGroup").GetComponent<Scrollbar>().value = Shared.doeGroups / 10f;
        GameObject.Find("Wolf").GetComponent<Scrollbar>().value = Shared.wolves / 10f;
    }

    public void OnPlayButton() {
        SceneManager.LoadScene("Main");
    }

    public void OnRabbitValueChanged(Scrollbar scrollbar) {
        Shared.rabbits = (int)(scrollbar.value * 19) + 1;
        scrollbar.gameObject.transform.GetChild(2).GetComponent<Text>().text = Shared.rabbits.ToString();
    }

    public void OnDoeValueChanged(Scrollbar scrollbar) {
        Shared.does = (int)(scrollbar.value * 29) + 1;
        scrollbar.gameObject.transform.GetChild(2).GetComponent<Text>().text = Shared.does.ToString();
    }

    public void OnDoeGroupValueChanged(Scrollbar scrollbar) {
        Shared.doeGroups = (int)(scrollbar.value * 9) + 1;
        scrollbar.gameObject.transform.GetChild(2).GetComponent<Text>().text = Shared.doeGroups.ToString();
    }

    public void OnWolfValueChanged(Scrollbar scrollbar) {
        Shared.wolves = (int)(scrollbar.value * 9) + 1;
        scrollbar.gameObject.transform.GetChild(2).GetComponent<Text>().text = Shared.wolves.ToString();
    }
}
