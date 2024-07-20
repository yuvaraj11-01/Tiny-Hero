using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPuzzle : MonoBehaviour
{
    public GameObject refObjectPrefab;

    private GridSystem gridSystem;
    private bool EditingEnabled;

    [SerializeField] private GameObject cellVisualPrefab;
    [SerializeField] private int rows = 7, colums = 7, gridSize = 1;
    [SerializeField] private Vector3 gridPosition;
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject ObjectsUI;

    [SerializeField] private List<Vector2> SourcePoints = new List<Vector2>();
    [SerializeField] private List<Vector2> EndPoints = new List<Vector2>();



    void Start()
    {
        gridSystem = new GridSystem(rows, colums, gridSize, gridPosition);

        gridSystem.cellVisualPrefab = cellVisualPrefab;
        gridSystem.CreateGrid();
        gridSystem.GridDisplay();
        gridSystem.LoadLevel(levelData);
        PlaceSourcePoints();
        EditingEnabled = false;

    }

    void PlaceSourcePoints()
    {

        foreach (var item in SourcePoints)
        {
            var newObject = levelData.blocks.GetBlock(BlockType.ONE_ONE_SOURCE);
            gridSystem.PlaceNewObject(item, newObject.block);
        }

        foreach (var item in EndPoints)
        {
            var newObject = levelData.blocks.GetBlock(BlockType.ONE_ONE_ENDPOINT);
            gridSystem.PlaceNewObject(item, newObject.block);
        }


    }

    void CalculateEnergy()
    {
        List<GameObject> VisitedObjects = new List<GameObject>();

        foreach (var endpoint in EndPoints)
        {
            DeAcivateEndPointObject(endpoint);
        }
        foreach (var source in SourcePoints)
        {
            var sourceCell = gridSystem.GetCellWithIndex(source);
            var sourceNeighbours = gridSystem.GetAllNeighbors(sourceCell);

            VisitedObjects.Add(sourceCell.currentObject);
            foreach (var neighbour in sourceNeighbours)
            {
                
                if (neighbour.currentObject != null)
                {
                    if (VisitedObjects.Contains(neighbour.currentObject)) continue;

                    var foundObject = neighbour.currentObject;
                    BranchOutFindRecurssion(foundObject, VisitedObjects);
                    if (EndPoints.Contains(neighbour.position))
                    {
                        //Debug.Log("End point Found at - " + neighbour);
                        AcivateEndPointObject(neighbour.position);
                    }

                }
            }

        }
    }

    

    void BranchOutFindRecurssion(GameObject Object, List<GameObject> VisitedObjects)
    {
        var _Object = Object.GetComponent<IObject>();
        var ObjectCells = _Object.pivotCell.neighbouringCells;

        VisitedObjects.Add(Object);


        List<GameObject> newlyFoundObjects = new List<GameObject>();

        foreach (var item in ObjectCells)
        {
            var itemNeighbors = gridSystem.GetAllNeighbors(item);
            foreach (var neighbour in itemNeighbors)
            {
                if (neighbour.currentObject != null && neighbour.currentObject != Object)
                {
                    if (VisitedObjects.Contains(neighbour.currentObject)) continue;

                    if (SourcePoints.Contains(neighbour.position))
                    {
                        continue;
                    }
                    if (EndPoints.Contains(neighbour.position))
                    {
                        //Debug.Log("End point Found at - " + neighbour);
                        AcivateEndPointObject(neighbour.position);
                        continue;
                    }
                    newlyFoundObjects.Add(neighbour.currentObject);
                }
            }
        }


        foreach (var item in newlyFoundObjects)
        {
            BranchOutFindRecurssion(item, VisitedObjects);
        }


    }

    void AcivateEndPointObject(Vector2 endPointPosition)
    {
        var endPointCell = gridSystem.GetCellWithIndex(endPointPosition);

        var EndPoint = endPointCell.currentObject?.GetComponent<EndPointBlock>();

        if (EndPoint == null) return;

        EndPoint.ActivateEndPoint();

    }

    void DeAcivateEndPointObject(Vector2 endPointPosition)
    {
        var endPointCell = gridSystem.GetCellWithIndex(endPointPosition);

        var EndPoint = endPointCell.currentObject?.GetComponent<EndPointBlock>();

        if (EndPoint == null) return;

        EndPoint.DeActivateEndPoint();

    }



    private void Update()
    {
        gridSystem.ResetVisuals();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!EditingEnabled)
            {
                ActivateEditing();
                EditingEnabled = true;
            }
            else
            {
                DeActivateEditing();
                EditingEnabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CalculateEnergy();
        }


        if (!EditingEnabled) return;

        if (Input.GetMouseButtonDown(0))
        {
            var cell = gridSystem.GetGrid(MouseInput.instance.GetMousePosition());
            //Debug.Log(cell);
            if (cell != null)
                if (cell.currentObject == null)
                {
                    gridSystem.PlaceNewObject(cell.position, refObjectPrefab);
                }
        }

        if (Input.GetMouseButtonDown(1))
        {
            var cell = gridSystem.GetGrid(MouseInput.instance.GetMousePosition());
            //Debug.Log(cell);
            if (cell != null)
                if (cell.currentObject != null)
                {
                    gridSystem.removeObject(cell);
                }
        }

        var hoverCell = gridSystem.GetGrid(MouseInput.instance.GetMousePosition());
        if (hoverCell != null)
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

    void ActivateEditing()
    {
        ObjectsUI.SetActive(true);
    }

    void DeActivateEditing()
    {
        ObjectsUI.SetActive(false);
    }

    public void LateUpdate()
    {
        CalculateEnergy();
    }

}
