using UnityEngine;
using System.Collections;

public class ControllerPlayer : EntityBase {
    public PlayerForm form;
    public float cooldown;
    float cd_remaining;
    public bool trample;

    void Start()
    {
        form = gameObject.GetComponent<PlayerForm>();
        cd_remaining = 0;

        //get player maximum speed and jump height
    }

    void Update () {        
        //switch form
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            form.SwitchForm(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            form.SwitchForm(0);
        }

        //attack
        if (Input.GetKeyDown(KeyCode.Q) && cd_remaining == 0)
        {
            anim.SetTrigger("Attack");
            cd_remaining = cooldown;
        }

        //attack cooldown control
        if (cd_remaining > 0)
        {
            cd_remaining -= Time.deltaTime;
        }
        if (cd_remaining < 0)
        {
            cd_remaining = 0;
        }

        //set animation controller to current form
        anim.SetInteger("Form", form.currentForm);

        //get keyboard input for movement
        movement = Input.GetAxis("Horizontal") * speed;
        jump = Input.GetAxisRaw("Jump") * jumpHeight;

        //call update function that is shared between all entities
        base.OnUpdate();


    }
}
