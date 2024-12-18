using System;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public event Action<bool> OnLandEvent; // 착지햇을때 스킬이 있으면 true
    public event Action OnHitEvent;
    public event Action OnDeadEvent;

    public event Action OnTagEvent;

    [Header("Reference")]
    private Rigidbody2D _rigidbody;

    [Header("Setting")]
    public BlockStatSO _blockStatSO;

    [Header("Info")]
    [SerializeField] private bool _isGrounded = false;
    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        OnLandEvent += HandleLandEvent;
        OnHitEvent += HandleHitEvent;
        OnDeadEvent += HandleDeadEvent;

        OnTagEvent += HandleTagEvent;
    }

    private void Start()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigidbody.gravityScale = 0;
    }

    public abstract void HandleLandEvent(bool hasAbility);
    public abstract void HandleHitEvent();
    public abstract void HandleDeadEvent();

    public void HandleTagEvent()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.None;
        _rigidbody.gravityScale = _blockStatSO.mass;
        transform.parent = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnTagEvent?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnLandEvent -= HandleLandEvent;
        OnHitEvent -= HandleHitEvent;
        OnDeadEvent -= HandleDeadEvent;

        OnTagEvent -= HandleTagEvent;
    }
}
