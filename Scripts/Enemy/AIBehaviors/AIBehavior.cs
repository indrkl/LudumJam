using UnityEngine;
using System.Collections;

public abstract class AIBehaviour : MonoBehaviour {
    public AIBase ai;
    public int priority;
    public abstract void OnUpdate();

    public abstract bool Condition();
}
