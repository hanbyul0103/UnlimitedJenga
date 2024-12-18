using UnityEngine;

public class MergeBlock : Block
{
    public override void HandleDeadEvent()
    {
        Debug.Log("MergeBlock");
    }

    public override void HandleHitEvent()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleLandEvent(bool hasAbility)
    {
        throw new System.NotImplementedException();
    }
}
