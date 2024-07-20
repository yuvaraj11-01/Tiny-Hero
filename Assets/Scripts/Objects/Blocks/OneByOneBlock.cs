using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneByOneBlock : MonoBehaviour, IObject
{
    public Vector2 objectSize { get => new Vector2(1, 1); set { } }

    public CellObject cellType { get => CellObject.BLOCK; set { } }

    private Grid _pivotCell;

    public Grid pivotCell { get => _pivotCell; set => _pivotCell = value; }

    GridSystem gridSystem;

    [SerializeField] GameObject EditCanvas;

    public void ActivateEdit()
    {
        EditCanvas.SetActive(true);
    }

    public void DeActivateEdit()
    {
        EditCanvas.SetActive(false);
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
