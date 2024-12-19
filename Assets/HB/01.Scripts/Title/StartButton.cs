using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartButton : TitleButton
{
    protected override void HandleOnTimerEndEvent(float duration)
    {
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() => SoundManager.Instance.PlaySFX("TitleToInGame"));
        seq.AppendInterval(3);
        seq.AppendCallback(() => SceneManager.LoadScene("GameScene"));

        seq.Play();
    }
}
