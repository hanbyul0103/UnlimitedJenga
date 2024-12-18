using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundDetectBlock : MonoBehaviour, IBlockOutHandler
{
    [SerializeField] LayerMask blockLayer;
    private GroundBox groundBox;
    public bool IsGrounded => groundBox != null;
    [SerializeField] private float allowDistance = 0.1f;

    private Collider2D _collider;
    private List<GroundDetectBlock> bottomBlocks = new();
    private Dictionary<GroundDetectBlock, System.Action<GroundBox>> blockEventHandlers = new();

    public event System.Action<GroundBox> OnChangeGround;
    
    private void Awake() {
        _collider = GetComponent<Collider2D>();
    }

    public void GroundToutch(GroundBox box) {
        groundBox = box;
        box.AddBlock(this);

        OnChangeGround?.Invoke(box);
    }
    
    public void GroundExit() {
        groundBox.RemoveBlock(this);
        groundBox = null;
        OnChangeGround?.Invoke(null);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent(out GroundDetectBlock block)) {
            if (IsGrounded && !block.IsGrounded) {
                block.GroundToutch(groundBox);
            }
            bottomBlocks.Add(block);

            // 이벤트트
            blockEventHandlers[block] = (value) => OtherBlockChangeGround(block, value);
            block.OnChangeGround += blockEventHandlers[block];
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.TryGetComponent(out GroundDetectBlock block)) {
            block.OnChangeGround -= blockEventHandlers[block];
            blockEventHandlers.Remove(block);

            bottomBlocks.Remove(block);
        }
    }

    private void OtherBlockChangeGround(GroundDetectBlock block, GroundBox ground) {

        // 붙어있는 블럭이 더 아래 있고 땅에 닿으면
        if (block.transform.position.y - block.GetHeight() / 2f < transform.position.y - GetHeight() / 2f) {
            if (ground && !IsGrounded)
                GroundToutch(ground);
            else if (IsGrounded) { // 땅에 없음
                // 붙어있는 블럭 다 찾음
                bool detectGround = bottomBlocks.Any(b => b.IsGrounded);
                if (!detectGround) // 그래도 붙어있는 블럭들 중에 땅이 다 안닿여있음
                    GroundExit();
            }
        }
    }

    public void OnMapOut()
    {
        if (IsGrounded)
            GroundExit();
    }
    
    private void Update() {
        if (IsGrounded) {
            hasBottomBlock();
        }
    }
    
    private void hasBottomBlock() {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + Vector3.down * allowDistance /* 조금 아래 */, _collider.bounds.size, 0, blockLayer);
        
        // 바닥에 블럭 감지 됨
        foreach (Collider2D item in colliders)
        {
            if (item.gameObject == gameObject) continue;
            return;
        }

        // 바닥에 아무것도 없음
        GroundExit();
    }
    
    public float GetHeight() {
        return _collider.bounds.size.y;
    }
}
