using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public event Action<bool> OnLandEvent; // ���������� ��ų�� ������ true
    public event Action OnHitEvent;
    public event Action OnDeadEvent;

    public event Action OnTagEvent;

    [Header("Reference")]
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    [Header("Setting")]
    public BlockStatSO _blockStatSO;

    [Header("Info")]
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
        _rigidbody.mass = 0;
        _rigidbody.gravityScale = 0;
        _collider.isTrigger = true;
    }

    public virtual void HandleLandEvent(bool hasAbility)
    {
        SoundManager.Instance.PlaySFX("BlockFall");
    }
    public abstract void HandleHitEvent();
    public abstract void HandleDeadEvent();

    public void HandleTagEvent()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.None;
        _rigidbody.mass = _blockStatSO.mass;
        _rigidbody.gravityScale = 8;
        _collider.isTrigger = false;
        transform.parent = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Block"))
        {
            SoundManager.Instance.PlaySFX("BlockFall");
        }
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
