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
    private Collider2D _collider;

    [Header("Setting")]
    public BlockStatSO _blockStatSO;

    [Header("Info")]
    [SerializeField] private bool _isGrounded = false;
    public bool IsGrounded => _isGrounded;
    private int _count;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        OnLandEvent += HandleLandEvent;
        OnHitEvent += HandleHitEvent;
        OnDeadEvent += HandleDeadEvent;

        OnTagEvent += HandleTagEvent;
    }

    private void Start()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigidbody.gravityScale = 0;
        _collider.isTrigger = true;
    }

    public abstract void HandleLandEvent(bool hasAbility);
    public abstract void HandleHitEvent();
    public abstract void HandleDeadEvent();

    public void HandleTagEvent()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.None;
        _rigidbody.gravityScale = _blockStatSO.mass;
        _collider.isTrigger = false;
        transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count++;

            if (_count == 2)
                OnTagEvent?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count--;
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
