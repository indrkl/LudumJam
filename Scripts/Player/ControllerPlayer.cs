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

    void LateUpdate () {        
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

        WallsSlide();

    }

    void WallsSlide()
    {
        //slide down on walls
        Vector3 horizontalMove = new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed;
        horizontalMove.y = 0;

        float distance = horizontalMove.magnitude * Time.deltaTime;

        float width = box.bounds.size[0];
        float height = box.bounds.size[1];

        Vector3 point1 = transform.position + new Vector3(0, 0.1f, 0);
        Vector3 point2 = transform.position + new Vector3(0, 0.1f, 0) + horizontalMove.normalized * (distance + 0.5f * width);

        Vector3 point3 = transform.position + new Vector3(0, height, 0);
        Vector3 point4 = transform.position + new Vector3(0, height, 0) + horizontalMove.normalized * (distance + 0.5f * width);

        //print(Physics2D.Linecast(point1, point2, 1 << LayerMask.NameToLayer("Ground")));
        if (Physics2D.Linecast(point1, point2, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(point3, point4, 1 << LayerMask.NameToLayer("Ground")))
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(movement, body.velocity.y + 0.1f);
        }
    }
}
