using UnityEngine;

[CreateAssetMenu(fileName = "BlockStatSO", menuName = "SO/Block/BlockStatSO")]
public class BlockStatSO : ScriptableObject
{
    [Tooltip("질량")] public float mass;
    [Tooltip("체력")] public float health;
    [Tooltip("가격")] public float cost;
    [Tooltip("능력")] public bool hasAbility;
}
