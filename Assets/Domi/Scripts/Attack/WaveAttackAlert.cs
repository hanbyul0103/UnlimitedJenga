using System;
using UnityEngine;

public class WaveAttackAlert : MonoBehaviour
{
    [SerializeField] private AttackSystemSO attackSystem;

    private void Awake()
    {
        attackSystem.OnAfterAttackStart += HandleAttackStart;
    }

    private void OnDestroy() {
        attackSystem.OnAfterAttackStart -= HandleAttackStart;
    }

    private void HandleAttackStart(AttackDataSO data)
    {
        print($"!주의! - {data.Name} 재해가 시작됩니다.");
    }
}
