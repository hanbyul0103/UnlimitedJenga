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
    }

    private void OnDestroy() {
        groundBox.BlockAdded -= OnBlockAdded;
        waveSys.OnLineUp -= HandleLineUp;
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
        if (!timerProcess || timeSec < 0) return;
        timeSec = Mathf.Max(0, Time.deltaTime);
    }
}
