using UnityEngine;

public class BushBlock : Block
{
    public override void HandleLandEvent(bool hasAbility)
    {
        base.HandleLandEvent(hasAbility);
        Debug.Log("BushBlock");
    }

    public override void HandleHitEvent()
    {
    }

    public override void HandleDeadEvent()
    {
    }
}
