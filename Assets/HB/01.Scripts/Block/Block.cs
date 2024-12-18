using System;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public event Action<bool> OnLandEvent; // 착지햇을때 스킬이 있으면 true
    public event Action OnHitEvent;
    public event Action OnDeadEvent;

    [Header("Reference")]
    private Rigidbody2D _rigidbody;

    [Header("Setting")]
    public BlockStatSO _blockStatSO;

    [Header("Info")]
    [SerializeField] private bool _isInShop = true;
    [SerializeField] private bool _isGrounded = false;
    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        OnLandEvent += HandleLandEvent;
        OnHitEvent += HandleHitEvent;
        OnDeadEvent += HandleDeadEvent;
    }

    private void Start()
    {
        Debug.Log(_blockStatSO.cost);
    }

    private void Update()
    {
        CheckIsInShop();
    }

    public abstract void HandleLandEvent(bool hasAbility);
    public abstract void HandleHitEvent();
    public abstract void HandleDeadEvent();

    private void CheckIsInShop()
    {
        if (_isInShop)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rigidbody.gravityScale = 0;
        }
        else
        {
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.gravityScale = _blockStatSO.mass;
            transform.parent = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isInShop = false;

        }
    }

    private void OnDestroy()
    {
        OnLandEvent -= HandleLandEvent;
        OnHitEvent -= HandleHitEvent;
        OnDeadEvent -= HandleDeadEvent;
    }
}
