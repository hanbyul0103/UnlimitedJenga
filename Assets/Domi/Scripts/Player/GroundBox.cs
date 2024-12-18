using System.Collections.Generic;
using UnityEngine;

public class GroundBox : MonoBehaviour
{
    private List<GroundDetectBlock> blocks = new();

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent(out GroundDetectBlock block) && !block.IsGrounded) {
            block.GroundToutch(this);
        }
    }

    public void AddBlock(GroundDetectBlock block) {
        blocks.Add(block);
        print($"Add Blocks count: {blocks.Count}");
    }

    public void RemoveBlock(GroundDetectBlock block) {
        blocks.Remove(block);
    }
}
