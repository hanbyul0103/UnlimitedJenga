using System;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public event Action<bool> OnLandEvent; // 착지햇을때 스킬이 있으면 true
    public event Action OnHitEvent;
    public event Action OnDeadEvent;

    [Header("Reference")]
    private Rigidbody2D _rbComponent;

    [Header("Setting")]
    public BlockStatSO _blockStatSO;
    [SerializeField] private Transform _groundCheckerTransform;
    [SerializeField] private Vector2 _groundCheckerSize;

    [Header("Info")]
    [SerializeField] private bool _isInShop = true;
    [SerializeField] private bool _isGrounded = false;
    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _rbComponent = GetComponent<Rigidbody2D>();

        OnLandEvent += HandleLandEvent;
        OnHitEvent += HandleHitEvent;
        OnDeadEvent += HandleDeadEvent;
    }

    private void Start()
    {
        Debug.Log(_blockStatSO.cost);
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    public abstract void HandleLandEvent(bool hasAbility);
    public abstract void HandleHitEvent();
    public abstract void HandleDeadEvent();

    private void ApplyGravity()
    {
        if (!_isInShop)
        {
            _rbComponent.gravityScale = _blockStatSO.mass;
        }
        else
            _rbComponent.gravityScale = 0;
    }

    private void OnDestroy()
    {
        OnLandEvent -= HandleLandEvent;
        OnHitEvent -= HandleHitEvent;
        OnDeadEvent -= HandleDeadEvent;
    }
}
