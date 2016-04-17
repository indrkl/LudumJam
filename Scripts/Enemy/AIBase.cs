using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBase : EntityBase {

    public List<AIBehaviour> behaviours;

	// Use this for initialization
	void Start () {

        print(startingForm);
        anim.SetInteger("Form", startingForm);

        foreach (AIBehaviour beh in behaviours)
            beh.ai = this;
    }
	
	// Update is called once per frame
	void Update () {
        AIBehaviour bestBehaviour = null;
        foreach(AIBehaviour beh in behaviours)
        {
            if (beh.Condition())
            {
                if (bestBehaviour == null)
                    bestBehaviour = beh;
                else
                {
                    if (beh.priority > bestBehaviour.priority)
                    {
                        bestBehaviour = beh;
                    }
                }
            }
        }
        if(bestBehaviour != null)
            bestBehaviour.OnUpdate();

        base.OnUpdate();
    }
}
