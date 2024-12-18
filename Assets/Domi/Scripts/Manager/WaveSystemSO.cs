using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSystemSO", menuName = "SO/WaveSystemSO")]
public class WaveSystemSO : ScriptableObject
{
    private int waveCount = 0;
    [field: SerializeField] public bool IsAttack { get; private set; } = false;
    public float AttackStartHeight { get; private set; } = 0;
    public float  DeadHeight { get; private set; } = 0;
    [SerializeField] private float attackDuration = 0; // 자연재해 지속 시간
    public int LineUpDuration { get; private set; } = 60 * 10; // 라인 까지 쌓아야 하는 시간
    
    public event Action OnChangeWaveCount;
    public event Action OnLineUp; // 라인보다 더 높이 쌓아짐
    public event Action OnAttackStart;

    private void OnEnable() {
        IsAttack = false;
    }

    public void SetWave(int value) {
        waveCount = value;
        AttackStartHeight = value * 6;
        DeadHeight = AttackStartHeight - 5f;

        OnChangeWaveCount?.Invoke();
    }
    public int GetWave() => waveCount;

    public void OnTimerEnd() {
        Debug.Log("타이머 끝");
    }
    
    public void HandleLineUp() {
        OnLineUp?.Invoke();

        if (IsAttack) return;
        IsAttack = true; // 선 넘었으니 자연재해 시작
        OnAttackStart?.Invoke();

        Debug.Log("자연재해 ㄱㄱ");
    }
}
