using UnityEngine;
using System.Collections;

public class BasicWalk : AIBehaviour {
    public bool right = true;

    public float lastChange;

    public override void OnUpdate() {
        Physics2D.Linecast(transform.position + new Vector3(0, 0.1f, 0), transform.position, 1 << LayerMask.NameToLayer("Ground"));
        Vector3 lookingAt = Vector2.left;
        if (right)
            lookingAt = Vector2.right;

        if (Time.time - lastChange < 2)
        {
            ai.movement = 0;
        }
        else
        {
            if (Physics2D.Linecast(transform.position + new Vector3(0, 0.3f, 0), transform.position + new Vector3(0, 0.3f, 0) + lookingAt * 1.5f, 1 << LayerMask.NameToLayer("Ground")) || !Physics2D.Linecast(transform.position + new Vector3(0, 1.5f, 0) + lookingAt * 3, transform.position - new Vector3(0, 0.3f, 0) + lookingAt * 1.5f, 1 << LayerMask.NameToLayer("Ground")))
            {
                right = !right;
                lastChange = Time.time;
            }
            if (right)
            {
                ai.movement = ai.speed / 2.0f;
            }
            else
            {
                ai.movement = -ai.speed / 2.0f;
            }
        }
	}


    public override bool Condition ()
    {
        return true;
    }
}
