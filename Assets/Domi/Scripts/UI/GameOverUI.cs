using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private WaveSystemSO waveSys;
    [SerializeField] private StatisticsSO statistics;

    [SerializeField] private RectTransform boxRect;
    [SerializeField] private CanvasGroup mainGroup;

    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI fallBlockText;
    [SerializeField] private TextMeshProUGUI anyKeyText;
    [SerializeField] Image rankImage;
    [SerializeField] Sprite[] rankSprites;

    private Sequence sequence;
    private Sequence anyKeySequence;
    private bool _activeKeyInput = false;

    private void Awake()
    {
        waveSys.OnGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        waveSys.OnGameOver -= HandleGameOver;
    }

    [ContextMenu("Test")]
    private void HandleGameOver()
    {
        Vector2 screenSize = (transform.root as RectTransform).rect.size;

        waveText.color = heightText.color = fallBlockText.color = new Color(0, 0, 0, 0);
        anyKeyText.gameObject.SetActive(false);
        mainGroup.alpha = 0;
        boxRect.anchoredPosition = new Vector2(0, -screenSize.y);
        rankImage.transform.localScale = Vector3.zero;

        sequence = DOTween.Sequence();

        sequence.AppendInterval(1f); // 테스트
        sequence.AppendCallback(() => Time.timeScale = 0); // 시간 멈춤

        // 화면 나오는 애님
        sequence.SetUpdate(true);
        sequence.Append(mainGroup.DOFade(1, 0.3f).SetEase(Ease.OutQuad));
        sequence.Join(boxRect.DOAnchorPosY(0, 0.3f).SetEase(Ease.OutQuad));

        sequence.AppendInterval(0.5f);

        int maxWave = waveSys.GetWave();
        float maxHeight = statistics.maxHeight;
        int fallBlock = statistics.fallBlock;

        // 최고 웨이브
        sequence.AppendCallback(() => waveText.color = Color.black);
        sequence.Append(NumberAnim(waveSys.GetWave(), 1f, (v) => waveText.text = v.ToString()));
        sequence.JoinCallback(() => SoundManager.Instance?.PlaySFX("Ding1"));

        // 높이
        sequence.AppendCallback(() => heightText.color = Color.black);
        sequence.Append(NumberAnim(maxHeight, 1f, (v) => heightText.text = $"{v}m"));
        sequence.JoinCallback(() => SoundManager.Instance?.PlaySFX("Ding2"));

        // 떨어진거 블럭
        sequence.AppendCallback(() => fallBlockText.color = Color.black);
        sequence.Append(NumberAnim(fallBlock, 1f, (v) => fallBlockText.text = $"{v}블럭"));
        sequence.JoinCallback(() => SoundManager.Instance?.PlaySFX("Ding3"));
        sequence.AppendCallback(() => SoundManager.Instance?.PlaySFX("Ding4"));

        // 랭크
        // 점수 계산
        int rankScore = 100 * maxWave - 10 * fallBlock;
        rankImage.sprite = GetRankSprite(rankScore);

        sequence.AppendInterval(0.5f);

        sequence.Append(rankImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        sequence.JoinCallback(() => SoundManager.Instance?.PlaySFX("Rank"));

        sequence.AppendInterval(0.5f);
        sequence.AppendCallback(() =>
        {
            anyKeyText.gameObject.SetActive(true);
            sequence = null;
        });
        sequence.AppendCallback(() => _activeKeyInput = true);
    }

    private Tween NumberAnim(float number, float duration, Action<int> cb)
    {
        return DOTween.To(() => 0f, x =>
        {
            cb?.Invoke((int)x);
        }, number, duration).SetEase(Ease.Linear);
    }

    private Sprite GetRankSprite(int score)
    {
        int idx;
        if (score > 990)
        {
            idx = 0;
        }
        else if (score > 850)
        {
            idx = 1;
        }
        else if (score > 700)
        {
            idx = 2;
        }
        else if (score > 450)
        {
            idx = 3;
        }
        else if (score > 200)
        {
            idx = 4;
        }
        else idx = 5;

        return rankSprites[idx];
    }

    private void Update()
    {
        if (_activeKeyInput)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}
