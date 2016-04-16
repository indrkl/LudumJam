using UnityEngine;
using System.Collections;

public class AIBase : EntityBase {

	// Use this for initialization
	void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName("CatIdle"))
        {
            anim.SetBool("IsIdle", true);
        }
        else
        {
            anim.SetBool("IsIdle", false);
        }
    }
}
