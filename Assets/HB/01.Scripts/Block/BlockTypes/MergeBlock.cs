using UnityEngine;

public class MergeBlock : Block
{
    public override void HandleLandEvent(bool hasAbility)
    {
        base.HandleLandEvent(hasAbility);
        Debug.Log("MergeBlock");
    }

    public override void HandleHitEvent()
    {
    }

    public override void HandleDeadEvent()
    {
    }
}
