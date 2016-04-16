using UnityEngine;
using System.Collections;

public abstract class AIBehaviour : MonoBehaviour {
    public abstract void OnUpdate();

    public abstract bool Condition();
}
