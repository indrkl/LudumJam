using UnityEngine;
using System.Collections;

public class ControllerPlayer : MonoBehaviour {
    Rigidbody2D body;
    EntityBase manager;
    PlayerForm form;
    float speed;
    float jumpHeight;

    //state that the player spawns in
    public int startingForm;

	void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        manager = gameObject.GetComponent<EntityBase>();
        form = gameObject.GetComponent<PlayerForm>();

        //get player maximum speed and jump height
        speed = manager.speed;
        jumpHeight = manager.jumpHeight;


	}
	
	void Update () {
        float movement = Input.GetAxis("Horizontal") * speed;
        movement *= Time.deltaTime;
        body.velocity = new Vector2(movement, body.velocity.y);

        float jump = Input.GetAxisRaw("Jump") * jumpHeight;

        if (Mathf.Abs(jump) > 0.1) {
            body.velocity = new Vector2(body.velocity.x, jump);
        }

	}
}
