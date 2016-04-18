using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
    public Rigidbody2D body;
    public CircleCollider2D circle;
    public ParticleSystem psys;

    void Update()
    {
        if (circle.IsTouchingLayers())
        {
            print("kekle");
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position - new Vector3(-0.3f, 0, 0), new Vector2(1, 0));
            try
            {
                hit.transform.gameObject.GetComponent<EntityBase>().takeDamage(45f);
                Instantiate(psys, transform.position, transform.rotation);
                Destroy(gameObject);
            } catch (System.Exception e)
            {
                Destroy(gameObject);
            }
            
            

        }
        else if (circle.IsTouchingLayers(LayerMask.NameToLayer("Ground")))
        {
            psys.Play();
            Destroy(gameObject);
            
        }
    }
}
