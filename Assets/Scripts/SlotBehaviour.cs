using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBehaviour : MonoBehaviour
{
    private Skulls piece;
    private GridManager grid;

    void Awake()
    {
        piece = GetComponent<Skulls>();
    }
    
    public void Move(int newX, int newY, int newZ)
    {
        piece.x = newX;
        piece.y = newY;

        piece.transform.localPosition = piece.gridRef.GetWorldPosition(newX, newY, newZ);
    }
}
