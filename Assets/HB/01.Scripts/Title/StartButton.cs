using UnityEngine.SceneManagement;

public class StartButton : TitleButton
{
    protected override void HandleOnTimerEndEvent(float duration)
    {
        SceneManager.LoadScene("GameScene");
    }
}
