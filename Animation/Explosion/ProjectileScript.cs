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
            gameObject.GetComponent<EntityBase>().takeDamage(30f, 1);
            Destroy(gameObject);

        }
        else if (circle.IsTouchingLayers(9))
        {
            Destroy(gameObject);
        }
    }

	void OnCollisionEnter2D(Collision2D col)
    {
        print(col.gameObject);
        if (col.transform.gameObject.layer == 8)
        {
            col.transform.gameObject.GetComponent<EntityBase>().takeDamage(50f, 1);
            Destroy (gameObject);

        } else if (col.transform.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}
