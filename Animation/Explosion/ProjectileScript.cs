using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
    public Rigidbody2D body;
    public CircleCollider2D circle;


    void Update()
    {
        if (circle.IsTouchingLayers(8))
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, body.velocity.normalized);
            gameObject.GetComponent<EntityBase>().takeDamage(45f, 1);
            Destroy(gameObject);

        }
        else if (circle.IsTouchingLayers(9))
        {
            Destroy(gameObject);
        }
    }
}
