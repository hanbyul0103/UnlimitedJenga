using System;
using UnityEngine;

public class StoneBlock : Block
{
    public override void HandleLandEvent(bool hasAbility)
    {
        Debug.Log("StoneBlock");
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
