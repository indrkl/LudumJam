using UnityEngine;
using System.Collections;

public abstract class EntityBase : MonoBehaviour {
    public float speed;
    public float jumpHeight;

    public Rigidbody2D body;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        //get player maximum speed and jump height
    }
}
