using UnityEngine;
using System.Collections;

public class ControllerPlayer : EntityBase {

    //state that the player spawns in
    public int startingForm;
    public PlayerForm form;

    void Start()
    {
        form = gameObject.GetComponent<PlayerForm>();

        //get player maximum speed and jump height
    }

    void Update () {
        float movement = Input.GetAxis("Horizontal") * speed;
        movement *= Time.deltaTime;
        body.velocity = new Vector2(movement, body.velocity.y + 0.1f);

        float jump = Input.GetAxisRaw("Jump") * jumpHeight;

        if (Mathf.Abs(jump) > 0.1) {
            body.velocity = new Vector2(body.velocity.x, jump);
        }

	}
}
