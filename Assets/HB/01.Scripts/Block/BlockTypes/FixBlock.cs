using UnityEngine;

public class FixBlock : Block
{
    public override void HandleLandEvent(bool hasAbility)
    {
        base.HandleLandEvent(hasAbility);
        Debug.Log("FixBlock");
    }

    public override void HandleHitEvent()
    {
    }

    public override void HandleDeadEvent()
    {
    }
}
