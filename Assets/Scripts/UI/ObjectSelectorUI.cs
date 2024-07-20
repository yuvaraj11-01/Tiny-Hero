using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelectorUI : MonoBehaviour
{
    public GameObject Selector;
    public GameObject _object;
    public EnergyPuzzle puzzle;

    public void onClick()
    {
        Selector.transform.position = transform.position;
        puzzle.refObjectPrefab = _object;
    }
}
