using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid System / Blocks")]

public class BlocksContainer : ScriptableObject
{
    [SerializeField] List<BlockItem> Blocks = new List<BlockItem>();

    public BlockItem GetBlock(BlockType blockType)
    {
        BlockItem res;
        res = new BlockItem()
        {
            blockType = BlockType.NULL
        };
        foreach (var item in Blocks)
        {
            if(item.blockType == blockType)
            {
                return item;
            }
        }

        return res;
    }
}

[System.Serializable]
public struct BlockItem
{
    public BlockType blockType;
    public GameObject block;
}

public enum BlockType
{
    NULL,
    ONE_ONE,
    TWO_TWO,
    THREE_THREE,
    ONE_TWO,
    TWO_ONE
}