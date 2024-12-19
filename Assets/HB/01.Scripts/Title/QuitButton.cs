using UnityEngine;

public class QuitButton : TitleButton
{
    protected override void HandleOnTimerEndEvent(float duration)
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
