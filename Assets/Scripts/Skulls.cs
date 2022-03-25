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
    private SkullsColor skullsComponent;
    public SkullsColor SkullsComponent
    {
        get { return skullsComponent; }
    }

    void Awake()
    {
        movableComponent = GetComponent<SlotBehaviour>();
        skullsComponent = GetComponent<SkullsColor>();
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

    public bool IsColored()
    {
        return skullsComponent != null;
    }
}
