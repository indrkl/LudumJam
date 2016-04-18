using UnityEngine;
using System.Collections;

public class DeadBody : MonoBehaviour {
	public bool isCollectable;
	public int formID;

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.name == "Player")
			isCollectable = true;

        if (obj.GetComponent<ControllerPlayer>())
        {
            PlayerForm form = obj.GetComponent<ControllerPlayer>().form;            
            if (form.AvailableForms.Count == 3)
            {
                form.AvailableForms[0] = formID;
            } else
            {
                form.AvailableForms.Add(formID);
            }

            Destroy(gameObject);
        }

	}

	void OnTriggerExit2D(Collider2D obj){
		if (obj.name == "Player")
			isCollectable = false;

	}
}
