using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skulls : MonoBehaviour
{
    public int x;
    public int y;

    public int X
    {
        get { return x; }
        set
        {
            if (IsMovable())
            {
                x = value;
            }
        }
    }
    public int Y
    {
        get { return y; }
        set
        {
            if (IsMovable())
            {
                x = value;
            }
        }
    }

    private GridManager.PieceType type;

    public GridManager.PieceType Type
    {
        get { return type; }
    }

    private GridManager grid;

    public GridManager gridRef
    {
        get { return grid; }
    }

    private SlotBehaviour movableComponent;
    public SlotBehaviour MovableComponent
    {
        get { return movableComponent; }
    }

    void Awake()
    {
        movableComponent = GetComponent<SlotBehaviour>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int _x, int _y, GridManager _grid, GridManager.PieceType _type)
    {
        x = _x;
        y = _y;
        grid = _grid;
        type = _type;
    }

    public bool IsMovable()
    {
        return movableComponent != null;
    }
}
