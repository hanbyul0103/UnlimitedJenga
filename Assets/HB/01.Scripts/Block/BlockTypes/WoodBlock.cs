using System;
using UnityEngine;

public class WoodBlock : Block
{
    public override void HandleLandEvent(bool hasAbility)
    {
        base.HandleLandEvent(hasAbility);
        Debug.Log("WoodBlock");
    }

    public override void HandleHitEvent()
    {
        throw new NotImplementedException();
    }

    public override void HandleDeadEvent()
    {
        throw new NotImplementedException();
    }
}
