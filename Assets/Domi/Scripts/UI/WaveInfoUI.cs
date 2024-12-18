using System;
using TMPro;
using UnityEngine;

public class WaveInfoUI : MonoBehaviour
{
    [SerializeField] private WaveSystemSO waveSystem;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI goalText;

    private void Awake()
    {
        waveSystem.OnChangeWaveCount += HandleChangeWave;
    }

    private void OnDestroy() {
        waveSystem.OnChangeWaveCount -= HandleChangeWave;
    }

    private void HandleChangeWave()
    {
        waveText.text = $"{waveSystem.GetWave()} 웨이브";
        goalText.text = $"목표: {waveSystem.AttackStartHeight}m";
    }
}
