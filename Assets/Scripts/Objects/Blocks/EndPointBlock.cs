using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointBlock : MonoBehaviour, IObject
{

    [SerializeField] Color EnableColor;
    [SerializeField] Color DisableColor;
    [SerializeField] SpriteRenderer block;


    public Vector2 objectSize { get => new Vector2(1, 1); set { } }

    public CellObject cellType { get => CellObject.BLOCK; set { } }

    private Grid _pivotCell;

    public Grid pivotCell { get => _pivotCell; set => _pivotCell = value; }

    GridSystem gridSystem;


    public void ActivateEdit()
    {
    }

    public void DeActivateEdit()
    {
    }

    bool isInitialized;
    public void InitializeObject(GridSystem gridSystem)
    {
        if (isInitialized) return;

        isInitialized = true;


        this.gridSystem = gridSystem;
        Debug.Log("object initialized");
    }

    public void TerminateObject()
    {
        Debug.Log("object terminated");
    }

    public void ActivateEndPoint()
    {
        block.color = EnableColor;
    }

    public void DeActivateEndPoint()
    {
        block.color = DisableColor;
    }
}
