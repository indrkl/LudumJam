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
    public Collider2D feet;


    public bool alive = true;

    public float cooldown; //ability cooldown for enemies, players get their cooldown from playerForm
    public float cd_remaining = 0;  //cooldown remaining til you can attack again
    public GameObject projectile;

    Vector2 lastVelocity;
    float DashSpeed;

    //top, right, down, bott
    public void takeDamage(float damage, int dir = 1) {
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

    void Start()
    {
        lastJumpTime = -5;
        body = gameObject.GetComponent<Rigidbody2D>();
        //get player maximum speed and jump height
    }

    public float lastJumpTime;

    public void OnUpdate()
    {
        if (!alive)
            return;
        if (debug)
            Debug.Log("Is updating");

        
        if (direction == "RIGHT")
        {
            movement += DashSpeed;
        } else
        {
            movement -= DashSpeed;
        }
        DashSpeed -= 0.1f;

        if (DashSpeed < 0)
        {
            DashSpeed = 0;
        }

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
        if (debug)
            Debug.Log(feet.IsTouchingLayers());


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
        WallsSlide();
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

    public void MeleeAttack(float damage)
    {
        float AttackRange = 1.5f;

        if (cd_remaining >= 0)
        {
            anim.SetTrigger("Attack");
            cd_remaining = cooldown;

            Vector2 point1 = new Vector2(transform.position.x, transform.position.y + 0.3f);
            Vector2 point2;

            if (direction == "RIGHT")
            {
                point2 = new Vector2(transform.position.x + AttackRange, transform.position.y + 0.3f);
            }
            else
            {
                point2 = new Vector2(transform.position.x - AttackRange, transform.position.y + 0.3f);
            }

            RaycastHit2D hit = Physics2D.Linecast(point1, point2, 1 << 8);
            if (hit)
            {
                print(hit.transform.gameObject);
                hit.transform.gameObject.GetComponent<EntityBase>().takeDamage(damage, 1);
            }

        }
    }

    public void RangedAttack(float damage)
    {
        if (cd_remaining >= 0)
        {
            Debug.Log(projectile.name);


            if (direction == "RIGHT")
            {
                Rigidbody2D Projectile = (Instantiate(projectile, gameObject.transform.position + new Vector3(1.6443f, 1.3027f, 0), Quaternion.Euler(new Vector3(0, 0, 150))) as GameObject).GetComponent<Rigidbody2D>();

                Projectile.velocity = new Vector2(10, 0);
            } else
            {
                Rigidbody2D Projectile = (Instantiate(projectile, gameObject.transform.position + new Vector3(-1.6443f, 1.3027f, 0), Quaternion.Euler(new Vector3(0, 0, 326))) as GameObject).GetComponent<Rigidbody2D>();

                Projectile.velocity = new Vector2(-10, 0);
                //Projectile.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    public void DashAttack(float speedModifier)
    {
        if (cd_remaining >= 0)
            DashSpeed = speed * speedModifier;
    }


    protected void WallsSlide()
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
