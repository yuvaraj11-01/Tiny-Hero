using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid System / Level Data")]

public class LevelData : ScriptableObject
{
    public int level;
    public BlocksContainer blocks;
    public List<CellData> data;
}

[System.Serializable]

public struct CellData
{
    public Vector2 position;
    public BlockType cellType;
}


public enum CellObject
{
    NULL,
    BLOCK,
    OBJECT
}