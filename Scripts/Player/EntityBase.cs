using UnityEngine;
using System.Collections;

public abstract class EntityBase : MonoBehaviour {
    public float speed;
    public float jumpHeight;
    public bool debug = false;
    public int threat;

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

    public float cooldown; //ability cooldown for enemies, players get their cooldown from playerForm
    public float cd_remaining = 0;  //cooldown remaining til you can attack again

    Vector2 lastVelocity;

    //top, right, down, bott
    public void takeDamage(float damage, int dir) {
        curLife = Mathf.Max(curLife - damage * damageLowerer[dir]);
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

        try
        {
            PlayerForm form = gameObject.GetComponent<PlayerForm>();
            if (form.currentForm == 1) {
            }
            else
            {
                body.velocity = new Vector2(movement, body.velocity.y);
            }
        }
        catch (System.Exception e) {
            body.velocity = new Vector2(movement, body.velocity.y);
        }
        
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
                if (feet.IsTouchingLayers())
                {
                    lastJumpTime = Time.time;
                    anim.SetTrigger("Jump");
                    
                }
                body.velocity = new Vector2(body.velocity.x, jumpHeight);
                //body.AddForce(new Vector2(0, Mathf.Max(0, jump - body.velocity.y) * 30 * body.mass));
            }
        }

        //flip direction
        if (movement > 0)
        {
            direction = "RIGHT";
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movement < 0)
        {
            direction = "LEFT";
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //scale cooldown down
        cd_remaining -= Time.deltaTime;
        if (cd_remaining < 0)
        {
            cd_remaining = 0;
        }

        lastVelocity = body.velocity;

    }

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
            float velocityPower = (other.lastVelocity - this.lastVelocity).magnitude;
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

    public void MeleeAttack()
    {
        float AttackRange = 2f;

        if (cd_remaining >= 0)
        {
            anim.SetTrigger("Attack");
            cd_remaining = cooldown;

            Vector2 point1 = new Vector2(transform.position.x, transform.position.y + 0.3f);
            Vector2 point2;

            if (direction == "RIGHT")
            {
                point2 = new Vector2(transform.position.x + AttackRange, transform.position.y + 0.3f);
            } else
            {
                point2 = new Vector2(transform.position.x - AttackRange, transform.position.y + 0.3f);
            }

            RaycastHit2D hit = Physics2D.Linecast(point1, point2, 1 << 8);
            if (hit)
            {
                print(hit.transform.gameObject);
                hit.transform.gameObject.GetComponent<EntityBase>().takeDamage(25f, 1);
            }
            
        }
       


    }
}
