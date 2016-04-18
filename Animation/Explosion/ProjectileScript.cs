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
            } else
            {
                Instantiate(psys, transform.position, transform.rotation);
                Destroy(gameObject);
            }
                
         
                
            
            

        }
    }
}
