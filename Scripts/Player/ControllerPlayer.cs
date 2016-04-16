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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            form.SwitchForm(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            form.SwitchForm(0);
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
