using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WaveNotifyUI : MonoBehaviour
{
    [SerializeField] private WaveSystemSO waveSystem;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI goalText;
    private RectTransform rectTrm;
    private CanvasGroup group;

    Sequence sequence;
    
    private void Awake() {
        waveSystem.OnChangeWaveCount += HandleChangeWave;

        rectTrm = transform as RectTransform;
        group = GetComponent<CanvasGroup>();
    }

    private void OnDestroy() {
        waveSystem.OnChangeWaveCount -= HandleChangeWave;
    }

    [ContextMenu("Test")]
    private void HandleChangeWave()
    {
        if (sequence != null) {
            sequence.Kill();
        }

        float screenWidth = (transform.root as RectTransform).rect.width;

        waveText.text = $"{waveSystem.GetWave()} 웨이브";
        goalText.text = $"목표 {waveSystem.AttackStartHeight}m";
        
        rectTrm.anchoredPosition = new Vector2(-screenWidth / 2f, 0);
        group.alpha = 0;

        sequence = DOTween.Sequence();
        sequence.Append(rectTrm.DOAnchorPosX(0, 0.5f).SetEase(Ease.InOutSine));
        sequence.Join(group.DOFade(1, 0.5f).SetEase(Ease.InOutSine));

        sequence.AppendInterval(1.5f);

        sequence.Append(rectTrm.DOAnchorPosX(screenWidth / 2f, 0.5f).SetEase(Ease.InOutSine));
        sequence.Join(group.DOFade(0, 0.5f).SetEase(Ease.InOutSine));
        sequence.AppendCallback(() => {
            group.alpha = 0;
            sequence = null;
        });
    }
}
