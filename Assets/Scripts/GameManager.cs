using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GridSystem gridSystem;// = new GridSystem(7, 7, 1, new Vector3(-3.5f, -3.5f, 0));

    [SerializeField] private GameObject cellVisualPrefab;
    public GameObject refObjectPrefab;
    [SerializeField] private int rows = 7, colums = 7, gridSize = 1;
    [SerializeField] private Vector3 gridPosition;
    [SerializeField] private LevelData levelData;


    void Start()
    {
        gridSystem = new GridSystem(rows, colums, gridSize, gridPosition);

        gridSystem.cellVisualPrefab = cellVisualPrefab;
        gridSystem.CreateGrid();
        gridSystem.GridDisplay();
        gridSystem.LoadLevel(levelData);
    }

    void Update()
    {
        gridSystem.ResetVisuals();

        if (Input.GetMouseButtonDown(0))
        {
            var cell = gridSystem.GetGrid(MouseInput.instance.GetMousePosition());
            Debug.Log(cell);
            if (cell != null)
                if (cell.currentObject == null)
                {
                gridSystem.PlaceNewObject(cell.position, refObjectPrefab);
                }
        }

        if (Input.GetMouseButtonDown(1))
        {
            var cell = gridSystem.GetGrid(MouseInput.instance.GetMousePosition());
            Debug.Log(cell);
            if (cell != null)
                if (cell.currentObject != null)
                {
                gridSystem.removeObject(cell);                
                }
        }

        var hoverCell = gridSystem.GetGrid(MouseInput.instance.GetMousePosition());
        if(hoverCell != null)
        {
            if (hoverCell.currentObject == null)
            {
                hoverCell.cellVisual.ChangeColor(hoverCell.cellVisual.HoverColor);
            }
            else 
            {
                hoverCell.cellVisual.ChangeColor(hoverCell.cellVisual.InvalidColor);
            }
        }

    }

    private void OnDrawGizmos()
    {
        gridSystem?.DebugDraw();
    }
}
