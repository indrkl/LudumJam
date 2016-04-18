using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D obj)
    {
        Debug.Log("TEST");
        if (obj.gameObject.name == "Player")
        {
            ControllerPlayer.instance.won = true;
        }
    }
}
