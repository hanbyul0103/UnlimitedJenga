using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPanelUI : MonoBehaviour, IPopup
{
    [SerializeField] private BlockContainerSO _blockContainerSO;

    public List<Block> blocks = new List<Block>(4);
    public List<Transform> points = new List<Transform>(4);
    private Dictionary<Block, System.Action> handlers = new();
    public List<TextMeshPro> costs = new List<TextMeshPro>(4);

    [SerializeField] private float _originPositionX;
    [SerializeField] private float _targetPositionX;

    private bool _isTweening;
    [SerializeField] private int _count;

    public int Block { get; private set; }

    private void Awake()
    {
        Block = LayerMask.NameToLayer("Block");
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
                Block block = blocks[i] = Instantiate(_blockContainerSO.PickRandomBlock(), points[i].position, Quaternion.identity);
                block.transform.SetParent(points[i], true);

                costs[i].text = $"{block._blockStatSO.cost}$";

                handlers[block] = () =>
                {
                    HandleBlockTag(block);
                };
                block.OnTagEvent += handlers[block];
            }
        }
    }

    private void HandleBlockTag(Block block)
    {
        block.OnTagEvent -= handlers[block];
        handlers.Remove(block);

        int idx = blocks.IndexOf(block);
        if (idx >= 0)
        {
            blocks[idx].gameObject.layer = Block;
            blocks[idx] = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count++;

            if (_count == 2)
                OpenPopup(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count--;

            if (_count == 0)
                ClosePopup(1);
        }
    }
}
