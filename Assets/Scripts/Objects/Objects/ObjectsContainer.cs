using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Grid System / Objects")]

public class ObjectsContainer : ScriptableObject
{
    [SerializeField] List<ObjectItem> Blocks = new List<ObjectItem>();

    public ObjectItem GetBlock(ObjectType blockType)
    {
        ObjectItem res;
        res = new ObjectItem()
        {
            blockType = ObjectType.NULL
        };
        foreach (var item in Blocks)
        {
            if (item.blockType == blockType)
            {
                return item;
            }
        }

        return res;
    }
}

[System.Serializable]
public struct ObjectItem
{
    public ObjectType blockType;
    public GameObject block;
}

public enum ObjectType
{
    NULL,
    ONE_ONE,
    TWO_TWO,
    THREE_THREE,
    ONE_TWO,
    TWO_ONE,
    ONE_TWO_ENERGY,
    TWO_ONE_ENERGY
}
