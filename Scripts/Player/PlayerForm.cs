﻿using UnityEngine;
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
    public Sprite icon;

    public Vector2 colliderSize;
    public Vector2 colliderOffset;
    public Vector3 localScale;

    public float mass;
    public float cooldown;

    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public ControllerPlayer cp;
    public ParticleSystem psys;
	public SoundManager soundManager;
    public int threat;

	public AudioClip transformSound;

    public List<int> AvailableForms;

	public UnityStandardAssets.ImageEffects.SunShafts sunShafts;
	private float blinkSpeed = 720f;
	private bool transforming;
	private float blinkIntensity;


    public static List<PlayerForm> forms;
    public float[] damageLowerer;
    public float attackDmg;

    // Use this for initialization
    void Start () {
        AvailableForms.Add(1);
        forms = new List<PlayerForm>(Resources.LoadAll<PlayerForm>("Forms"));
        forms.Sort();
    }
	
	// Update is called once per frame
	void Update () {
		if (transforming) {
			blinkIntensity += Time.deltaTime * blinkSpeed * Mathf.Deg2Rad;
			sunShafts.maxRadius = 0.2f - 0.2f*Mathf.Cos (blinkIntensity);

			if (blinkIntensity > 2f * Mathf.PI) {
				transforming = false;
				blinkIntensity = 0f;
				sunShafts.maxRadius = 0;
			}

		}
	}

    public void SwitchForm (int newForm) {
        if (AvailableForms.Contains(newForm))
        {
            if (currentForm != newForm)
            {
                psys.Play();
                soundManager.PlaySingle(transformSound);
                transforming = true;
                blinkIntensity = 0.1f;

            }
            currentForm = newForm;

            cp.speed = forms[currentForm].speed;
            cp.jumpHeight = forms[currentForm].jumpHeight;
            bc.size = forms[currentForm].colliderSize;
            bc.offset = forms[currentForm].colliderOffset;
            transform.localScale = forms[currentForm].localScale;

            rb.mass = forms[currentForm].mass;
            cp.damageLowerer = forms[currentForm].damageLowerer;

            cp.threat = forms[currentForm].threat;
        }
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
