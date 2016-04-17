using UnityEngine;
using System.Collections;

public abstract class EntityBase : MonoBehaviour {
    public float speed;
    public float jumpHeight;
    public bool debug = false;

    public float[] damageLowerer;

    public float maxLife;
    public float curLife;

    public float threatLevel;
    string direction;

    public float movement;
    public float jump;

    public int startingForm;
    public Animator anim;

    public Rigidbody2D body;
    public BoxCollider2D box;

    public bool alive = true;

    //top, right, down, bott
    public void takeDamage(float damage, int dir) {
        curLife -= damage * damageLowerer[dir];
        if (curLife <= 0)
            Die();
    }


    public void Die()
    {
        foreach(Collider2D coll in GetComponents<Collider2D>())
        {
            coll.isTrigger = true;
            alive = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
    public CircleCollider2D feet;

    void Start()
    {
        lastJumpTime = -5;
        body = gameObject.GetComponent<Rigidbody2D>();
        //get player maximum speed and jump height
    }

    public float lastJumpTime;

    public void OnUpdate()
    {
        if (debug)
            Debug.Log("Is updating");
        body.velocity = new Vector2(movement, body.velocity.y);
        //body.AddForce(new Vector2((movement - body.velocity.x), 0));
        //controll movment animation
        if (Mathf.Abs(movement) > 0.01)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        //test if jump is possible
        if (Mathf.Abs(jump) > 0.1)
        {

            if (((feet.IsTouchingLayers() && (Time.time - lastJumpTime) >= 0.3f)) || (Time.time - lastJumpTime < 0.2f))
            {
                if(feet.IsTouchingLayers())
                    lastJumpTime = Time.time;
                body.velocity = new Vector2(body.velocity.x, jump);
                //body.AddForce(new Vector2(0, Mathf.Max(0, jump - body.velocity.y) * 30 * body.mass));
            }
        }

        //flip direction
        if (movement > 0)
        {
            direction = "RIGHT";
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (movement < 0)
        {
            direction = "LEFT";
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        lastVelocity = body.velocity;
    }

    public Vector3 lastVelocity;

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.GetComponent<EntityBase>())
        {

            EntityBase other = c.gameObject.GetComponent<EntityBase>();

            if (!other.alive)
            {
                return;
            }
            //float velocityPower = (other.GetComponent<Rigidbody2D>().velocity - this.GetComponent<Rigidbody2D>().velocity).magnitude * other.GetComponent<Rigidbody2D>().mass;
            float velocityPower = (other.lastVelocity - this.lastVelocity).magnitude * other.GetComponent<Rigidbody2D>().mass;
            Debug.Log(velocityPower + " Power level");

            Vector3 collisionDir = other.transform.position - this.transform.position;
            if (Mathf.Abs(collisionDir.y) > Mathf.Abs(collisionDir.x))
            {
                //Up or downs
                if (collisionDir.y < 0)
                {
                    takeDamage(velocityPower, 2);

                    //down
                }
                else
                {
                    takeDamage(velocityPower, 0);
                    //up
                }

            }
            else
            {
                if (collisionDir.x < 0)
                {
                    if (transform.rotation.eulerAngles.y == 0)
                        takeDamage(velocityPower, 3);
                    else takeDamage(velocityPower, 1);
                    //left
                }
                else
                {
                    if(transform.rotation.eulerAngles.y > 0)
                        takeDamage(velocityPower, 3);
                    else takeDamage(velocityPower, 1);


                    //right
                }
            }
        }
    }
}
