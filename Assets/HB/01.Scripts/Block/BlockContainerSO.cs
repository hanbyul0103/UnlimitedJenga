using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockContainerSO", menuName = "SO/Block/BlockContainerSO")]
public class BlockContainerSO : ScriptableObject
{
    public List<Block> blocks;

    public Block PickRandomBlock()
    {
        return blocks[Random.Range(0, blocks.Count)];
    }
}
