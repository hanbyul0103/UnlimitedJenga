using UnityEngine;


[CreateAssetMenu(fileName = "AttackSystem", menuName = "SO/Attack/Data")]
public class AttackDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    [field: SerializeField] public WaveAttackBase attackPrefab { get; private set; }
}
