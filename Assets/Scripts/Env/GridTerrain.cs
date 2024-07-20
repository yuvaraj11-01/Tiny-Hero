using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTerrain : MonoBehaviour
{
    private GridSystem gridSystem;
    private bool EditingEnabled;

    [SerializeField] private GameObject cellVisualPrefab;
    [SerializeField] private int rows = 7, colums = 7;
    [SerializeField] private float gridSize = 1;
    [SerializeField] private Vector3 gridPosition;
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject ObjectsUI;


    void Start()
    {
        gridSystem = new GridSystem(rows, colums, gridSize, gridPosition);

        gridSystem.cellVisualPrefab = cellVisualPrefab;
        gridSystem.CreateGrid();
        gridSystem.GridDisplay();
        gridSystem.LoadLevel(levelData);
        EditingEnabled = false;

    }


}
