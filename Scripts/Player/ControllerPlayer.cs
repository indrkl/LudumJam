using UnityEngine;
using System.Collections;

public class ControllerPlayer : EntityBase {
    public PlayerForm form;
    public bool won = false;

    public static ControllerPlayer instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        form = gameObject.GetComponent<PlayerForm>();
        cooldown = form.cooldown;
        form.SwitchForm(1);
    }

    void LateUpdate () {        
        //switch form
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            form.SwitchForm(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            form.SwitchForm(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            form.SwitchForm(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            form.SwitchForm(3);
        }

        //attack
        if (Input.GetKeyDown(KeyCode.Q) && cd_remaining == 0)
        {
            anim.SetTrigger("Attack");
            if (form.currentForm == 0)
            {
                MeleeAttack(form.attackDmg);
            }
            else if (form.currentForm == 3)
            {
                RangedAttack(25f);
            }
            else if (form.currentForm == 2)
            {
                DashAttack(1.2f);
            }

        }

        //set animation controller to current form
        anim.SetInteger("Form", form.currentForm);

        //get keyboard input for movement
        movement = Input.GetAxis("Horizontal") * speed;
        jump = Input.GetAxisRaw("Jump");

        //call update function that is shared between all entities
        base.OnUpdate();
    }

}
