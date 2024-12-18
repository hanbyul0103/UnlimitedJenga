using UnityEngine;

public class Agent : MonoBehaviour
{
    protected virtual void Awake()
    {
        IAgentInitable[] inits = GetComponentsInChildren<IAgentInitable>(true);
        foreach (IAgentInitable init in inits)
            init.Init(this);
    }
}
