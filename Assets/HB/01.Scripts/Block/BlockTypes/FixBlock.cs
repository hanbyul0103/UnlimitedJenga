using UnityEngine;

public class FixBlock : Block
{
    public override void HandleDeadEvent()
    {
        Debug.Log("FixBlock");
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
