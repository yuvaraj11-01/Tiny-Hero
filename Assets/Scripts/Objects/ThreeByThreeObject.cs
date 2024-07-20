using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeByThreeObject : MonoBehaviour, IObject
{
    public Vector2 objectSize { get => new Vector2(3, 3); set { } }

    public CellObject cellType { get => CellObject.OBJECT; set { } }

    private Grid _pivotCell;

    public Grid pivotCell { get => _pivotCell; set => _pivotCell = value; }

    GridSystem gridSystem;

    [SerializeField] GameObject EditCanvas;

    public void ActivateEdit()
    {
        EditCanvas.SetActive(true);
        CheckForArrows();

    }

    public void DeActivateEdit()
    {
        EditCanvas.SetActive(false);
        CheckForArrows();

    }

    private Button Left, Right, Top, Bottom;

    bool isInitialized;
    public void InitializeObject(GridSystem gridSystem)
    {
        if (isInitialized)
        {
            CheckForArrows();
            return;
        }
        isInitialized = true;


        this.gridSystem = gridSystem;
        Debug.Log("object initialized");

        Left = EditCanvas.transform.Find("Left").GetComponent<Button>();
        Right = EditCanvas.transform.Find("Right").GetComponent<Button>();
        Top = EditCanvas.transform.Find("Top").GetComponent<Button>();
        Bottom = EditCanvas.transform.Find("Bottom").GetComponent<Button>();

        Left.onClick.AddListener(OnLeftClick);
        Right.onClick.AddListener(OnRightClick);
        Top.onClick.AddListener(OnTopClick);
        Bottom.onClick.AddListener(OnBottomClick);

    }

    public void CheckForArrows()
    {
        var newCellIndex = _pivotCell.position + new Vector2(-1, 0);
        if (!gridSystem.IsPlaceAvailable(newCellIndex, objectSize, this.gameObject))
        {
            Left.gameObject.SetActive(false);
        }
        else
        {
            Left.gameObject.SetActive(true);
        }

        var newCellIndex_1 = _pivotCell.position + new Vector2(1, 0);
        if (!gridSystem.IsPlaceAvailable(newCellIndex_1, objectSize, this.gameObject))
        {
            Right.gameObject.SetActive(false);
        }
        else
        {
            Right.gameObject.SetActive(true);
        }

        var newCellIndex_2 = _pivotCell.position + new Vector2(0, 1);
        if (!gridSystem.IsPlaceAvailable(newCellIndex_2, objectSize, this.gameObject))
        {
            Top.gameObject.SetActive(false);
        }
        else
        {
            Top.gameObject.SetActive(true);
        }

        var newCellIndex_3 = _pivotCell.position + new Vector2(0, -1);
        if (!gridSystem.IsPlaceAvailable(newCellIndex_3, objectSize, this.gameObject))
        {
            Bottom.gameObject.SetActive(false);
        }
        else
        {
            Bottom.gameObject.SetActive(true);
        }



    }

    public void OnLeftClick()
    {
        var newCellIndex = _pivotCell.position + new Vector2(-1, 0);
        if (!gridSystem.IsPlaceAvailable(newCellIndex, objectSize, this.gameObject)) return;

        gridSystem.MoveCellObject(_pivotCell, newCellIndex);

    }

    public void OnRightClick()
    {
        var newCellIndex = _pivotCell.position + new Vector2(1, 0);
        if (!gridSystem.IsPlaceAvailable(newCellIndex, objectSize, this.gameObject)) return;

        gridSystem.MoveCellObject(_pivotCell, newCellIndex);
    }

    public void OnTopClick()
    {
        var newCellIndex = _pivotCell.position + new Vector2(0, 1);
        if (!gridSystem.IsPlaceAvailable(newCellIndex, objectSize, this.gameObject)) return;

        gridSystem.MoveCellObject(_pivotCell, newCellIndex);
    }

    public void OnBottomClick()
    {
        var newCellIndex = _pivotCell.position + new Vector2(0, -1);
        if (!gridSystem.IsPlaceAvailable(newCellIndex, objectSize, this.gameObject)) return;

        gridSystem.MoveCellObject(_pivotCell, newCellIndex);
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
    void LateUpdate()
    {
        if (isInitialized)
        {
            CheckForArrows();
        }
    }
}
