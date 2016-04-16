using UnityEngine;
using System.Collections;

public class PlayerForm : MonoBehaviour {
    public int currentForm = 0;

    /* 
    PLAYER FORMS:
     0 - cat
     1 - barrel
    */

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SwitchForm (int newForm) {
        currentForm = newForm;

        if (newForm == 0)
        {

        }
    }
}
