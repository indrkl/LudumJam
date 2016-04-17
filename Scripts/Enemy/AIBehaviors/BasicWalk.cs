using UnityEngine;
using System.Collections;

public class BasicWalk : AIBehaviour {
    public bool right = true;

    public override void OnUpdate () {
        Vector3 lookingAt = Vector2.left;
        if (right)
            lookingAt = Vector2.right;

	}


    public override bool Condition ()
    {
        return true;
    }
}
