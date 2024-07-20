using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellVisual : MonoBehaviour
{
    public Color defaultColor;
    public Color HoverColor;
    public Color InvalidColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject SelectObject;

    public void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void SetSelect(bool value)
    {
        SelectObject.SetActive(value);
    }


}
