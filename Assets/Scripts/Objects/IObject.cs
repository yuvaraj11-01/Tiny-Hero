using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IObject
{
    public Grid pivotCell { get; set; }
    public CellObject cellType { get; set; }
    public Vector2 objectSize { get; set; }
    public void InitializeObject(GridSystem gridSystem);
    public void TerminateObject();
    public void ActivateEdit();
    public void DeActivateEdit();

}
