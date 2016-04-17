using UnityEngine;
using System.Collections;
using System;

public class ChargeBehaviour : AIBehaviour
{
    public bool right;
    public float triggerDistance;
    public override bool Condition()
    {
        ControllerPlayer pl = ControllerPlayer.instance;
        if (Vector3.Distance(pl.transform.position, ai.transform.position) < triggerDistance)
            return true;
        else return false;
    }
    public float lastChange = -5;
    public override void OnUpdate()
    {
        Vector3 lookingAt = Vector2.left;
        if (right)
            lookingAt = Vector2.right;


        ControllerPlayer pl = ControllerPlayer.instance;
        Vector3 dir = ai.transform.position - pl.transform.position;
        if (dir.x < 0 && !right)
        {
            right = true;
            lastChange = Time.time;
        }
        else if (dir.x > 0 && right)
        {
            lastChange = Time.time;
            right = false;
        }
        if (Time.time - lastChange < 2)
        {
            ai.movement = 0;
        }
        else
        {
            ai.movement = ai.speed;
            if (!right)
                ai.movement *= -1;
            if (Physics2D.Linecast(transform.position + new Vector3(0, 0.3f, 0), transform.position + new Vector3(0, 0.3f, 0) + lookingAt * 2f, 1 << LayerMask.NameToLayer("Ground")))
            {
                ai.jump = 1;
            }
            else
            {
                ai.jump = 0;
            }
        }
    }
}
