using UnityEngine;

public class GroundDetectBlock : MonoBehaviour
{
    private GroundBox groundBox;
    public bool IsGrounded => groundBox != null;

    public void GroundToutch(GroundBox box) {
        groundBox = box;
        box.AddBlock(this);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (IsGrounded && other.gameObject.TryGetComponent(out GroundDetectBlock block) && !block.IsGrounded) {
            block.GroundToutch(groundBox);
        }
    }
}
