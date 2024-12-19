using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private WaveSystemSO waveSys;

    [SerializeField] private RectTransform boxRect;
    [SerializeField] private CanvasGroup mainGroup;

    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI fallBlockText;
    [SerializeField] private TextMeshProUGUI anyKeyText;
    [SerializeField] Image rankImage;

    private Sequence sequence;
    private Sequence anyKeySequence;

    private void Awake() {
        waveSys.OnGameOver += HandleGameOver;
    }

    private void OnDestroy() {
        waveSys.OnGameOver -= HandleGameOver;
    }

    [ContextMenu("Test")]
    private void HandleGameOver()
    {
        Vector2 screenSize = (transform.root as RectTransform).rect.size;

        Time.timeScale = 0; // 시간 멈춤
        waveText.color = heightText.color = fallBlockText.color = new Color(0, 0, 0, 0);
        anyKeyText.gameObject.SetActive(false);
        mainGroup.alpha = 0;
        boxRect.anchoredPosition = new Vector2(0, -screenSize.y);
        rankImage.transform.localScale = Vector3.zero;

        sequence = DOTween.Sequence();

        sequence.AppendInterval(1f); // 테스트
        
        // 화면 나오는 애님
        sequence.SetUpdate(true);
        sequence.Append(mainGroup.DOFade(1, 0.3f).SetEase(Ease.OutQuad));
        sequence.Join(boxRect.DOAnchorPosY(0, 0.3f).SetEase(Ease.OutQuad));

        sequence.AppendInterval(0.5f);

        int maxWave = waveSys.GetWave();
        int maxHeight = 1000;
        int fallBlock = 500;

        // 최고 웨이브
        sequence.AppendCallback(() => waveText.color = Color.black);
        sequence.Append(NumberAnim(waveSys.GetWave(), 1f, (v) => waveText.text = v.ToString()));
        
        // 높이
        sequence.AppendCallback(() => heightText.color = Color.black);
        sequence.Append(NumberAnim(maxHeight, 1f, (v) => heightText.text = $"{v}m"));
        
        // 떨어진거 블럭
        sequence.AppendCallback(() => fallBlockText.color = Color.black);
        sequence.Append(NumberAnim(fallBlock, 1f, (v) => fallBlockText.text = $"{v}블럭"));

        // 랭크
        sequence.AppendInterval(0.5f);
        sequence.Append(rankImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));

        sequence.AppendInterval(0.5f);
        sequence.AppendCallback(() => {
            anyKeyText.gameObject.SetActive(true);
            sequence = null;
        });
    }

    private Tween NumberAnim(float number, float duration, Action<int> cb) {
        return DOTween.To(() => 0f, x => {
            cb?.Invoke((int)x);
        }, number, duration).SetEase(Ease.Linear);
    }
}
