using UnityEngine;

public class BushBlock : Block
{
    public override void HandleDeadEvent()
    {
        Debug.Log("BushBlock");
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
