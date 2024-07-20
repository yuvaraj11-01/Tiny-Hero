using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IObject
{
    public CellObject cellType { get; set; }
    public Vector2 objectSize { get; set; }
    public void InitializeObject();
    public void TerminateObject();


}
