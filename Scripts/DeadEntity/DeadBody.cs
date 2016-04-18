using UnityEngine;
using System.Collections;

public class DeadBody : MonoBehaviour {
	public bool isCollectable;
	public int formID;
    public static int curIndex = 1;

	void OnTriggerStay2D(Collider2D obj) {
		if (obj.name == "Player")
			isCollectable = true;

        if (obj.GetComponent<ControllerPlayer>())
        {

            if (Input.GetKeyDown(KeyCode.W))
            {
                PlayerForm form = obj.GetComponent<ControllerPlayer>().form;
                if (!form.AvailableForms.Contains(formID))
                {

                    if (form.AvailableForms.Count < 3)
                    {
                        form.AvailableForms.Add(formID);
                        curIndex++;
                    }
                    else
                    {
                        form.AvailableForms[curIndex] = formID;
                        curIndex++;
                    }
                    if (curIndex == 3)
                        curIndex = 0;
                }
                Destroy(gameObject);
            }
            
        }

	}
    void OnTriggerEnter2D(Collider2D obj)
    {

        if (obj.gameObject.name == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
        }

    }

    void OnTriggerExit2D(Collider2D obj){

        if(obj.gameObject.name == "Player")
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }

	}
}
