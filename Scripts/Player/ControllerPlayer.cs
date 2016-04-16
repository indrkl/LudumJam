using UnityEngine;
using System.Collections;

public class ControllerPlayer : EntityBase {
    public PlayerForm form;

    //state that the player spawns in



    void Start()
    {
        form = gameObject.GetComponent<PlayerForm>();

        //get player maximum speed and jump height
    }

    void Update () {
        movement = Input.GetAxis("Horizontal") * speed;
        jump = Input.GetAxisRaw("Jump") * jumpHeight;

        base.OnUpdate();
    }
}
