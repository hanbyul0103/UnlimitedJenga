using UnityEngine;

public class TestAttackWave : WaveAttackBase
{
    public override void AttackStart(Vector2 rangeX)
    {
        print($"테스트 재해 시작. 범위: {rangeX}");
    }
}
