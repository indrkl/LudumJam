using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
    public Rigidbody2D body;
    public CircleCollider2D circle;
    public ParticleSystem psys;
    public float damage;

    void LateUpdate()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<EntityBase>())
        {
            EntityBase entity = collision.collider.GetComponent<EntityBase>();
            entity.takeDamage(damage);
        }

        Instantiate(psys, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
