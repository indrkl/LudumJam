using UnityEngine;
using System.Collections;

public class ControllerPlayer : EntityBase {
    public PlayerForm form;
    public float cooldown;
    float cd_remaining;
    //state that the player spawns in



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

        if (cd_remaining > 0)
        {
            cd_remaining -= Time.deltaTime;
        }
        if (cd_remaining < 0)
        {
            cd_remaining = 0;
        }

        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (Mathf.Abs(movement) < 0.05 && currentState.IsName("CatIdle"))
        {
            anim.SetBool("IsIdle", true);
        }
        else
        {
            anim.SetBool("IsIdle", false);
        }

        if (Mathf.Abs(movement) > 0.05 && currentState.IsName("CatRun"))
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        anim.SetInteger("Form", form.currentForm);

        movement = Input.GetAxis("Horizontal") * speed;
        jump = Input.GetAxisRaw("Jump") * jumpHeight;

        base.OnUpdate();
    }
}
