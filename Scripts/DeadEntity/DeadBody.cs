using UnityEngine;
using System.Collections;

public class DeadBody : MonoBehaviour {
	public bool isCollectable;
	public int formID;

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.name == "Player")
			isCollectable = true;
			//Debug.Log ("I can feel the vibes of the player");
	}

	void OnTriggerExit2D(Collider2D obj){
		if (obj.name == "Player")
			isCollectable = false;
			//Debug.Log ("Player vibes left me"); 
	}
}
