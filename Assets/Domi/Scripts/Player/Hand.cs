using UnityEngine;

public class Hand : Agent
{
    [field: SerializeField] public PlayerControlSO Control { get; private set; }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
