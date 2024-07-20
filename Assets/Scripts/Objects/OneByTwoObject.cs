using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneByTwoObject : MonoBehaviour, IObject
{
    public Vector2 objectSize { get => new Vector2(1, 2); set { } }

    public CellObject cellType { get => CellObject.OBJECT; set { } }

    public void InitializeObject()
    {
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
