using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
    public Rigidbody2D body;
    public CircleCollider2D circle;
    public ParticleSystem psys;
    public float damage;

    void LateUpdate()
    {
        /*if (circle.IsTouchingLayers())
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position - new Vector3(-0.3f, 0, 0), new Vector2(1, 0));

            print(hit.transform.gameObject);

            if (hit.transform.gameObject.GetComponent<AIBase>())
            {
                hit.transform.gameObject.GetComponent<AIBase>().takeDamage(45f);
                Instantiate(psys, transform.position, transform.rotation);

                Destroy(gameObject);
            }
            else if (hit.transform.gameObject.GetComponent<ControllerPlayer>())
            {
                hit.transform.gameObject.GetComponent<ControllerPlayer>().takeDamage(45f);
                Instantiate(psys, transform.position, transform.rotation);

                Destroy(gameObject);
            }
            else
            {

            }
        }*/
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name + " projectile collided");
        if (collision.collider.GetComponent<EntityBase>())
        {
            EntityBase entity = collision.collider.GetComponent<EntityBase>();
            entity.takeDamage(damage);
        }

        Instantiate(psys, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
