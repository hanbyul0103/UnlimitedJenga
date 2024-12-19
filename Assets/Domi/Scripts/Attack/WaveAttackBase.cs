using UnityEngine;

public abstract class WaveAttackBase : MonoBehaviour
{
    public abstract void AttackStart(Vector2 rangeY);
    public static Vector2 GetScreenSideX(float margin) {
        float leftX = Camera.main.ViewportToWorldPoint(new Vector3(Camera.main.rect.xMin, 0, 0)).x - margin;
        float rightX = Camera.main.ViewportToWorldPoint(new Vector3(Camera.main.rect.xMax, 0, 0)).x + margin;
        return new Vector2(leftX, rightX);        
    }

    // OnDestroy 되면 끝난거임
    

    public static Quaternion LookRotation2D(Vector2 direction)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}
