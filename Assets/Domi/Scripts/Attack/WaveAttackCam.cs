using System;
using Unity.Cinemachine;
using UnityEngine;

public class WaveAttackCam : MonoBehaviour
{
    [SerializeField] private WaveSystemSO waveSystem;
    [SerializeField] private float padding = 2f;
    private CinemachineCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;
    
    private void Awake() {
        waveSystem.OnBeforeAttackStart += HandleAttackStart;
        waveSystem.OnAttackFinish += HandleAttackFinish;

        cam = GetComponent<CinemachineCamera>();
        noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnDestroy() {
        waveSystem.OnBeforeAttackStart -= HandleAttackStart;
        waveSystem.OnAttackFinish -= HandleAttackFinish;
    }

    private void HandleAttackStart(Vector2 range)
    {
        // 카메라 중간으로 이동 ㄱㄱ
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, (range.x + range.y) / 2f, pos.z);

        float diff = Mathf.Abs(range.y - range.x);
        print($"{range} cam.Lens.OrthographicSize = {diff} / 2f");
        cam.Lens.OrthographicSize = (diff * .5f) + padding;

        cam.Priority = 1;
    }

    private void HandleAttackFinish()
    {
        cam.Priority = -1;
    }

    public void SetNoise(float amplitude, float frequency)
    {
        noise.AmplitudeGain = amplitude;
        noise.FrequencyGain = frequency;
    }
}
