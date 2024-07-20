using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{
    public List<Grid> neighbouringCells = new List<Grid>();

    public Grid(int x, int y)
    {
        position = new Vector2(x, y);
    }

    public CellVisual cellVisual;

    public Vector2 position;
    public GameObject currentObject;
    public override string ToString()
    {
        return ($"x : {position.x} y : {position.y}");
    }

}
