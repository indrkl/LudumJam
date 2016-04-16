using UnityEngine;
using System.Collections;

public class ControllerPlayer : EntityBase {

    //state that the player spawns in
    public int startingForm;
    public PlayerForm form;
    public Animator anim;

    string direction;

    void Start()
    {
        form = gameObject.GetComponent<PlayerForm>();

        //get player maximum speed and jump height
    }

    void Update () {
        float movement = Input.GetAxis("Horizontal") * speed;
        body.velocity = new Vector2(movement, body.velocity.y + 0.1f);

        //controll movment animation
        if (Mathf.Abs(movement) > 0.01) {
            anim.SetFloat("Speed", 1);
            //anim.Play("catRun", 0);
        } else {
            anim.SetFloat("Speed", 0);
        }

        float jump = Input.GetAxisRaw("Jump") * jumpHeight;

        if (Mathf.Abs(jump) > 0.1)
        {
            RaycastHit2D testJump = Physics2D.Linecast(transform.position + new Vector3(0, 0.1f, 0), transform.position, 1 << LayerMask.NameToLayer("Ground"));
            Debug.Log("jump y " + testJump.point.y);
            Debug.Log("my Y " + transform.position.y);
            if (testJump)
            {
                body.velocity = new Vector2(body.velocity.x, jump);
            }
        }
        if (movement > 0)
        {
            direction = "RIGHT";
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } else if (movement < 0)
        {
            direction = "LEFT";
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

	}
}
