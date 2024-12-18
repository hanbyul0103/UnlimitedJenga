using UnityEngine;

[CreateAssetMenu(fileName = "BlockStatSO", menuName = "SO/Block/BlockStatSO")]
public class BlockStatSO : ScriptableObject
{
    [Tooltip("����")] public float mass;
    [Tooltip("ü��")] public float health;
    [Tooltip("����")] public float cost;
    [Tooltip("�ɷ�")] public bool hasAbility;
}
