using UnityEngine;
using System.Collections;

public class PlayerForm : MonoBehaviour {
    public int currentForm = 0;

    /* 
    PLAYER FORMS:
     0 - cat
     1 - barrel
    */

    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public ControllerPlayer cp;
    public ParticleSystem psys;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchForm (int newForm) {

        if (currentForm != newForm)
        {
            psys.Play();
        }
        currentForm = newForm;

        if (newForm == 0)
        {
            cp.speed = 5;
            cp.jumpHeight = 15;

            bc.size = new Vector2(0.56f, 0.43f);
            bc.offset = new Vector2(0, 0.23f);

            rb.mass = 1;
        }
        else if (newForm == 1)
        {
            cp.speed = 0;
            cp.jumpHeight = 0;

            bc.size = new Vector2(0.34f, 0.43f);
            bc.offset = new Vector2(0, 0.23f);

            rb.mass = 3;
        }
    }
}
