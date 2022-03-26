using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skulls : MonoBehaviour
{
    [SerializeField]
    public int x;
    public int y; 
    
    GridManager gridManager;
    [HideInInspector]
    public SpriteRenderer render;

    public string type;

    public void Constructor(GridManager GameManager, int X, int Y)
    {
        x = X;
        y = Y;
        gridManager = GameManager;

        name = string.Format("({0}, {1})", x, y);
        render = GetComponent<SpriteRenderer>();
        render.enabled = Y < gridManager.sizeY;
    }

    public void ChangePosition(int X, int Y)
    {
        x = X;
        y = Y;

        name = string.Format("({0}, {1})", x, y);
        render.enabled = Y < gridManager.sizeY;
    }

    void OnMouseDown()
    {
        gridManager.Move(this);
        print(string.Format("Drag {0},{1}", x, y));
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            gridManager.Drop(this);
            print(string.Format("Drop {0},{1}", x, y));
        }
    }
}