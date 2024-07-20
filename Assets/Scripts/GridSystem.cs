using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class GridSystem
{
    public GridSystem(int _rows, int _columns, int _GridSize, Vector3 _gridPosition)
    {
        rows = _rows;
        columns = _columns;
        gridSize = _GridSize;
        gridPosition = _gridPosition;


        CellContainer = new GameObject("Cell Container");

    }

    public int rows = 5;
    public int columns = 5;
    public float gridSize = 1;
    public Vector3 gridPosition = Vector3.zero;
    public GameObject cellVisualPrefab;

    private Color gridDebugColor = Color.white;
    private List<Grid> cells = new List<Grid>();
    private GameObject CellContainer;


    public void CreateGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                cells.Add(new Grid(i, j));
            }
        }
    }

    public void GridDisplay()
    {
        foreach(Grid g in cells)
        {
            var visualObject = GameObject.Instantiate(cellVisualPrefab);
            visualObject.transform.position = LocalToGlobalPos(g.position);
            visualObject.transform.SetParent(CellContainer.transform);

            g.cellVisual = visualObject.GetComponent<CellVisual>();
        }
    }

    public void ActivateEditing()
    {
        foreach (var item in cells)
        {
            var _object = item.currentObject?.GetComponent<IObject>();
            if(_object != null && _object.cellType == CellObject.OBJECT)
            {
                _object.ActivateEdit();
            }
        }
    }

    public void DeActivateEditing()
    {
        foreach (var item in cells)
        {
            var _object = item.currentObject?.GetComponent<IObject>();
            if (_object != null && _object.cellType == CellObject.OBJECT)
            {
                _object.DeActivateEdit();
            }
        }
    }

    public void ResetVisuals()
    {
        foreach (var g in cells)
        {
            g.cellVisual.ChangeColor(g.cellVisual.defaultColor);
        }
    }

    public void ResetSelect()
    {
        foreach (var g in cells)
        {
            g.cellVisual.SetSelect(false);
        }
    }

    public void DebugDraw()
    {
        Gizmos.color = gridDebugColor;
        foreach(Grid g in cells)
        {
            Gizmos.DrawWireCube(LocalToGlobalPos(g.position), new Vector3(gridSize, gridSize, 0.0001f));
        }
    }



    public Vector3 LocalToGlobalPos(Vector2 localPos)
    {
        var res = new Vector3();
        res = (Vector3)(localPos * gridSize) + gridPosition;
        return res;
    }

    public Vector3 GlobalToLocal(Vector2 globalPos)
    {
        Vector3 res = Vector3.zero;
        var newGridPos = new Vector3(gridPosition.x - gridSize/2, gridPosition.y - gridSize / 2);
        var localPost = (Vector3)globalPos - newGridPos;
        int x = Mathf.FloorToInt(localPost.x/gridSize);
        int y = Mathf.FloorToInt(localPost.y/gridSize);
        //res = new Vector3(Mathf.Floor(globalPos.x + Mathf.Ceil(Mathf.Abs(gridPosition.x))), Mathf.Floor(globalPos.y + Mathf.Ceil(Mathf.Abs(gridPosition.y))));
        res = new Vector3(x, y);
        return res;
    }

    public bool IsValidPosition(Vector3 globalPos)
    {
        if(((globalPos.x <= (rows * gridSize) + gridPosition.x - (gridSize / 2)) && (globalPos.y <= (columns * gridSize) + gridPosition.y - (gridSize / 2))) &&
            ((globalPos.x >= gridPosition.x - (gridSize/2)) && (globalPos.y >= gridPosition.y - (gridSize / 2))))
        {
            return true;
        }
        return false;
    }

    public Grid GetCellWithIndex(Vector2 index)
    {
        Grid res = null;
        foreach (var cell in cells)
        {
            if (cell.position == index)
            {
                res = cell;
                break;
            }
        }
        return res;
    }

    public Grid GetGrid(Vector3 globalPos)
    {
        Grid res = null;
        if (IsValidPosition(globalPos))
        {
            var localPos = GlobalToLocal(globalPos);
            res = GetCellWithIndex(localPos);
        }
        return res;
    }

    public void PlaceNewObject(Vector2 cellIndex, GameObject _object)
    {
        var _IObject = _object.GetComponent<IObject>();
        if(_IObject == null)
            return;

        var cell = GetCellWithIndex(cellIndex);
        if (cell.currentObject != null)
        {
            Debug.Log("Cell With Index!");
            return;
        }
            

        if (!IsPlaceAvailable(cellIndex, _IObject.objectSize))
        {
            Debug.Log("Space Unavailable");
            return;
        }

        var newObject = GameObject.Instantiate(_object);

        PlaceObjectOnCell(cell, newObject);

    }

    public void PlaceObjectOnCell(Grid cell, GameObject newObject)
    {
        var _IObject = newObject.GetComponent<IObject>();
        var objectSize = _IObject.objectSize;
        _IObject.pivotCell = cell;


        cell.neighbouringCells = new List<Grid>();

        for (int i = (int)cell.position.x; i < objectSize.x + (int)cell.position.x; i++)
        {
            for (int j = (int)cell.position.y; j < objectSize.y + (int)cell.position.y; j++)
            {
                var _cell = GetCellWithIndex(new Vector2(i, j));
                _cell.currentObject = newObject;
                
                cell.neighbouringCells.Add(_cell);
            }
        }

        foreach(var _cell in cell.neighbouringCells) 
        {
            if(_cell == cell) { continue; }
            _cell.neighbouringCells = cell.neighbouringCells;
        }
        var globalPos = LocalToGlobalPos(cell.position);

        newObject.transform.position = globalPos;

        _IObject.InitializeObject(this);
    }

    public void removeObject(Grid cell)
    {
        var cellType = cell.currentObject.GetComponent<IObject>().cellType;
        if(cellType == CellObject.BLOCK) { return; }
        GameObject.Destroy(cell.currentObject);
        cell.currentObject = null;

        foreach(var _cell in cell.neighbouringCells)
        {
            _cell.currentObject = null;
        }
    }

    public bool IsPlaceAvailable(Vector2 cellIndex, Vector2 cellSize, GameObject ignoreObject = null)
    {
        var max_X = (cellSize.x + cellIndex.x) - 1;
        var max_Y = (cellSize.y + cellIndex.y) - 1;

        if(max_X >= columns || max_Y >= rows) return false;

        for (int i = (int)cellIndex.x; i < cellSize.x + (int)cellIndex.x; i++)
        {
            for (int j = (int)cellIndex.y; j< cellSize.y + (int)cellIndex.y; j++)
            {
                var cell = GetCellWithIndex(new Vector2(i,j));
                if(cell == null) return false;
                
                if (cell.currentObject != null && cell.currentObject != ignoreObject) return false;
            }
        }
        return true;
    }

    public void MoveCellObject(Grid cell, Vector2 newIndex)
    {
        var currentObject = cell.currentObject;

        foreach (var item in cell.neighbouringCells)
        {
            item.currentObject = null;
        }

        var newCell = GetCellWithIndex(newIndex);

        PlaceObjectOnCell(newCell, currentObject);
    }

    public void LoadLevel(LevelData levelData)
    {
        foreach(var item in levelData.blocksData)
        {
            var newObject = levelData.blocks.GetBlock(item.cellType);

            PlaceNewObject(item.position, newObject.block);
        }

        foreach (var item in levelData.objectsData)
        {
            var newObject = levelData.objects.GetBlock(item.cellType);

            PlaceNewObject(item.position, newObject.block);
        }

    }

    public List<Grid> GetAllNeighbors(Grid cell)
    {
        var res = new List<Grid>();
        if (cell.currentObject == null) return res;

        var top = cell.position + new Vector2(0, 1);
        var bottom = cell.position + new Vector2(0, -1);
        var left = cell.position + new Vector2(-1, 0);
        var right = cell.position + new Vector2(1, 0);

        var topGrid = GetCellWithIndex(top);
        if (topGrid != null) res.Add(topGrid);

        var bottomGrid = GetCellWithIndex(bottom);
        if (bottomGrid != null) res.Add(bottomGrid);

        var leftGrid = GetCellWithIndex(left);
        if (leftGrid != null) res.Add(leftGrid);

        var rightGrid = GetCellWithIndex(right);
        if (rightGrid != null) res.Add(rightGrid);


        return res;
    }
}
