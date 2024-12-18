using UnityEngine;

public abstract class AgentMovement : MonoBehaviour, IAgentInitable
{
    protected Rigidbody2D rigid;
    protected Agent agent;
    
    protected virtual void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    public abstract void Move(Vector2 dir);
    public virtual void Init() {}

    void IAgentInitable.Init(Agent agent)
    {
        this.agent = agent;
        Init();
    }
}