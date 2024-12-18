using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopPanelUI : MonoBehaviour, IPopup
{
    [SerializeField] private BlockContainerSO _blockContainerSO;

    public List<Block> blocks = new List<Block>(4);
    public List<Transform> points = new List<Transform>(4);
    public List<BoxCollider2D> spawnedBlockColliders = new List<BoxCollider2D>();

    [SerializeField] private float _originPositionX;
    [SerializeField] private float _targetPositionX;

    private bool _isTweening;
    [SerializeField] private int _count;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            OpenPopup(1);
    }

    public void OpenPopup(float _duration)
    {
        if (_isTweening) transform.DOKill();

        _isTweening = true;

        SettingBlock();

        transform.DOMoveX(_targetPositionX, _duration)
            .OnComplete(() => _isTweening = false);
    }

    public void ClosePopup(float _duration)
    {
        if (_isTweening) transform.DOKill();

        _isTweening = true;
        transform.DOMoveX(_originPositionX, _duration)
            .OnComplete(() => _isTweening = false);
    }

    private void SettingBlock()
    {
        for (int i = 0; i < 4; i++)
        {
            if (blocks[i] == null)
            {
                blocks[i] = _blockContainerSO.PickRandomBlock();

                Block block = Instantiate(blocks[i], points[i].position, Quaternion.identity);
                block.transform.SetParent(points[i], true);

                spawnedBlockColliders.Add(block.GetComponent<BoxCollider2D>());
            }
        }

        for (int i = 0; i < spawnedBlockColliders.Count; i++)
        {
            for (int k = 0; k < spawnedBlockColliders.Count; k++)
            {
                if (i == k) continue;
                Physics2D.IgnoreCollision(spawnedBlockColliders[i], spawnedBlockColliders[k]);
                Physics2D.IgnoreCollision(spawnedBlockColliders[k], spawnedBlockColliders[i]);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _count++;

        if (_count == 2)
            OpenPopup(1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _count--;

        if (_count == 0)
            ClosePopup(1);
    }
}
