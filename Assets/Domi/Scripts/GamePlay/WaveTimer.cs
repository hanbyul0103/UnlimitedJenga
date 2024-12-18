using System;
using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] private WaveSystemSO waveSys;
    private GroundBox groundBox;
    private bool timerProcess = false; // 타이머 실행중
    private float timeSec = 0; // 타임 초

    private void Awake() {
        groundBox = FindAnyObjectByType<GroundBox>();
        groundBox.BlockAdded += OnBlockAdded;
        waveSys.OnLineUp += HandleLineUp;
        waveSys.OnBeforeAttackStart += HandleAttackBefore;
    }

    private void OnDestroy() {
        groundBox.BlockAdded -= OnBlockAdded;
        waveSys.OnLineUp -= HandleLineUp;
        waveSys.OnBeforeAttackStart -= HandleAttackBefore;
    }

    private void HandleAttackBefore(Vector2 range)
    {
        timeSec = waveSys.AttackDuration;
        timerProcess = true;
    }

    // 선 넘으면
    private void HandleLineUp()
    {
        if (timerProcess && !waveSys.IsAttack) {
            timerProcess = false;
        }
    }


    private void OnBlockAdded(GroundDetectBlock block)
    {
        print($"OnBlockAdded {waveSys.IsAttack} {timerProcess}");
        if (waveSys.IsAttack || timerProcess) return; // 자연재해 중임
        
        // 타이머 시작 ㄱㄱ
        BlockStackTimerStart();
    }

    // 블럭 선까지 쌓는 타이머 시작
    private void BlockStackTimerStart() {
        print("BlockStackTimerStart");

        timeSec = waveSys.LineUpDuration;
        timerProcess = true;
    }

    private void Update() {
        // print($"waveSys.IsAttack {waveSys.IsAttack}");
        if (!timerProcess || timeSec < 0) return;
        timeSec = Mathf.Max(0, timeSec - Time.deltaTime);
        
        // 시간 끗
        if (timeSec == 0) {
            timerProcess = false; // 자동 종료
            waveSys.OnTimerEnd();
        }
    }
}
