using UnityEngine;

public class GroundDetectBlock : MonoBehaviour, IBlockOutHandler
{
    [SerializeField] LayerMask blockLayer;
    private GroundBox groundBox;
    public bool IsGrounded => groundBox != null;
    [SerializeField] private float allowDistance = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Collider2D _collider;
    
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    public void GroundToutch(GroundBox box) {
        groundBox = box;
        box.AddBlock(this);
    }
    
    public void GroundExit() {
        groundBox.RemoveBlock(this);
        groundBox = null;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (IsGrounded && other.gameObject.TryGetComponent(out GroundDetectBlock block) && !block.IsGrounded) {
            block.GroundToutch(groundBox);
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
