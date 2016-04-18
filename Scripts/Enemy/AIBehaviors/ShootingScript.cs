using UnityEngine;
using System.Collections;
using System;

public class ShootingScript : AIBehaviour {

    public float coolDown = 1.0f;
    public float lastShotTime;
    public float damage;

    public override bool Condition()
    {
        if (!ControllerPlayer.instance)
            return false;   
        if (Time.time < lastShotTime + coolDown)
            return false;
        ControllerPlayer pl = ControllerPlayer.instance;
        Vector3 dir = pl.transform.position - transform.position - new Vector3(0, 1, 0);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(0, 1, 0), dir.normalized, 10);
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject == this.gameObject)
                continue;
            if (hits[i].collider.gameObject.name == "Player")
                return true;
            else return false;
        }

        return false;
    }

    public override void OnUpdate()
    {
        ControllerPlayer pl = ControllerPlayer.instance;
        Vector3 dir = pl.transform.position - transform.position;
        if (dir.x > 0)
            ai.movement = 0.01f;
        else
            ai.movement = -0.01f;
        ai.RangedAttack(damage);
        lastShotTime = Time.time;

    }
}
