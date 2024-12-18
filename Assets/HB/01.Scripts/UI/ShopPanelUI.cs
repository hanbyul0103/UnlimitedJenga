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

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            OpenPopup(1);
    }

    public void OpenPopup(float _duration)
    {
        if (_isTweening) return; // 이전에 있던 트윈 킬하고 열기

        _isTweening = true;

        SettingBlock();

        transform.DOMoveX(_targetPositionX, _duration)
            .OnComplete(() => _isTweening = false);
    }

    public void ClosePopup(float _duration)
    {
        if (_isTweening) return; // 이전에 있던 트윈 킬하고 닫기

        _isTweening = true;
        transform.DOMoveX(_originPositionX, _duration)
            .OnComplete(() => _isTweening = false);
    }

    private void SettingBlock()
    {
        // 랜덤뽑기
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

        // ignore collider
        for (int i = 0; i < spawnedBlockColliders.Count; i++)
        {
            Physics2D.IgnoreCollision(spawnedBlockColliders[i], spawnedBlockColliders[(i + 1) % spawnedBlockColliders.Count]);
        }

    }
}
