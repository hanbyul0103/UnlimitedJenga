using System;
using UnityEngine;

public class DefaultBlock : Block
{
    public override void HandleLandEvent(bool hasAbility)
    {
        Debug.Log($"HasAbility: {hasAbility}");
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
