using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoByOneObject : MonoBehaviour, IObject
{
    public Vector2 objectSize { get => new Vector2(1, 2); set { } }

    public CellObject cellType { get => CellObject.OBJECT; set { } }

    GridSystem gridSystem;

    [SerializeField] GameObject EditCanvas;

    private Grid _pivotCell;

    public Grid pivotCell { get => _pivotCell; set => _pivotCell = value; }

    public Transform RayShootPoint;
    public Transform RayShootPoint2;

    public LayerMask collLayer;
    public float rayDistance = 0.5f;

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

    private GameObject LeftColl, RightColl, TopColl, BottomColl, Top2Coll, Bottom2Coll;




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

        LeftColl = transform.Find("LeftCol").gameObject;
        RightColl = transform.Find("RightCol").gameObject;
        TopColl = transform.Find("TopCol").gameObject;
        BottomColl = transform.Find("BottomCol").gameObject;

        Top2Coll = transform.Find("Top2Col").gameObject;
        Bottom2Coll = transform.Find("Bottom2Col").gameObject;

        Left.onClick.AddListener(OnLeftClick);
        Right.onClick.AddListener(OnRightClick);
        Top.onClick.AddListener(OnTopClick);
        Bottom.onClick.AddListener(OnBottomClick);

    }
    List<GameObject> KnownCollider = new List<GameObject>();
    public void CheckForColliders()
    {
        foreach (var item in KnownCollider)
        {
            item.SetActive(true);
        }

        var CollUp = Physics2D.Raycast(RayShootPoint.position, Vector2.up, rayDistance, collLayer);
        if (CollUp.collider != null)
        {
            TopColl.SetActive(false);
            CollUp.collider.gameObject.SetActive(false);
            if (!KnownCollider.Contains(CollUp.collider.gameObject))
            {
                KnownCollider.Add(CollUp.collider.gameObject);
            }
        }

        var CollDown = Physics2D.Raycast(RayShootPoint.position, Vector2.down, rayDistance, collLayer);
        if (CollDown.collider != null)
        {
            BottomColl.SetActive(false);
            CollDown.collider.gameObject.SetActive(false);
            if (!KnownCollider.Contains(CollDown.collider.gameObject))
            {
                KnownCollider.Add(CollDown.collider.gameObject);
            }
        }

        var Coll2Up = Physics2D.Raycast(RayShootPoint2.position, RayShootPoint2.transform.up, rayDistance, collLayer);
        if (Coll2Up.collider != null)
        {
            Top2Coll.SetActive(false);
            Coll2Up.collider.gameObject.SetActive(false);
            if (!KnownCollider.Contains(Coll2Up.collider.gameObject))
            {
                KnownCollider.Add(Coll2Up.collider.gameObject);
            }
        }

        var Coll2Down = Physics2D.Raycast(RayShootPoint2.position, -RayShootPoint2.transform.up, rayDistance, collLayer);
        if (Coll2Down.collider != null)
        {
            Bottom2Coll.SetActive(false);
            Coll2Down.collider.gameObject.SetActive(false);
            if (!KnownCollider.Contains(Coll2Down.collider.gameObject))
            {
                KnownCollider.Add(Coll2Down.collider.gameObject);
            }
        }

        var CollRight = Physics2D.Raycast(RayShootPoint.position, Vector2.right, rayDistance, collLayer);
        if (CollRight.collider != null)
        {
            RightColl.SetActive(false);
            CollRight.collider.gameObject.SetActive(false);
            if (!KnownCollider.Contains(CollRight.collider.gameObject))
            {
                KnownCollider.Add(CollRight.collider.gameObject);
            }
        }

        var CollLeft = Physics2D.Raycast(RayShootPoint.position, Vector2.left, rayDistance, collLayer);
        if (CollLeft.collider != null)
        {
            LeftColl.SetActive(false);
            CollLeft.collider.gameObject.SetActive(false);
            if (!KnownCollider.Contains(CollLeft.collider.gameObject))
            {
                KnownCollider.Add(CollLeft.collider.gameObject);
            }
        }




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
            CheckForColliders();
        }
    }


}
