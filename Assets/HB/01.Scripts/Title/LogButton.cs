using UnityEngine;

public class LogButton : TitleButton
{
    protected override void HandleOnTimerEndEvent(float duration)
    {
        Debug.Log("asdf");
    }
}
