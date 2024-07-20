using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneByTwoEnergyObject : MonoBehaviour, IObject
{
    public Vector2 objectSize { get => new Vector2(1, 2); set { } }

    public CellObject cellType { get => CellObject.OBJECT; set { } }

    GridSystem gridSystem;


    private Grid _pivotCell;

    public Grid pivotCell { get => _pivotCell; set => _pivotCell = value; }

    public void ActivateEdit()
    {

    }

    public void DeActivateEdit()
    {

    }


    bool isInitialized;
    public void InitializeObject(GridSystem gridSystem)
    {
        if (isInitialized)
        {
            return;
        }
        isInitialized = true;

        this.gridSystem = gridSystem;
        Debug.Log("object initialized");


    }

    public void TerminateObject()
    {
        Debug.Log("object terminated");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
}