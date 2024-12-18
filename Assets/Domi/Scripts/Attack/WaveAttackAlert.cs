using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveAttackAlert : MonoBehaviour
{
    [SerializeField] private AttackSystemSO attackSystem;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI content;
    RectTransform rectTrm;

    private readonly Color redColor = new Color32(255, 200, 200, 255);
    private readonly Color whiteColor = Color.white;

    Sequence sequence = null;
    float screenWidth;

    private void Awake()
    {
        attackSystem.OnAfterAttackStart += HandleAttackStart;
        rectTrm = transform as RectTransform;
    }

    private void Start() {
        screenWidth = (transform.root as RectTransform).rect.width;
    }

    private void OnDestroy() {
        attackSystem.OnAfterAttackStart -= HandleAttackStart;
    }

    private void HandleAttackStart(AttackDataSO data)
    {
        if (sequence != null) sequence.Kill();
        
        // 위치 초기화
        rectTrm.anchoredPosition = new Vector2(-screenWidth, 0);
        canvasGroup.alpha = 1;

        content.text = $" {data.Name} 재해가 시작됩니다.";

        // 애님

        sequence = DOTween.Sequence();
        sequence.Append(rectTrm.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutQuad));

        sequence.AppendInterval(0.3f);
        sequence.Append(image.DOColor(redColor, 0.3f));
        sequence.AppendInterval(0.3f);
        sequence.Append(image.DOColor(whiteColor, 0.3f));
        sequence.AppendInterval(0.3f);
        sequence.Append(image.DOColor(redColor, 0.3f));
        sequence.AppendInterval(0.3f);
        sequence.Append(image.DOColor(whiteColor, 0.3f));
        sequence.AppendInterval(0.3f);
        sequence.Append(image.DOColor(redColor, 0.3f));
        sequence.AppendInterval(0.3f);
        sequence.Append(image.DOColor(whiteColor, 0.3f));

        sequence.Append(rectTrm.DOAnchorPosX(screenWidth, 0.5f).SetEase(Ease.OutQuad));
        sequence.AppendCallback(() => {
            canvasGroup.alpha = 0;
            sequence = null;
        });
    }
}
