using UnityEngine;

[CreateAssetMenu(fileName = "AttackSystem", menuName = "SO/Attack/System")]
public class AttackSystemSO : ScriptableObject
{
    [SerializeField] private WaveSystemSO waveSys;
    [SerializeField] private AttackDataSO[] attackList;
    public event System.Action<AttackDataSO> OnAfterAttackStart;

    private WaveAttackBase currentAttack;
    

    private void OnEnable() {
        waveSys.OnBeforeAttackStart += HandleAttackStart;
        waveSys.OnAttackFinish += HandleAttackFinish;
    }

    private void OnDisable() {
        waveSys.OnBeforeAttackStart -= HandleAttackStart;
        waveSys.OnAttackFinish -= HandleAttackFinish;
    }

    public AttackDataSO GetRandomAttack() {
        int index = Random.Range(0, attackList.Length);
        return attackList[index];
    }

    private void HandleAttackStart(Vector2 range)
    {
        AttackDataSO attackData = GetRandomAttack();

        currentAttack = Instantiate(attackData.attackPrefab);
        currentAttack.AttackStart(range);

        OnAfterAttackStart?.Invoke(attackData);
    }

    private void HandleAttackFinish()
    {
        Destroy(currentAttack.gameObject); // 공격 그만
    }
}
