using UnityEngine;
using System.Collections;

public class PlayerForm : MonoBehaviour {
    public int currentForm = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SwitchForm (int newForm) {
        currentForm = newForm;


    }
}
