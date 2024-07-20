using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid System / Level Data")]

public class LevelData : ScriptableObject
{
    public int level;
    public BlocksContainer blocks;
    public ObjectsContainer objects;

    public List<BlockCellData> blocksData = new List<BlockCellData>();

    public List<ObjectCellData> objectsData = new List<ObjectCellData>();
}

[System.Serializable]

public struct BlockCellData
{
    public Vector2 position;
    public BlockType cellType;
}

[System.Serializable]
public struct ObjectCellData
{
    public Vector2 position;
    public ObjectType cellType;
}


public enum CellObject
{
    NULL,
    BLOCK,
    OBJECT
}