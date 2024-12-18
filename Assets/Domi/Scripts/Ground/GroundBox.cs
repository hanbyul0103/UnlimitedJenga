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
        print($"Remove Blocks count: {blocks.Count}");
    }
    
    public float GetMaxHeight() {
        float maxHeight = float.MinValue;
        foreach (var block in blocks) {
            float height = block.transform.position.y + block.GetHeight() / 2f;
            if (height > maxHeight) {
                maxHeight = height;
            }
        }

        print($"maxHeight: {maxHeight}");
        return maxHeight;
    }
    
    // 최대 높이 표시
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, GetMaxHeight(), 0), new Vector3(10, 0.1f, 1));
    }
}