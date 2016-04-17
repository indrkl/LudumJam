using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerForm : MonoBehaviour, System.IEquatable<PlayerForm>, System.IComparable<PlayerForm>
{
    public int currentForm = 0;

    /* 
    PLAYER FORMS:
     0 - cat
     1 - barrel
    */
    public int formNumber;
    public float speed;
    public float jumpHeight;

    public Vector2 colliderSize;
    public Vector2 colliderOffset;

    public float mass;

    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public ControllerPlayer cp;
    public ParticleSystem psys;
	public SoundManager soundManager;

	public AudioClip transformSound;

    public static List<PlayerForm> forms;

	// Use this for initialization
	void Start () {
        forms = new List<PlayerForm>(Resources.LoadAll<PlayerForm>("Forms"));
        forms.Sort();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchForm (int newForm) {

        if (currentForm != newForm)
        {
            psys.Play();
			soundManager.PlaySingle (transformSound);
        }
        currentForm = newForm;
        Debug.Log(forms[currentForm].formNumber + " " + newForm);

        cp.speed = forms[currentForm].speed;
        cp.jumpHeight = forms[currentForm].jumpHeight;
        bc.size = forms[currentForm].colliderSize;
        bc.offset = forms[currentForm].colliderOffset;

        rb.mass = forms[currentForm].mass;
    }
    public int CompareTo(PlayerForm comparePart)
    {
        // A null value means that this object is greater.
        if (comparePart == null)
            return 1;

        else {
            if (formNumber < comparePart.formNumber) return -1;
            else if (formNumber == comparePart.formNumber) return 0;
            else return 1;
        }
    }

    public bool Equals(PlayerForm other)
    {
        if (other == null) return false;
        return (this.formNumber == other.formNumber);    
    }
}
