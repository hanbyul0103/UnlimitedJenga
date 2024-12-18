using UnityEngine;

public abstract class WaveAttackBase : MonoBehaviour
{
    public abstract void AttackStart(Vector2 rangeX);

    // OnDestroy 되면 끝난거임
}
