using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSystemSO", menuName = "SO/WaveSystemSO")]
public class WaveSystemSO : ScriptableObject
{
    private int waveCount = 0;
    [field: SerializeField] public bool IsAttack { get; private set; } = false;
    public float AttackStartHeight { get; private set; } = 0;
    private float lastAttackStartHeight = 0; // 이전 높이
    public float  DeadHeight { get; private set; } = 0;
    [field: SerializeField] public float AttackDuration = 30; // 자연재해 지속 시간
    [SerializeField] private float deadLineSize = 0.75f;
    public int LineUpDuration { get; private set; } = 60 * 10; // 라인 까지 쌓아야 하는 시간
    [field:SerializeField] public bool GameStart { get; private set; } = false;
    
    public event Action OnChangeWaveCount;
    public event Action OnLineUp; // 라인보다 더 높이 쌓아짐
    public event Action<Vector2> OnBeforeAttackStart;
    public event Action OnAttackFinish;
    public event Action OnGameOver;

    private void OnEnable() {
        IsAttack = false;
        AttackStartHeight = 0;
        waveCount = 0;
        lastAttackStartHeight = 0;
        DeadHeight = 0;
        GameStart = false;
        
        OnGameOver += HandleGameOver;
    }

    private void OnDisable() {
        OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        GameStart = false;
    }

    

    public void GoGame() {
        GameStart = true;
        SetWave(1);
    }

    public void SetWave(int value) {
        waveCount = value;
        lastAttackStartHeight = AttackStartHeight;
        AttackStartHeight = value * 6;
        DeadHeight = AttackStartHeight * deadLineSize;

        OnChangeWaveCount?.Invoke();
    }
    public int GetWave() => waveCount;

    public void OnTimerEnd() {
        if (!GameStart) return;

        if (IsAttack) { // 공격 끗
            IsAttack = false;
            OnAttackFinish?.Invoke();

            // 다음 웨이브 ㄱㄱ
            SetWave(waveCount + 1);
        } else { // 다음 웨이브 선 까지 채우지 못하고 시간 오버!!!
            OnGameOver?.Invoke();
        }
        Debug.Log("타이머 끝");
    }
    
    public void HandleLineUp() {
        OnLineUp?.Invoke();

        if (IsAttack || !GameStart) return;
        IsAttack = true; // 선 넘었으니 자연재해 시작
        OnBeforeAttackStart?.Invoke(new Vector2(lastAttackStartHeight, AttackStartHeight));

        Debug.Log("자연재해 ㄱㄱ");
    }

    public void HandleDeadLineLow() {
        IsAttack = false;
        OnGameOver?.Invoke();
        Debug.Log("데드라인 아래 있음 (끝)");
    }
}
