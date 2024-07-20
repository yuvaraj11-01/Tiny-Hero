using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{

    public static MouseInput instance;

    public Camera cam;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
    }

    public Vector3 GetMousePosition()
    {
        var screenPos = Input.mousePosition;
        var mousePos = cam.ScreenToWorldPoint(screenPos);

        return mousePos;
    }

}
